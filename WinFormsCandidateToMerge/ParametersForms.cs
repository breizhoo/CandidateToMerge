﻿using System;
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
    public partial class ParametersForms : DockContent
    {
        private readonly DataSetManipulator _dataSetManipulator;

        public ParametersForms(DataSetManipulator dataSetManipulator)
        {
            _dataSetManipulator = dataSetManipulator;
            InitializeComponent();

            CloseButtonVisible = false;

        }

        public void Initialise()
        {
            txtTfsUrl.Text = _dataSetManipulator.GetTfsUrl();            
        }

        private void txtTfsUrl_Leave(object sender, EventArgs e)
        {
            _dataSetManipulator.SetTfsUrl(txtTfsUrl.Text);
        }
    }
}
