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

namespace BinaryComponents.SuperList
{
	/// <summary>
	/// Delegate to convert an item into an object for rendering
	/// </summary>
	/// <param name="item"></param>
	/// <returns></returns>
	public delegate object ColumnItemValueAccessor( object rowItem );

	/// <summary>
	/// Alignment of the column text within the column header.
	/// </summary>
	public enum Alignment
	{
		Left,
		Center,
		Right
	}

	public class Column
	{
		/// <summary>
		/// Contructs a basic column
		/// </summary>
		/// <param name="name">Name of column, this can be used to identify the column across languages being used where caption will be different</param>
		/// <param name="caption">The caption used on the header.</param>
		/// <param name="width">Initial width of the column</param>
		public Column( string name, string caption, int width, ColumnItemValueAccessor columnItemValueAccessor )
		{
			if( name == null )
				throw new ArgumentNullException( "name" );
			if( caption == null )
				throw new ArgumentNullException( "caption" );
			if( width < 0 )
				throw new ArgumentOutOfRangeException( "width" );

			_name = name;
			_caption = caption;
			_width = width;
			_columnItemValueAccessor = columnItemValueAccessor;

			_comparitor = delegate( object x, object y )
			{
				object cellItemX = this.ColumnItemAccessor( x );
				object cellItemY = this.ColumnItemAccessor( y );
				IComparable comparer = cellItemX as IComparable;
				if( comparer == null )
				{
					string error = string.Format( "Cell item for column '{0}' doesn't support comparing. You will need to supply a comparer by setting Column.Comparitor", this.Name );
					throw new NotSupportedException( error );
				}
				return comparer.CompareTo( cellItemY );
			};
			_groupedComparitor = delegate( object x, object y )
			{
				if( x == y  )
					return 0;

				if( x != null && y != null )
				{
					if( this.GroupItemAccessor( x ) == this.GroupItemAccessor( y ) )
					{
						return 0;
					}
				}

				return _comparitor( x, y );
			};
		}

		/// <summary>
		/// Creates a copy of the given column
		/// </summary>
		/// <param name="columnToCopy"></param>
		public Column( Column columnToCopy )
		{
			_name = columnToCopy._name;
			_comparitor = columnToCopy._comparitor;
			_caption = columnToCopy._caption;
			_width = columnToCopy._width;
			_sortOrder = columnToCopy._sortOrder;
			_groupSortOrder = columnToCopy._groupSortOrder;
			_isVisible = columnToCopy._isVisible;
			_isGrouped = columnToCopy._isGrouped;
			_columnItemValueAccessor = columnToCopy._columnItemValueAccessor;
			_groupItemValueAccessor = columnToCopy._groupItemValueAccessor;
			_groupedComparitor = columnToCopy._groupedComparitor;
		}



		/// <summary>
		/// Comparitor for a column item. Note it may be called by
		/// seperate threads.
		/// </summary>
		/// <param name="listItem"></param>
		/// <returns></returns>
		public Comparison<object> Comparitor
		{
			get
			{
				return _comparitor;
			}
			set
			{
				_comparitor = value;
			}
		}

		/// <summary>
		/// Delegate to convert an item into a string given this column
		/// </summary>
		public ColumnItemValueAccessor ColumnItemAccessor
		{
			get
			{
				return _columnItemValueAccessor;
			}
			set
			{
				_columnItemValueAccessor = value;
			}
		}

		/// <summary>
		/// Comparitor for a grouped column item. Note it may be called by
		/// seperate threads. Default is to use Comparitor
		/// </summary>
		/// <param name="listItem"></param>
		/// <returns></returns>
		public Comparison<object> GroupedComparitor
		{
			get
			{
				return _groupedComparitor;
			}
			set
			{
				_groupedComparitor = value;
			}
		}

		/// <summary>
		/// Delegate that's called when the group value of an item is needed. Default behavour if not set is
		/// to call the ColumnItemAccessor.
		/// </summary>
		public ColumnItemValueAccessor GroupItemAccessor
		{
			get
			{
				return _groupItemValueAccessor == null ? _columnItemValueAccessor : _groupItemValueAccessor;
			}
			set
			{
				_groupItemValueAccessor = value;
			}
		}

		/// <summary>
		/// Style of behaviour when a column is dragged from the column header to the column group area
		/// </summary>
		public enum MoveToGroupBehaviour 
		{ 
			/// <summary>
			/// Copy the column from the header to the group area when dragged.
			/// </summary>
			Copy, 

			/// <summary>
			/// Moves the column from the header to the group area when dragged.
			/// </summary>
			Move 
		};

		/// <summary>
		/// When a column is dragged to the group area this property denotes what to do,
		/// either make a copy of the column or move it. The default behaviour is to move.
		/// </summary>
		public MoveToGroupBehaviour MoveBehaviour
		{
			get
			{
				return _moveBehaviour;
			}
			set
			{
				_moveBehaviour = value;
			}
		}

