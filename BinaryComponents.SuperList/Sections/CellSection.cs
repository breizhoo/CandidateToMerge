/////////////////////////////////////////////////////////////////////////////
//
// (c) 2007 BinaryComponents Ltd.  All Rights Reserved.
//
// http://www.binarycomponents.com/
//
/////////////////////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace BinaryComponents.SuperList.Sections
{
	public class CellSection : Section
	{
		public CellSection( ISectionHost host, HeaderColumnSection hcs, object item )
			: base( host )
		{
			_hcs = hcs;
			_item = item;
		}

		public override void Layout( Section.GraphicsSettings gs, Size maximumSize )
		{
			const int margin = 2;
			SizeF size = gs.Graphics.MeasureString( this.DisplayItem.ToString(), Font );
			this.Size = new Size( maximumSize.Width, (int) size.Height + margin * 2 );
		}

		private BinaryComponents.WinFormsUtility.Drawing.GdiPlusEx.Alignment GdiExAlignment
		{
			get
			{
				switch( _hcs.Column.Alignment )
				{
					case Alignment.Left:
						return BinaryComponents.WinFormsUtility.Drawing.GdiPlusEx.Alignment.Left;
					case Alignment.Center:
						return BinaryComponents.WinFormsUtility.Drawing.GdiPlusEx.Alignment.Center;
					case Alignment.Right:
						return BinaryComponents.WinFormsUtility.Drawing.GdiPlusEx.Alignment.Right;
					default:
						throw new NotSupportedException();
				}
			}
		}

		public override void Paint( Section.GraphicsSettings gs, System.Drawing.Rectangle clipRect )
		{
			Rectangle rcScrollAdjusted = this.HostBasedRectangle;
			Rectangle rc = new Rectangle( rcScrollAdjusted.X + 5,
				rcScrollAdjusted.Y + 2,
				rcScrollAdjusted.Width - 5,
				rcScrollAdjusted.Height - 2
				);

			Alignment alignment = _hcs.Column.Alignment;

			WinFormsUtility.Drawing.GdiPlusEx.DrawString
				( gs.Graphics, DisplayItem.ToString(), Font, (this.Host.FocusedSection == this.ListSection && RowSection.IsSelected) ? SystemColors.HighlightText : SystemColors.MenuText, rc
				, this.GdiExAlignment, WinFormsUtility.Drawing.GdiPlusEx.TextSplitting.SingleLineEllipsis, WinFormsUtility.Drawing.GdiPlusEx.Ampersands.Display );
		}

		protected virtual Font Font
		{
			get
			{
				return Host.Font;
			}
		}

		public HeaderColumnSection HeaderColumnSection
		{
			get
			{
				return _hcs;
			}
		}

		public object Item
		{
			get
			{
				return _item;
			}
		}

		protected object DisplayItem
		{
			get
			{
				return _hcs.Column.ColumnItemAccessor( _item );
			}
		}

		private RowSection RowSection
		{
			get
			{
				return (RowSection) this.Parent;
			}
		}

		private ListSection ListSection
		{
			get
			{
				return (ListSection) this.Parent.Parent;
			}
		}

		private HeaderColumnSection _hcs;
		private object _item;
	}
}
