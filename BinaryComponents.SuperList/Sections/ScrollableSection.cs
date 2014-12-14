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
using System.Windows.Forms;
using System.Text;

namespace BinaryComponents.SuperList.Sections
{
	public class ScrollableSection : SectionContainer
	{
		public ScrollableSection( ISectionHost host )
			: base( host )
		{
		}

		public override void Dispose()
		{
			base.Dispose();
			if( _vScrollbar != null )
			{
				this.Host.ControlCollection.Remove( _vScrollbar );
				_vScrollbar.Dispose();
				_vScrollbar = null;
			}
			if( _hScrollbar != null )
			{
				this.Host.ControlCollection.Remove( _hScrollbar );
				_hScrollbar.Dispose();
				_hScrollbar = null;
			}
		}

		public override Point GetScrollCoordinates()
		{
			Point pt = Point.Empty;
			if( this.HorizontalScrollbarVisible )
			{
				pt.X += this.HScrollbar.Value;
			}
			if( this.VerticalScrollbarVisible )
			{
				pt.Y += this.VScrollbar.Value;
			}
			return pt;
		}

		public void UpdateScrollInfo()
		{
			if( this.HorizontalScrollbarVisible )
			{
				this.SetHScrollInfo();
				this.PositionHorizontalScrollbar();
			}
			if( this.VerticalScrollbarVisible )
			{
				this.SetVScrollInfo();
				this.PositionVerticalScrollbar();
			}
		}

		protected bool HorizontalScrollbarVisible
		{
			get
			{
				return this.HScrollbar != null && this.HScrollbar.Visible;
			}
			set
			{
				if( value != this.HorizontalScrollbarVisible )
				{
					if( this.HScrollbar == null )
					{
						_hScrollbar = new NonSelectableHScrollBar();

						_hScrollbar.Visible = false;
						_hScrollbar.ValueChanged += new EventHandler( HScrollbar_ValueChanged );
						this.Host.ControlCollection.Add( this.HScrollbar );
					}
					if( value )
					{
						SetHScrollInfo();
						PositionHorizontalScrollbar();
					}
					this.HScrollbar.Visible = value;
				}
			}
		}

		protected bool VerticalScrollbarVisible
		{
			get
			{
				return this.VScrollbar != null && this.VScrollbar.Visible;
			}
			set
			{
				if( value != this.VerticalScrollbarVisible )
				{
					if( this.VScrollbar == null )
					{
						_vScrollbar = new NonSelectableVScrollBar();

						_vScrollbar.Visible = false;
						_vScrollbar.ValueChanged += new EventHandler( VScrollbar_ValueChanged );
						this.Host.ControlCollection.Add( this.VScrollbar );
					}
					if( value )
					{
						this.SetVScrollInfo();
						PositionVerticalScrollbar();
					}
					this.VScrollbar.Visible = value;
				}
			}
		}

		protected virtual void OnVScrollValueChanged( int value )
		{
			this.Invalidate();
		}

		protected virtual void SetVScrollInfo()
		{
		}
		protected virtual void PositionVerticalScrollbar()
		{
			Rectangle workingRectangle = this.WorkingRectangle;
			int hScrollSpacing = this.HorizontalScrollbarVisible ? this.HScrollbar.Bounds.Height : 0;
			this.VScrollbar.Bounds = new Rectangle(
				workingRectangle.Right,
				workingRectangle.Top,
				this.VScrollbar.Bounds.Width,
				workingRectangle.Height );
		}

		protected virtual void OnHScrollValueChanged( int value )
		{
			this.Invalidate();
		}
		protected virtual void SetHScrollInfo()
		{
		}
		protected virtual void PositionHorizontalScrollbar()
		{
			int vScrollSpacing = this.VerticalScrollbarVisible ? this.VScrollbar.Bounds.Width : 0;
			this.HScrollbar.Bounds = new Rectangle(
				this.Rectangle.X,
				this.Rectangle.Bottom - this.HScrollbar.Bounds.Height,
				this.Rectangle.Width - vScrollSpacing,
				this.HScrollbar.Bounds.Height
				);
		}
		protected Rectangle WorkingRectangle
		{
			get
			{
				Rectangle rc = this.Rectangle;
				if( this.VerticalScrollbarVisible )
				{
					rc.Width -= this.VScrollbar.Width;
				}
				if( this.HorizontalScrollbarVisible )
				{
					rc.Height -= this.HScrollbar.Height;
				}
				if( this.ExcludeFirstChildrenFromVScroll > 0 )
				{
					int bottom = 0;
					int count = this.ExcludeFirstChildrenFromVScroll;
					foreach( Section s in this.Children )
					{
						if( count-- == 0 )
							break;
						if( s.Rectangle.Bottom > bottom )
						{
							bottom = s.Rectangle.Bottom;
						}
					}
					int height = bottom - this.Rectangle.Top;
					rc.Y += height;
					rc.Height -= height;
				}
				return rc;
			}
		}

		public override void Paint( Section.GraphicsSettings gs, Rectangle clipRect )
		{
			for( int i = 0; i < this.ExcludeFirstChildrenFromVScroll; i++ )
			{
				this.Children[i].Paint( gs, clipRect );
			}

			GraphicsContainer container = gs.Graphics.BeginContainer();
			try
			{
				using( Region clipRegion = new Region( clipRect ) )
				{
					clipRegion.Intersect( this.WorkingRectangle );
					gs.Graphics.Clip = clipRegion;
					for( int i = this.ExcludeFirstChildrenFromVScroll; i < this.Children.Count; i++ )
					{
						Section s = this.Children[i];
						if( clipRect.IntersectsWith( s.HostBasedRectangle ) )
						{
							s.Paint( gs, clipRect );
						}
					}
				}
			}
			finally
			{
				gs.Graphics.EndContainer( container );
			}
		}

		public VScrollBar VScrollbar
		{
			get
			{
				return _vScrollbar;
			}
		}

		public HScrollBar HScrollbar
		{
			get
			{
				return _hScrollbar;
			}
		}

		private void VScrollbar_ValueChanged( object sender, EventArgs e )
		{
			this.OnVScrollValueChanged( _vScrollbar.Value );
		}

		private void HScrollbar_ValueChanged( object sender, EventArgs e )
		{
			this.OnHScrollValueChanged( _hScrollbar.Value );
		}

		protected int ExcludeFirstChildrenFromVScroll
		{
			get
			{
				return _excludeFirstChildrenFromVScroll;
			}
			set
			{
				_excludeFirstChildrenFromVScroll = value;
			}
		}

		#region NonSelectableHScrollBar

		internal sealed class NonSelectableHScrollBar : HScrollBar
		{
			internal NonSelectableHScrollBar()
			{
				SetStyle( ControlStyles.Selectable, false );
			}
		}

		#endregion
		#region NonSelectableVScrollBar

		internal sealed class NonSelectableVScrollBar : VScrollBar
		{
			internal NonSelectableVScrollBar()
			{
				SetStyle( ControlStyles.Selectable, false );
			}
		}

		#endregion

		private int _excludeFirstChildrenFromVScroll = 0;
		private NonSelectableVScrollBar _vScrollbar = null;
		private NonSelectableHScrollBar _hScrollbar = null;
	}
}
