using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BootStrapper;

namespace WinFormsCandidateToMerge
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var ioc = new Ioc();
            ioc.Initialization();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mainForms = ioc.GetInstance<CandidateToMerge>();
            Application.Run((Form)mainForms);
        }
    }
}
