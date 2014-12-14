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
using System.Runtime.InteropServices;

namespace BinaryComponents.SuperList.Helper
{
	public partial class ImageWindow : Form
	{
		public ImageWindow( Icon iconToDraw )
		{
			if( iconToDraw == null )
				throw new ArgumentNullException( "iconToDraw" );

			this.Visible = false;
			this.WindowState = FormWindowState.Normal;

			InitializeComponent();
			this.TransparencyKey = this.BackColor;

			_iconToDraw = iconToDraw;
			this.Size = new Size( _iconToDraw.Width, _iconToDraw.Height );
		}


		protected override void OnHandleDestroyed( EventArgs e )
		{
			base.OnHandleDestroyed( e );
		}


		protected override void OnSizeChanged( EventArgs e )
		{
			base.OnSizeChanged( e );
			if( _iconToDraw != null )
			{
				this.Size = new Size( _iconToDraw.Width, _iconToDraw.Height );
			}
		}

		protected override void OnVisibleChanged( EventArgs e )
		{
			if( this.Visible )
			{
				//
				//	We call SetWindowPos here rather than just setting the TopMost property
				//	as the property doesn't take into account Form.ShowWithoutActivation.
				IntPtr HWND_TOPMOST = new IntPtr( -1 );

				Utility.Win32.User.SetWindowPos
					( Handle, HWND_TOPMOST, 0, 0, 0, 0
					, Utility.Win32.SetWindowPosOptions.SWP_NOSIZE | Utility.Win32.SetWindowPosOptions.SWP_NOMOVE
					| Utility.Win32.SetWindowPosOptions.SWP_NOREDRAW | Utility.Win32.SetWindowPosOptions.SWP_NOACTIVATE );
			}
		}

		protected override void WndProc( ref Message m )
		{
			switch( (Utility.Win32.Messages)m.Msg )
			{
				case Utility.Win32.Messages.WM_NCHITTEST:
					{
						m.Result = new IntPtr( (int)BinaryComponents.Utility.Win32.NCHITTEST.HTTRANSPARENT );
					}
					return;
			}
			base.WndProc( ref m );
		}

		protected override void OnPaint( PaintEventArgs e )
		{
			base.OnPaint( e );

			if( _iconToDraw != null )
			{
				e.Graphics.DrawIcon( _iconToDraw, this.ClientRectangle );
			}
		}

		protected override bool ShowWithoutActivation
		{
			get
			{
				return true;
			}
		}

		private Icon _iconToDraw = null;
	}
}