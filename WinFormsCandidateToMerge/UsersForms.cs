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
    public partial class UsersForms : DockContent
    {
        private readonly DataSetManipulator _dataSetManipulator;

        public UsersForms(DataSetManipulator dataSetManipulator)
        {
            _dataSetManipulator = dataSetManipulator;

            InitializeComponent();
            dsCandidateToMerge2.DataSource = dataSetManipulator.DsCandidateToMerge;
            CloseButtonVisible = false;

        }

        public void Initialise()
        {

        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
