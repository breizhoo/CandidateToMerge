/////////////////////////////////////////////////////////////////////////////
//
// (c) 2007 BinaryComponents Ltd.  All Rights Reserved.
//
// http://www.binarycomponents.com/
//
/////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BinaryComponents.SuperList;

namespace BinaryComponents.SuperList.Helper
{
	public partial class AvailableSectionsForm : Form
	{
		public AvailableSectionsForm()
		{
			InitializeComponent();
			_availableSectionsControl.SizeChanged += new EventHandler( _availableSectionsControl_SizeChanged );
		}

		private bool _firstSize = true;

		void _availableSectionsControl_SizeChanged( object sender, EventArgs e )
		{
			_panel.AutoScrollMinSize = new Size( 0, _availableSectionsControl.Height );
			if( _firstSize )
			{
				int min = 130;
				int max = this.Size.Height;
				int newHeight = this.Size.Height + _availableSectionsControl.Height - _panel.Size.Height;
				if( newHeight > min && newHeight < max )
				{
					this.Size = new Size( this.Size.Width, newHeight );
				}
				_firstSize = false;
			}
		}

		protected override void OnHandleCreated( EventArgs e )
		{
			base.OnHandleCreated( e );

			if( _currentForm != null && _currentForm.IsHandleCreated )
			{
				_currentForm.Close();
			}
			_currentForm = this;
		}

		protected override void OnHandleDestroyed( EventArgs e )
		{
			base.OnHandleDestroyed( e );
			if( _currentForm == this )
			{
				_currentForm = null;
			}
		}

		public ColumnList ColumnList
		{
			get
			{
				return _availableSectionsControl.ColumnList;
			}
			set
			{
				_availableSectionsControl.ColumnList = value;
			}
		}

		private static AvailableSectionsForm _currentForm = null;
	}
}