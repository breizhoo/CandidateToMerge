using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsCandidateToMerge
{
    public class ChangesetVisualizer
    {
        string GetTfexePath()
        {
            var programFilesPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            const string tfexe = @"\Microsoft Visual Studio 12.0\Common7\IDE\tf.exe";

            return Path.Combine(programFilesPath, tfexe);
        }

        public bool IsVisualizerAvailable
        {
            get { return File.Exists(this.GetTfexePath()); }
        }

        public void Execute(int changesetId)
        {
            var arguments = string.Format("changeset {0}", changesetId);

            var process = new Process();
            process.StartInfo.FileName = this.GetTfexePath();
            process.StartInfo.Arguments = arguments;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            process.Start();
        }
    }
}
