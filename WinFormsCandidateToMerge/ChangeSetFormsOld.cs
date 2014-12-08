using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace WinFormsCandidateToMerge
{
    public partial class ChangeSetFormsOld : DockContent
    {
        private readonly DataSetManipulator _dataSetManipulator;
        private readonly ChangesetVisualizer _changesetVisualizer;


        public ChangeSetFormsOld(DataSetManipulator dataSetManipulator)
        {
            _dataSetManipulator = dataSetManipulator;
            _changesetVisualizer = new ChangesetVisualizer();

            InitializeComponent();


            CloseButtonVisible = false;
        }

        public void Initialise()
        {
            dsCandidateToMerge2.DataSource = _dataSetManipulator.DsCandidateToMerge;

            dataListView1.DataSource = new DataView(
                _dataSetManipulator.DsCandidateToMerge.MergeResult, 
                "IsToDisplay = true", "", 
                DataViewRowState.CurrentRows);
            //AutosizeColumns();
        }

        private void AutosizeColumns()
        {
            foreach (ColumnHeader col in dataListView1.Columns)
            {
                //auto resize column width

                int colWidthBeforeAutoResize = col.Width;
                col.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                int colWidthAfterAutoResizeByHeader = col.Width;
                col.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                int colWidthAfterAutoResizeByContent = col.Width;

                if (colWidthAfterAutoResizeByHeader > colWidthAfterAutoResizeByContent)
                    col.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);

                //specific adjusts

                //first column
                if (col.Index == 0)
                    //we have to manually take care of tree structure, checkbox and image
                    col.Width += 16 + 16 + dataListView1.SmallImageSize.Width;
                //last column
                else if (col.Index == dataListView1.Columns.Count - 1)
                    //avoid "fill free space" bug
                    if (colWidthBeforeAutoResize > colWidthAfterAutoResizeByContent)
                        col.Width = colWidthBeforeAutoResize;
                    else
                        col.Width = colWidthAfterAutoResizeByContent;
            }
        }

        private void dataListView1_CellRightClick(object sender, BrightIdeasSoftware.CellRightClickEventArgs e)
        {
            toolStripIgnore.Enabled = dataListView1.SelectedObjects.Count > 0;
            toolStripDetail.Enabled = dataListView1.SelectedObjects.Count == 1;
            e.MenuStrip = this.contextMenuStrip1;

        }

        private bool TryGetSelectedChangetSetId2(out int outChangesetId)
        {
            outChangesetId = ((DsCandidateToMerge.MergeResultRow)
             (dataListView1.SelectedObjects.OfType<DataRowView>().First().Row))
                .ChangesetId;

            return true;
        }

        private void contextMenuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == toolStripIgnore)
            {
                var confirmResult = MessageBox.Show("Are you sure to ignore this item ?",
                                     "Confirm Ignorer!",
                                     MessageBoxButtons.YesNo);
                if (confirmResult != DialogResult.Yes)
                    return;

                foreach (DataRowView obj in dataListView1.SelectedObjects.OfType<DataRowView>())
                {
                    _dataSetManipulator.Ignore((DsCandidateToMerge.MergeResultRow)
                        ((DataRowView)(obj)).Row);
                }
            }
            if (e.ClickedItem == toolStripDetail)
            {
                int csId;
                if (TryGetSelectedChangetSetId2(out csId))
                {
                    try
                    {
                        _changesetVisualizer.Execute(csId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error while starting tf.exe\r\n\r\n" + ex.ToString(), "Arf !");
                    }
                }
            }

        }
    }
}
