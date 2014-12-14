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
	public class CustomiseGroupingSection: HeaderColumnSectionContainer
	{
		public CustomiseGroupingSection( ISectionHost hostControl, EventingList<Column> columns )
			: base( hostControl, columns )
		{
			this.Columns.DataChanged += new EventHandler<EventingList<Column>.EventInfo>( GroupedItems_DataChanged );
		}

		public override void Layout( Section.GraphicsSettings gs, System.Drawing.Size maximumSize )
		{
			this.SyncSectionsToColumns( HeaderColumnSection.DisplayMode.Customise );

			const int verticalMargin = 5;
			const int columnItemSeparation = 7;

			Point pt = new Point( this.Location.X + columnItemSeparation, this.Location.Y + verticalMargin );
			int bottom = 0;
			if( this.Children.Count == 0 )
			{
				SizeF messageSize = gs.Graphics.MeasureString( _emptyGroupMessage, this.Host.Font );
				_minimumWidth = (int)messageSize.Width + _paintEmptyMessageMargin * 2;
			}
			else
			{
				foreach( HeaderColumnSection hcs in this.Children )
				{
					hcs.Location = pt;
					hcs.Layout( gs, maximumSize );
					pt.X += hcs.Size.Width + columnItemSeparation;
					pt.Y += hcs.Size.Height / 2;
					bottom = hcs.Rectangle.Bottom;
					_minimumWidth = hcs.Rectangle.Right - this.Rectangle.Left;
				}
			}
			bottom += verticalMargin;
			this.Size = new System.Drawing.Size( maximumSize.Width, Math.Max( 30, bottom ) );
		}

		public int MinimumWidth
		{
			get
			{
				return _minimumWidth;
			}
		}

		public override SortOrder GetColumnSortOrder( Column column )
		{
			return column.GroupSortOrder;
		}
		public override void SetColumnSortOrder( Column column, SortOrder sortOrder )
		{
			column.GroupSortOrder = sortOrder;
		}
		
		public override bool ShouldRemoveColumnOnDrop( Column column )
		{
			return true;
		}		


		public override void Paint( Section.GraphicsSettings gs, Rectangle clipRect )
		{
			base.Paint( gs, clipRect );

			if( this.Children.Count == 0 )
			{
				SizeF messageSize = gs.Graphics.MeasureString( _emptyGroupMessage, this.Host.Font );
				RectangleF rcDraw = new RectangleF( 0, 0, messageSize.Width + _paintEmptyMessageMargin, messageSize.Height + _paintEmptyMessageMargin );

				rcDraw.Offset( 5, (this.Rectangle.Height - rcDraw.Height) / 2 );

				StringFormat sf = new StringFormat( gs.DefaultStringFormat );
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment = StringAlignment.Center;

				gs.Graphics.FillRectangle( SystemBrushes.ButtonFace, rcDraw );
				gs.Graphics.DrawString( _emptyGroupMessage, this.Host.Font, Brushes.Gray, rcDraw, sf ); 
			}
			else
			{
				for( int i = 1; i < this.Children.Count; i++ )
				{
					Rectangle rcPrior = this.Children[i - 1].Rectangle;
					Rectangle rcHcs = this.Children[i].Rectangle;

					Point[] linkPoints = new Point[]{
					new Point( rcPrior.Right - 4, rcPrior.Bottom - 2 ),
					new Point( rcPrior.Right - 4, rcHcs.Top + rcHcs.Height / 2  ),
					new Point( rcHcs.Left - 1, rcHcs.Top + rcHcs.Height / 2  ),
				};
					gs.Graphics.DrawLines( Pens.Black, linkPoints );
				}
			}
		}

		private void GroupedItems_DataChanged( object sender, EventingList<Column>.EventInfo e )
		{
			this.Host.LazyLayout( null );
		}

		private string _emptyGroupMessage = "Drag a column header here to group by that column";
		private int _paintEmptyMessageMargin = 4;
		private int _minimumWidth;
	}
}
