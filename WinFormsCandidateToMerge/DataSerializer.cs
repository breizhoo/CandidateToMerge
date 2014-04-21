using System.IO;

namespace WinFormsCandidateToMerge
{
    public class DataSerializer
    {
        private readonly DsCandidateToMerge _dsCandidateToMerge;

        public DataSerializer(DsCandidateToMerge dsCandidateToMerge)
        {
            _dsCandidateToMerge = dsCandidateToMerge;
        }

        public void Save()
        {
            _dsCandidateToMerge.AcceptChanges();
            _dsCandidateToMerge.WriteXml(@"Data.xml");
        }

        public void Restore()
        {
            var f = new FileInfo(@"Data.xml");
            if (f.Exists)
                _dsCandidateToMerge.ReadXml(@"Data.xml");

        }

    }
}