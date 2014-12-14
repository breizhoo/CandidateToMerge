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
            if (_dsCandidateToMerge.HasChanges())
                _dsCandidateToMerge.AcceptChanges();
            _dsCandidateToMerge.WriteXml(@"Data.xml");
        }

        public void Restore()
        {
            var f = new FileInfo(@"Data.xml");

            if (!f.Exists) return;

            _dsCandidateToMerge.ReadXml(@"Data.xml");
            if (_dsCandidateToMerge.HasChanges())
                _dsCandidateToMerge.AcceptChanges();
        }

    }
}