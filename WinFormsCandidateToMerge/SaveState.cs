namespace WinFormsCandidateToMerge
{

    public class ColumnState
    {
        public string Identifier { get; set; }

        public int DisplayIndex { get; set; }

        public int Width { get; set; }
    }


    public class SaveState
    {
        public int SplitterDistance1 { get; set; }
        public int SplitterDistance2 { get; set; }
        public int SplitterDistance3 { get; set; }

        public ColumnState[] DataGridView1 { get; set; }

        public ColumnState[] DataGridView2 { get; set; }

        public ColumnState[] DataGridView3 { get; set; }

        public ColumnState[] DataGridView4 { get; set; }


    }
}