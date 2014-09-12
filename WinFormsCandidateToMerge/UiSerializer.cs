using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace WinFormsCandidateToMerge
{
    public class UiSerializer
    {
        private readonly CandidateToMerge _ui;

        public UiSerializer(CandidateToMerge ui)
        {
            _ui = ui;
        }

        public void Restore()
        {
            if (new FileInfo("SaveState.xml").Exists)
            {
                var xs = new XmlSerializer(typeof(UiSerializeRoot));
                using (var rd = new StreamReader("SaveState.xml"))
                {
                    var p = xs.Deserialize(rd) as UiSerializeRoot;

                    if (p.FormSize != Size.Empty)
                        _ui.Size = p.FormSize;

                    if (p.FormPosition != Point.Empty)
                        _ui.Location = p.FormPosition;

                    _ui.WindowState = p.WindowState;

                    _ui.splitContainer1.SplitterDistance = p.SplitterDistance1;
                    _ui.splitContainer2.SplitterDistance = p.SplitterDistance2;
                    _ui.splitContainer3.SplitterDistance = p.SplitterDistance3;


                    SetColumnState(_ui.dgvResult.Columns, p.DataGridView1);
                    SetColumnState(_ui.dgvUsers.Columns, p.DataGridView2);
                    SetColumnState(_ui.dgvProjects.Columns, p.DataGridView3);
                    SetColumnState(_ui.dgvBranches.Columns, p.DataGridView4);

                    var item = _ui.dgvUsers.Columns
                        .Cast<DataGridViewColumn>().FirstOrDefault(x => x.Name == p.dataGridView2SortedColumn);
                    if (item != null)
                        _ui.dgvUsers.Sort(item, p.dataGridView2SortOrder == SortOrder.Descending 
                            ? ListSortDirection.Descending 
                            : ListSortDirection.Ascending
                            );
                }
            }
        }

        public void Save()
        {
            var s = new UiSerializeRoot
            {
                SplitterDistance1 = _ui.splitContainer1.SplitterDistance,
                SplitterDistance2 = _ui.splitContainer2.SplitterDistance,
                SplitterDistance3 = _ui.splitContainer3.SplitterDistance,

                DataGridView2 = GetColumnState(_ui.dgvUsers.Columns),
                DataGridView3 = GetColumnState(_ui.dgvProjects.Columns),
                DataGridView4 = GetColumnState(_ui.dgvBranches.Columns),
                DataGridView1 = GetColumnState(_ui.dgvResult.Columns),

                FormSize = _ui.Size,
                FormPosition = _ui.Location,
                WindowState = _ui.WindowState,

                dataGridView2SortOrder = _ui.dgvUsers.SortOrder,
                dataGridView2SortedColumn = _ui.dgvUsers.SortedColumn.Name,
            };

            var xs = new XmlSerializer(typeof(UiSerializeRoot));
            using (var wr = new StreamWriter("SaveState.xml"))
            {
                xs.Serialize(wr, s);
            }
        }

        private static UiSerializeColumnState[] GetColumnState(DataGridViewColumnCollection c)
        {
            return c.Cast<DataGridViewColumn>()
                .Select(x => new UiSerializeColumnState
                {
                    DisplayIndex = x.DisplayIndex,
                    Identifier = x.Name,
                    Width = x.Width
                }).ToArray();
        }

        private static void SetColumnState(DataGridViewColumnCollection c, UiSerializeColumnState[] cs)
        {
            foreach (var csc in cs)
            {
                if (!c.Contains(csc.Identifier))
                    continue;
                c[csc.Identifier].DisplayIndex = csc.DisplayIndex;
                c[csc.Identifier].Width = csc.Width;
            }
        }

    }
}