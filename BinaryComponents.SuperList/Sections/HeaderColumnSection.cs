/////////////////////////////////////////////////////////////////////////////
//
// (c) 2007 BinaryComponents Ltd.  All Rights Reserved.
//
// http://www.binarycomponents.com/
//
/////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Text;
using BinaryComponents.Utility.Collections;
using System.Windows.Forms.VisualStyles;


namespace BinaryComponents.SuperList.Sections
{
	public class HeaderColumnSection : Section
	{
		public enum DisplayMode { Header, Customise };
		public HeaderColumnSection( ISectionHost host, DisplayMode displayMode, Column column )
			: base( host )
		{
			System.Diagnostics.Debug.Assert( column != null );
			_displayMode = displayMode;
			_column = column;
			column.DataChanged += new Column.ColumnDataChangedHandler( column_DataChanged );
		}

		public override void Dispose()
		{
			base.Dispose();
			if( _column != null )
			{
				_column.DataChanged -= new Column.ColumnDataChangedHandler( column_DataChanged );
				_column = null;
			}
		}

		public override bool CanDropInVoid
		{
			get
			{
				return this.Parent.AllowColumnsToBeDroppedInVoid;
			}
		}

		public Column Column
		{
			get
			{
				return _column;
			}
		}

		public new HeaderColumnSectionContainer Parent
		{
			get
			{
				return (HeaderColumnSectionContainer)base.Parent;
			}
		}

		public override bool CanDrag
		{
			get
			{
				return true;
			}
		}

		public DisplayMode Mode
		{
			get
			{
				return _displayMode;
			}
		}

		public override void DroppedInVoid()
		{
			this.Parent.Columns.Remove( this.Column );
		}

		public override void Paint( GraphicsSettings gs, Rectangle clipRect )
		{
			using( SolidBrush brush = new SolidBrush( this.Host.TextColor ) )
			{
				Rectangle rc = this.Rectangle;
				int offset = 0;

				if( this.Parent.LayoutController != null )
				{
					offset = this.Parent.LayoutController.CurrentHorizontalScrollPosition;
					rc.X -= offset;
				}

				if( _displayMode == DisplayMode.Header )
				{
					if( VisualStyleRenderer.IsSupported )
					{
						VisualStyleRenderer renderer = GetRenderer();

						renderer.DrawBackground( gs.Graphics, rc );
					}
					else
					{
						gs.Graphics.FillRectangle( SystemBrushes.Control, rc );

						if( _leftMouseButtonPressed )
						{
							ControlPaint.DrawBorder3D( gs.Graphics, rc, Border3DStyle.Sunken );
						}
						else
						{
							ControlPaint.DrawBorder3D( gs.Graphics, rc, Border3DStyle.Raised );
						}
					}
				}
				else
				{
					DrawBox( gs.Graphics, rc );
				}

				const int textMargin = 2;

				rc.X += textMargin;
				rc.Width -= textMargin;

				if( this.Parent.LayoutController != null )
				{
					rc.Width -= _arrowWidth + _arrowSpaceXMargin * 2;
				}

				rc.Y -= 2;

				DrawCaption( gs, rc );

				DrawSortArrow( gs );
			}
		}

		protected virtual void DrawCaption( GraphicsSettings gs, Rectangle rc )
		{
			Helper.TextRendererEx.DrawText( gs.Graphics,
				_column.Caption,
				SystemFonts.MenuFont,
				rc,
				Color.Black,
				this.GetTextFormatFlags() );
		}

		protected virtual void DrawBox( Graphics g, Rectangle rc )
		{
			ControlPaint.DrawBorder3D( g, rc.Left, rc.Top, rc.Width, rc.Height - 1, Border3DStyle.RaisedInner, Border3DSide.All );
		}

		protected virtual void DrawSortArrow( GraphicsSettings gs  )
		{
			int offset = 0;

			if( this.Parent.LayoutController != null )
			{
				offset = this.Parent.LayoutController.CurrentHorizontalScrollPosition;
			}
			//
			// Draw sort arrows
			const int halfArrowWidth = _arrowWidth / 2;
			int right = this.Rectangle.Right - offset;
			Rectangle rcArrow = new Rectangle( right - _arrowWidth - _arrowSpaceXMargin * 2,
				this.Rectangle.Y + (this.Rectangle.Height - _arrowWidth) / 2,
				_arrowWidth,
				_arrowWidth
			);

			SortOrder sortOrder = this.Parent.GetColumnSortOrder( this.Column );

			switch( sortOrder )
			{
				case SortOrder.Ascending:
					{
						int xTop = rcArrow.Left + rcArrow.Width / 2 + 1;
						int yTop = rcArrow.Top + (rcArrow.Height - halfArrowWidth) / 2 - 1;
						int xLeft = xTop - halfArrowWidth;
						int yLeft = yTop + halfArrowWidth + 1;
						int xRight = xTop + halfArrowWidth + 1;
						int yRight = yTop + halfArrowWidth + 1;

						gs.Graphics.FillPolygon( SystemBrushes.ControlDark, new Point[]
							{
								new Point( xTop, yTop ), 
								new Point( xLeft, yLeft ), 
								new Point ( xRight, yRight )
							} );
					}
					break;
				case SortOrder.Descending:
					{
						int xBottom = rcArrow.Left + rcArrow.Width / 2 + 1;
						int xLeft = xBottom - halfArrowWidth + 1;
						int yLeft = rcArrow.Top + (rcArrow.Height - halfArrowWidth) / 2;
						int xRight = xBottom + halfArrowWidth;
						int yRight = rcArrow.Top + (rcArrow.Height - halfArrowWidth) / 2;
						int yBottom = yRight + halfArrowWidth;

						gs.Graphics.FillPolygon( SystemBrushes.ControlDark, new Point[]
							{
								new Point( xLeft, yLeft ), 
								new Point( xBottom, yBottom ), 
								new Point ( xRight, yRight )
							} );
					}
					break;
			}
		}

