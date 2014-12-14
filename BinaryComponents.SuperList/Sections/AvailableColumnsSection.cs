/////////////////////////////////////////////////////////////////////////////
//
// (c) 2007 BinaryComponents Ltd.  All Rights Reserved.
//
// http://www.binarycomponents.com/
//
/////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using BinaryComponents.Utility.Collections;
using System.Windows.Forms;

namespace BinaryComponents.SuperList.Sections
{
	public class AvailableColumnsSection : HeaderColumnSectionContainer
	{
		public AvailableColumnsSection( ISectionHost host, EventingList<Column> columns )
			: base( host, columns )
		{
			columns.DataChanged += new EventHandler<EventingList<Column>.EventInfo>( columns_DataChanged );
		}

		public override bool CanDrop( System.Windows.Forms.IDataObject dataObject )
		{
			return false;
		}

		public override bool AllowColumnsToBeDroppedInVoid
		{
			get
			{
				return false;
			}
		}

		public override bool ShouldRemoveColumnOnDrop( Column column )
		{
			return false;
		}

		public override void Layout( Section.GraphicsSettings gs, System.Drawing.Size maximumSize )
		{
			base.Layout( gs, maximumSize );
			this.Size = new System.Drawing.Size( this.Size.Width, this.Children.Count == 0 ? 0 : this.Children[this.Children.Count - 1].Rectangle.Bottom - this.Rectangle.Top );
		}

		protected override HeaderColumnSection CreateHeaderColumnSection( HeaderColumnSection.DisplayMode displayModeToCreate, Column column )
		{
			return this.Host.SectionFactory.CreateAvailableColumnSection( this.Host, column );
		}

		public class AvailableColumnSection : HeaderColumnSection
		{
			public AvailableColumnSection( ISectionHost host, Column column )
				: base( host, DisplayMode.Customise, column )
			{
			}

			public override void MouseUp( MouseEventArgs e )
			{
			}

			protected override System.Windows.Forms.TextFormatFlags GetTextFormatFlags()
			{
				return TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter | TextFormatFlags.EndEllipsis;
			}

			public override void Layout( GraphicsSettings gs, System.Drawing.Size maximumSize )
			{
				base.Layout( gs, maximumSize );
				this.Size = new System.Drawing.Size( maximumSize.Width, this.Size.Height );
			}

			protected override void DrawSortArrow( GraphicsSettings gs )
			{
				// do nothing.
			}
		}

		private void columns_DataChanged( object sender, EventingList<Column>.EventInfo e )
		{
			this.SyncSectionsToColumns( HeaderColumnSection.DisplayMode.Customise );
			this.Host.LazyLayout( null );
		}
	}
}
