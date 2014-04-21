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

                    _ui.splitContainer1.SplitterDistance = p.SplitterDistance1;
                    _ui.splitContainer2.SplitterDistance = p.SplitterDistance2;
                    _ui.splitContainer3.SplitterDistance = p.SplitterDistance3;

                    SetColumnState(_ui.dgvResult.Columns, p.DataGridView1);
                    SetColumnState(_ui.dataGridView2.Columns, p.DataGridView2);
                    SetColumnState(_ui.dataGridView3.Columns, p.DataGridView3);
                    SetColumnState(_ui.dataGridView4.Columns, p.DataGridView4);
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

                DataGridView2 = GetColumnState(_ui.dataGridView2.Columns),
                DataGridView3 = GetColumnState(_ui.dataGridView3.Columns),
                DataGridView4 = GetColumnState(_ui.dataGridView4.Columns),
                DataGridView1 = GetColumnState(_ui.dgvResult.Columns)
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
                c[csc.Identifier].DisplayIndex = csc.DisplayIndex;
                c[csc.Identifier].Width = csc.Width;
            }
        }

    }
}