		/// <summary>
		/// The name of the column.
		/// </summary>
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				if( _name != value )
				{
					_name = value;
					OnDataChanged( WhatPropertyChanged.Name );
				}
			}
		}

		/// <summary>
		/// Current sort order of the column
		/// </summary>
		public System.Windows.Forms.SortOrder SortOrder
		{
			get
			{
				return _sortOrder;
			}
			set
			{
				if( _sortOrder != value )
				{
					_sortOrder = value;
					OnDataChanged( WhatPropertyChanged.SortOrder );
				}
			}
		}

		/// <summary>
		/// Grouped sort order of the column.
		/// </summary>
		public System.Windows.Forms.SortOrder GroupSortOrder
		{
			get
			{
				return _groupSortOrder;
			}
			set
			{
				if( _groupSortOrder != value )
				{
					if( value == System.Windows.Forms.SortOrder.None )
					{
						throw new ArgumentException( "A sort order of none is not allowed for grouping order" );
					}
					_groupSortOrder = value;
					OnDataChanged( WhatPropertyChanged.SortOrder );
				}
			}
		}


		/// <summary>
		/// Actual text used to display in the olumn
		/// </summary>
		public string Caption
		{
			get
			{
				return _caption;
			}
			set
			{
				if( _caption != value )
				{
					_caption = value;
					OnDataChanged( WhatPropertyChanged.Caption );
				}
			}
		}

		/// <summary>
		/// Width of the column
		/// </summary>
		public int Width
		{
			get
			{
				return _width;
			}
			set
			{
				if( _width != value )
				{
					_width = value;
					OnDataChanged( WhatPropertyChanged.Width );
				}
			}
		}

		/// <summary>
		/// Is the column visible on the control.
		/// </summary>
		public bool IsVisible
		{
			get
			{
				return _isVisible;
			}
			set
			{
				if( _isVisible != value )
				{
					_isVisible = value;
					OnDataChanged( WhatPropertyChanged.IsVisible );
				}
			}
		}

		/// <summary>
		/// Is the column part of grouping area within the control.
		/// </summary>
		public bool IsGrouped
		{
			get
			{
				return _isGrouped;
			}
			set
			{
				if( _isGrouped != value )
				{
					_isGrouped = value;
					OnDataChanged( WhatPropertyChanged.IsGroup );
				}
			}
		}
		
		/// <summary>
		/// Alignment of the column text within the column header.
		/// </summary>
		public Alignment Alignment
		{
			get
			{
				return _alignment;
			}
			set
			{
				_alignment = value;
			}
		}

		/// <summary>
		/// Indicates what property changed when the data changed event fired.
		/// </summary>
		public enum WhatPropertyChanged
		{
			IsGroup,
			IsVisible,
			Name,
			Caption,
			Width,
			SortOrder
		};

		/// <summary>
		/// Details of data changed event args
		/// </summary>
		public class ColumnDataChangedEventArgs : EventArgs
		{
			internal ColumnDataChangedEventArgs( Column column, WhatPropertyChanged whatChanged )
			{
				this.WhatChanged = whatChanged;
				this.Column = column;
			}

			/// <summary>
			/// The Column whos data changed
			/// </summary>
			public readonly Column Column;

			/// <summary>
			/// The property that caused the change event.
			/// </summary>
			public readonly WhatPropertyChanged WhatChanged;
		}

		/// <summary>
		/// Handler for Column data changed.
		/// </summary>
		public delegate void ColumnDataChangedHandler( object sender, ColumnDataChangedEventArgs eventArgs );

		/// <summary>
		/// Event that gets fired when something in the column changes.
		/// </summary>
		public event ColumnDataChangedHandler DataChanged;

		/// <summary>
		/// Called to fire a data changed event.
		/// </summary>
		/// <param name="whatChanged"></param>
		protected virtual void OnDataChanged( WhatPropertyChanged whatChanged )
		{
			if( DataChanged != null )
			{
				DataChanged( this, new ColumnDataChangedEventArgs( this, whatChanged ) );
			}
		}

		private MoveToGroupBehaviour _moveBehaviour = MoveToGroupBehaviour.Move;
		private string _name;
		private string _caption;
		private int _width;
		private bool _isGrouped = false;
		private bool _isVisible = true;
		private System.Windows.Forms.SortOrder _sortOrder = System.Windows.Forms.SortOrder.None;
		private System.Windows.Forms.SortOrder _groupSortOrder = System.Windows.Forms.SortOrder.Ascending;
		private ColumnItemValueAccessor _columnItemValueAccessor = null, _groupItemValueAccessor = null;
		private Comparison<object> _comparitor = null, _groupedComparitor = null;
		private Alignment _alignment = Alignment.Left;
	}
}
