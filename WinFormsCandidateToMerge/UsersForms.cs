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
    public partial class UsersForms : DockContent
    {
        private readonly DataSetManipulator _dataSetManipulator;

        public UsersForms(DataSetManipulator dataSetManipulator)
        {
            _dataSetManipulator = dataSetManipulator;

            InitializeComponent();

            CloseButtonVisible = false;

        }

        public void Initialise()
        {
            dsCandidateToMerge2.DataSource = _dataSetManipulator.DsCandidateToMerge;
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void toolStripCheckSelected_Click(object sender, EventArgs e)
        {
            CheckUncheckUsers(true);
        }

        private void CheckUncheckUsers(bool check)
        {
            foreach (DataRowView obj in dataListView1.SelectedObjects.OfType<DataRowView>())
            {
                var item = ((DsCandidateToMerge.UsersRow)
                    ((DataRowView) (obj)).Row);

                item.IsToScan = check;
            }
        }

        private void toolStripUncheckSelected_Click(object sender, EventArgs e)
        {
            CheckUncheckUsers(false);
        }
    }
}