		protected virtual TextFormatFlags GetTextFormatFlags()
		{
			return TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.EndEllipsis;
		}

		public override void Layout( GraphicsSettings gs, Size maximumSize )
		{
			const int widthPadding = 10;
			int headerWidth;
			int height;

			if( VisualStyleRenderer.IsSupported )
			{
				VisualStyleRenderer renderer = GetRenderer();

				height = renderer.GetPartSize( gs.Graphics, ThemeSizeType.True ).Height;
			}
			else
			{
				height = SystemFonts.DialogFont.Height + 6;
			}
			if( height > maximumSize.Height )
			{
				height = maximumSize.Height;
			}

			switch( _displayMode )
			{
				case DisplayMode.Header:
					headerWidth = _column.Width;
					break;
				case DisplayMode.Customise:
					headerWidth = TextRenderer.MeasureText( _column.Caption, SystemFonts.MenuFont ).Width + widthPadding + _arrowWidth + _arrowSpaceXMargin * 2;
					break;
				default:
					throw new NotSupportedException();
			}

			this.Size = new Size( headerWidth, height );
		}

		public override void MouseEnter()
		{
			base.MouseEnter();
			this.Invalidate();
		}

		public override void MouseLeave()
		{
			this.Host.Cursor = Cursors.Default;
			base.MouseLeave();
			this.Invalidate();
		}

		public override void MouseDown( MouseEventArgs e )
		{
			base.MouseDown( e );

			if( e.Button == MouseButtons.Left )
			{
				_leftMouseButtonPressed = true;
			}
			this.Invalidate();
		}

		private bool PointInChangeWidthHotSpot( Point pt )
		{
			Rectangle rc = this.HostBasedRectangle;

			rc.X = rc.Right - _hotSpotWidth / 2;
			rc.Width = _hotSpotWidth;
			
			return rc.Contains( pt );
		}

		public override void KeyDown( KeyEventArgs e )
		{
			base.KeyDown( e );

			if( e.KeyCode == Keys.Escape && this.Host.SectionWithMouseCapture == this )
			{
				CancelMouseCapture();
			}
		}

		public override Section SectionFromPoint( Point pt )
		{
			Rectangle rc = this.Rectangle;

			if( _displayMode == DisplayMode.Header )
			{
				rc.Width += _hotSpotWidth / 2;
			}
			if( rc.Contains( pt ) )
			{
				return this;
			}
			return null;
		}

		public override void MouseMove( Point pt, MouseEventArgs e )
		{
			if( _displayMode == DisplayMode.Header && ( this.Host.SectionWithMouseCapture == this || this.PointInChangeWidthHotSpot( pt ) ) )
			{
				this.Host.Cursor = Cursors.VSplit;
				if( this.Host.SectionWithMouseCapture == this )
				{
					int newWidth = pt.X - this.HostBasedRectangle.Left;

					if( newWidth < _hotSpotWidth / 2 )
					{
						newWidth = _hotSpotWidth / 2;
					}
					this.Column.Width = newWidth;
				}
				else if( e.Button == MouseButtons.Left )
				{
					_oldWidth = this.Column.Width;
					this.Host.StartMouseCapture( this );
				}
			}
			else
			{
				this.Host.Cursor = Cursors.Default;
				base.MouseMove( pt, e );
			}
		}

		public override void MouseUp( MouseEventArgs e )
		{
			base.MouseUp( e );

			if( !this.Host.IsInDragOperation )
			{
				if( this.Host.SectionWithMouseCapture == this )
				{
					this.Host.EndMouseCapture();
				}
				else
				{
					if( _leftMouseButtonPressed )
					{
						switch( this.Parent.GetColumnSortOrder( this.Column ) )
						{
							case SortOrder.Ascending:
								this.Parent.SetColumnSortOrder( this.Column, SortOrder.Descending );
								break;

							case SortOrder.None:
							case SortOrder.Descending:
								this.Parent.SetColumnSortOrder( this.Column, SortOrder.Ascending );
								break;
						}
					}
				}
			}
			_leftMouseButtonPressed = false;
			this.Invalidate();
		}

		public override void CancelMouseCapture()
		{
			_leftMouseButtonPressed = false;
			this.Host.Cursor = Cursors.Default;
			base.CancelMouseCapture();
			this.Column.Width = _oldWidth;
			this.Host.EndMouseCapture();
		}

		private VisualStyleRenderer GetRenderer()
		{
			VisualStyleElement item;

			if( _leftMouseButtonPressed )
			{
				item = VisualStyleElement.Header.Item.Pressed;
			}
			else
			{
				if( this.Host.SectionMouseOver == this )
				{
					item = VisualStyleElement.Header.Item.Hot;
				}
				else
				{
					item = VisualStyleElement.Header.Item.Normal;
				}
			}

			VisualStyleRenderer renderer = new VisualStyleRenderer( item );

			return renderer;
		}

		private void column_DataChanged( object sender, Column.ColumnDataChangedEventArgs eventArgs )
		{
			if( eventArgs.WhatChanged == Column.WhatPropertyChanged.Width )
			{
				this.Host.LazyLayout( null );
			}
		}

		private bool _leftMouseButtonPressed = false;
		private int _oldWidth;
		private const int _arrowSpaceXMargin = 2;
		private const int _arrowWidth = 10;
		private DisplayMode _displayMode;
		private Column _column;
		private const int _hotSpotWidth = 20;
	}

}
