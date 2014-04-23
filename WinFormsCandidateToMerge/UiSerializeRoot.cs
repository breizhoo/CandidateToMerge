using System.Drawing;

namespace WinFormsCandidateToMerge
{
    public class UiSerializeRoot
    {
        public int SplitterDistance1 { get; set; }
        public int SplitterDistance2 { get; set; }
        public int SplitterDistance3 { get; set; }

        public UiSerializeColumnState[] DataGridView1 { get; set; }

        public UiSerializeColumnState[] DataGridView2 { get; set; }

        public UiSerializeColumnState[] DataGridView3 { get; set; }

        public UiSerializeColumnState[] DataGridView4 { get; set; }

        public Size FormSize { get; set; }

        public Point FormPosition { get; set; }
    }
}