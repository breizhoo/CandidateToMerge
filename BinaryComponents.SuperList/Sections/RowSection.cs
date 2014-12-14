/////////////////////////////////////////////////////////////////////////////
//
// (c) 2007 BinaryComponents Ltd.  All Rights Reserved.
//
// http://www.binarycomponents.com/
//
/////////////////////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Text;

namespace BinaryComponents.SuperList.Sections
{
	public class RowSection: SectionContainer
	{
		public RowSection( ListControl listControl, RowIdentifier rowIdentifier, HeaderSection headerSection, int position )
			: base( listControl )
		{
			_headerSection = headerSection;
			_rowIdentifier = rowIdentifier;
			_position = position;
		}

		public Object Item
		{
			get
			{
				return _rowIdentifier.Items[0];
			}
		}

		public int Position
		{
			get
			{
				return _position;
			}
		}

		public  RowIdentifier RowIdentifier
		{
			get
			{
				return _rowIdentifier;
			}
		}

		public HeaderSection HeaderSection
		{
			get
			{
				return _headerSection;
			}
		}

		public override void Layout( Section.GraphicsSettings gs, System.Drawing.Size maximumSize )
		{
			this.DeleteChildren();
			
			int bottom = this.Location.Y;

			foreach( Section s in _headerSection.Children )
			{
				HeaderColumnSection hcs = s as HeaderColumnSection;

				if( hcs != null )
				{
					CellSection cellSection = this.Host.SectionFactory.CreateCellSection( this.Host, hcs, this.Item );

					this.Children.Add( cellSection );

					//
					//	We position the cell aligned to its corresponding HeaderColumnSection. We nudge it to right here
					//	based on any difference between the column size and the headers columns actual size. The only time there
					//	will be a different currently is when we have multiple groups reserving initial space on the first column.
					cellSection.Location = new Point( hcs.Location.X + hcs.Rectangle.Width - hcs.Column.Width, this.Location.Y );
					cellSection.Layout( gs, new Size( hcs.Column.Width, maximumSize.Height ) );
					bottom = Math.Max( bottom, cellSection.Rectangle.Bottom );
				}
			}
			this.Size = new Size( this.HeaderSection.Rectangle.Width, bottom - this.Rectangle.Top );
		}

		public void PaintSelection( Section.GraphicsSettings gs )
		{
			if( this.IsSelected )
			{
				gs.Graphics.FillRectangle( this.Host.FocusedSection == this.ListSection ? SystemBrushes.Highlight : SystemBrushes.ButtonFace, this.Rectangle );
			}
		}

		public override void PaintBackground( Section.GraphicsSettings gs, Rectangle clipRect )
		{
			PaintSelection( gs );
			_drawnSelected = this.IsSelected;

			if( this.IsFocused )
			{
				Rectangle rc = this.HostBasedRectangle;
				int indent = this.IndentWidth;
				Rectangle focusRect = new Rectangle( rc.X + indent, rc.Y, rc.Width - indent, this.Rectangle.Height );

				focusRect.Width -= 1;
				focusRect.Height -= 1;

				using( Pen pen = new Pen( SystemColors.ControlDark ) )
				{
					pen.DashStyle = DashStyle.Dot;
					gs.Graphics.DrawRectangle( pen, focusRect );
				}
			}
		}

		public bool DrawnSelected
		{
			get
			{
				return _drawnSelected;
			}
		}

		public bool IsSelected
		{
			get
			{
				return this.ListSection.SelectedItems.IsSelected( this.RowIdentifier );
			}
		}

		public bool IsFocused
		{
			get
			{
				return this.ListSection.HasFocus( this.RowIdentifier );
			}
		}

		protected virtual void PaintSeparatorLine( Graphics g, Rectangle rc )
		{
			using( Pen pen = new Pen( Color.FromArgb( 70, 123, 164, 224 ), _separatorLineHeight ) )
			{
				g.DrawLine( pen, new Point( rc.Left, rc.Bottom - _separatorLineHeight ), new Point( rc.Right, rc.Bottom - _separatorLineHeight ) );
			}
		}

		public override void Paint( GraphicsSettings gs, System.Drawing.Rectangle clipRect )
		{
			//
			// Fill indent area
			if( this.Children.Count > 0 )
			{
				Rectangle rc = this.HostBasedRectangle;
				Rectangle rcIndent = new Rectangle( rc.X, this.Rectangle.Y, this.Children[0].HostBasedRectangle.X - rc.X, rc.Height );

				PaintIndentArea( gs.Graphics, rcIndent );
			}

			base.Paint( gs, clipRect );

			Rectangle rcLine = this.Rectangle;
			rcLine.X += this.IndentWidth;
			PaintSeparatorLine( gs.Graphics, rcLine );
		}

		protected virtual int IndentWidth
		{
			get
			{
				return this.Children.Count > 0 ? this.Children[0].Rectangle.X - this.Rectangle.X : 0;
			}
		}

		protected ListSection ListSection
		{
			get
			{
				return (ListSection)this.Parent;
			}
		}

		protected ListControl ListControl
		{
			get
			{
				return (ListControl)this.Host;
			}
		}

		protected virtual void PaintIndentArea( Graphics g, Rectangle rcIndent )
		{
			g.FillRectangle( Brushes.LightGoldenrodYellow, rcIndent );
		}

		const int _separatorLineHeight = 1;
		private int _position;
		private RowIdentifier _rowIdentifier;
		private HeaderSection _headerSection;
		private bool _drawnSelected = false;
	}
}
