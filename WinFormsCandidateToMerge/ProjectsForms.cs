using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace WinFormsCandidateToMerge
{
    public partial class ProjectsForms : DockContent
    {
        private readonly DataSetManipulator _dataSetManipulator;

        public ProjectsForms(DataSetManipulator dataSetManipulator)
        {
            _dataSetManipulator = dataSetManipulator;
            InitializeComponent();
            CloseButtonVisible = false;
        }

        public void Initialise()
        {
            dsCandidateToMerge2.DataSource = _dataSetManipulator.DsCandidateToMerge;
        }

    }
}
