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
	public class CustomiseListSection: SectionContainer
	{
		public CustomiseListSection( ListControl listControl )
			: base( listControl )
		{
			_customiseGroupingSection = listControl.SectionFactory.CreateCustomiseGroupingSection( listControl, listControl.Columns.GroupedItems );
			_optionsToolbarSection = listControl.SectionFactory.CreateOptionsToolbarSection( listControl );
			this.Children.Add( _customiseGroupingSection );
			this.Children.Add( _optionsToolbarSection );
		}

		public override void Layout( Section.GraphicsSettings gs, System.Drawing.Size maximumSize )
		{
			CustomiseGroupingSection groupSection = (CustomiseGroupingSection) _customiseGroupingSection;
			OptionsToolbarSection optionsSection = (OptionsToolbarSection) _optionsToolbarSection;
			ListSection listSection = this.ListSection;

			groupSection.Location = this.Location;
			groupSection.Layout( gs, maximumSize );

			optionsSection.Layout( gs,
				new System.Drawing.Size( Math.Max( optionsSection.MinimumWidth, maximumSize.Width - groupSection.MinimumWidth ),
				groupSection.Size.Height ) );

			groupSection.Size = new System.Drawing.Size( maximumSize.Width - optionsSection.Size.Width, groupSection.Size.Height );
			optionsSection.Location = new Point( groupSection.Rectangle.Right, this.Location.Y );
			optionsSection.Visible = true;
			
			Size = new System.Drawing.Size( maximumSize.Width, optionsSection.Size.Height );
		}
		
		public override void PaintBackground( Section.GraphicsSettings gs, Rectangle clipRect )
		{
			gs.Graphics.FillRectangle( SystemBrushes.ControlDark, this.Rectangle );
			base.PaintBackground( gs, clipRect );
		}		

		public ListSection ListSection
		{
			get
			{
				return ((ListControl)this.Host).ListSection;
			}
		}

		private Section _customiseGroupingSection;
		private Section _optionsToolbarSection;
	}
}
