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

namespace BinaryComponents.SuperList.Sections
{
	public class SectionFactory
	{
		public virtual ListSection CreateListSection( ListControl listControl )
		{
			return new ListSection( listControl );
		}

		public virtual HeaderSection CreateHeaderSection( ISectionHost hostControl, EventingList<Column> columns )
		{
			return new HeaderSection( hostControl, columns  );
		}

		public virtual HeaderColumnSection CreateHeaderColumnSection( ISectionHost host, HeaderColumnSection.DisplayMode displayMode, Column column )
		{
			return new HeaderColumnSection( host, displayMode, column );
		}

		public virtual GroupSection CreateGroupSection( ListControl listControl, RowIdentifier ri, HeaderSection headerSecton, int position, int groupIndentWidth )
		{
			return new GroupSection( listControl, ri, headerSecton, position, groupIndentWidth );
		}

		public virtual RowSection CreateRowSection( ListControl listControl, RowIdentifier rowIdenifier, HeaderSection headerSection, int position )
		{
			return new RowSection( listControl, rowIdenifier, headerSection, position );
		}

		public virtual CellSection CreateCellSection( ISectionHost host, HeaderColumnSection hcs, object item )
		{
			return new CellSection( host, hcs, item  );
		}

		public virtual Section CreateCustomiseListSection( ListControl listControl )
		{
			return new CustomiseListSection( listControl );
		}

		public virtual Section CreateCustomiseGroupingSection( ISectionHost hostControl, EventingList<Column> columns )
		{
			return new CustomiseGroupingSection( hostControl, columns );
		}

		public virtual OptionsToolbarSection CreateOptionsToolbarSection( ListControl listControl )
		{
			return new ToolStripOptionsToolbarSection( listControl );
		}

		public virtual HeaderColumnSection CreateAvailableColumnSection( ISectionHost host, Column column )
		{
			return new AvailableColumnsSection.AvailableColumnSection( host, column );
		}
	}
}
