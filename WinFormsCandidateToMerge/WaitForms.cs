using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsCandidateToMerge
{
    public partial class WaitForms : Form
    {
        public WaitForms()
        {
            InitializeComponent();

            progressBar1.Maximum = 100;
            progressBar1.Minimum = 0;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
