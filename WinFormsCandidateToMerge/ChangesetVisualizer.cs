using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace WinFormsCandidateToMerge
{
    public class ChangesetVisualizer
    {
        public bool IsVisualizerAvailable { get; private set; }
        private string _currentTfPath;

        public ChangesetVisualizer()
        {
            IsVisualizerAvailable = File.Exists(this.GetTfexePath());
        }

        string GetTfexePath()
        {
            var programFilesPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            var possibleTfExePath = new List<String>
            {
                @"Microsoft Visual Studio 13.0\Common7\IDE\tf.exe",
                @"Microsoft Visual Studio 12.0\Common7\IDE\tf.exe",
                @"Microsoft Visual Studio 11.0\Common7\IDE\tf.exe",

            };

            foreach (var tfExePath in possibleTfExePath)
            {
                var tfFullPath = Path.Combine(programFilesPath, tfExePath);
                if (File.Exists(tfFullPath))
                {
                    _currentTfPath = tfFullPath;
                    return tfFullPath;
                }
            }

            return null;
        }

        public void Execute(int changesetId)
        {
            var arguments = string.Format("changeset {0}", changesetId);

            var process = new Process();
            process.StartInfo.FileName = _currentTfPath;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            process.Start();
        }
    }
}
