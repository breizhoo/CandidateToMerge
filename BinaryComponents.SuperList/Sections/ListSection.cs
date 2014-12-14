/////////////////////////////////////////////////////////////////////////////
//
// (c) 2007 BinaryComponents Ltd.  All Rights Reserved.
//
// http://www.binarycomponents.com/
//
/////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using BinaryComponents.Utility.Collections;
using System.Xml;

namespace BinaryComponents.SuperList.Sections
{
	public class ListSection : ScrollableSection, HeaderSection.ILayoutController
	{
		public ListSection( ListControl listControl )
			: base( listControl )
		{
			_headerSection = this.Host.SectionFactory.CreateHeaderSection( listControl, listControl.Columns.VisibleItems );
			_headerSection.LayoutController = this;
			listControl.Columns.GroupedItems.DataChanged += new EventHandler<EventingList<Column>.EventInfo>( GroupedItems_DataChanged );
			this.Host.FocusedSection = this; // we handle row focus manually from here.
			this.Children.Add( _headerSection );
			ExcludeFirstChildrenFromVScroll = 1;
			listControl.SelectedItems.DataChanged += new SelectedItemsCollection.DataChangedHandler( SelectedItems_DataChanged );
		}

		void SelectedItems_DataChanged( object sender, SelectedItemsChangedEventArgs e )
		{
			foreach( Section s in this.Children )
			{
				RowSection rs = s as RowSection;
				if( rs != null )
				{
					if( this.SelectedItems.Contains( rs.RowIdentifier ) != rs.DrawnSelected )
					{
						rs.Invalidate();
					}
				}
			}
		}

		public bool AllowMultiSelect
		{
			get
			{
				return _allowMultiSelect;
			}
			set
			{
				_allowMultiSelect = value;
			}
		}




		public void SerializeState( System.IO.TextWriter writer )
		{
			Helper.SerializationState ss = new BinaryComponents.SuperList.Helper.SerializationState();
			List<Helper.SerializationState.ColumnState> columnStates = new List<Helper.SerializationState.ColumnState>();

			foreach( Column c in this.Columns )
			{
				Helper.SerializationState.ColumnState columnState = new Helper.SerializationState.ColumnState();

				columnState.Name = c.Name;
				columnState.SortOrder = c.SortOrder;
				columnState.GroupSortOrder = c.GroupSortOrder;
				columnState.VisibleIndex = this.Columns.VisibleItems.IndexOf( c );
				columnState.GroupedIndex = this.Columns.GroupedItems.IndexOf( c );
				columnState.Width = c.Width;
				columnStates.Add( columnState );
			}
			ss.ColumnStates = columnStates.ToArray();

			List<Helper.SerializationState.GroupInstance> groupStates = new List<Helper.SerializationState.GroupInstance>();

			ss.GlobalGroupState = _globalGroupState == GroupState.Collapsed ? Helper.SerializationState.GroupState.GroupCollapsed : Helper.SerializationState.GroupState.GroupExpanded;

			foreach( GroupIdentifier gi in _groupExpansionState )
			{
				Helper.SerializationState.GroupInstance groupInstance = new Helper.SerializationState.GroupInstance();

				groupInstance.GroupPath = gi.GroupValues;
				groupInstance.GroupName = gi.ColumnGroup.Name;
				groupStates.Add( groupInstance );
			}

			ss.GroupStates = groupStates.ToArray();

			using( XmlTextWriter tw = new XmlTextWriter( writer ) )
			{
				tw.Formatting = Formatting.Indented;

				Helper.SerializationState.Serialize( tw, ss );
			}
		}

		public void DeSerializeState( System.IO.TextReader reader )
		{
			Helper.SerializationState ss;

			using( XmlTextReader tr = new XmlTextReader( reader ) )
			{
				ss = Helper.SerializationState.Deserialize( tr );
			}

			if( ss == null )
			{
				return;
			}

			this.Columns.GroupedItems.Clear();
			this.Columns.VisibleItems.Clear();

			//
			// Add groups
			Array.Sort<Helper.SerializationState.ColumnState>( ss.ColumnStates,
				delegate( Helper.SerializationState.ColumnState x, Helper.SerializationState.ColumnState y )
				{
					return x.GroupedIndex - y.GroupedIndex;
				}
			);
			foreach( Helper.SerializationState.ColumnState columnState in ss.ColumnStates )
			{
				if( columnState.GroupedIndex >= 0 )
				{
					Column column = this.Columns.FromName( columnState.Name );
					if( column != null )
					{
						this.Columns.GroupedItems.Add( column );
					}
				}
			}

			//
			// Add visible items and set properties.
			Array.Sort<Helper.SerializationState.ColumnState>( ss.ColumnStates,
				delegate( Helper.SerializationState.ColumnState x, Helper.SerializationState.ColumnState y )
				{
					return x.VisibleIndex - y.VisibleIndex;
				}
			);
			foreach( Helper.SerializationState.ColumnState columnState in ss.ColumnStates )
			{
				Column column = this.Columns.FromName( columnState.Name );
				if( column != null )
				{
					if( columnState.VisibleIndex >= 0 )
					{
						this.Columns.VisibleItems.Add( column );
					}

					column.SortOrder = columnState.SortOrder;
					column.GroupSortOrder = columnState.GroupSortOrder;
					column.Width = columnState.Width;
				}
			}

			//
			// Restore group expansion states
			_groupExpansionState.Clear();
			_globalGroupState = ss.GlobalGroupState == Helper.SerializationState.GroupState.GroupCollapsed ? GroupState.Collapsed : GroupState.Expanded;
			_groupExpansionState.Clear();
			foreach( Helper.SerializationState.GroupInstance gi in ss.GroupStates )
			{
				Column column = this.Columns.FromName( gi.GroupName );
				if( column != null )
				{
					_groupExpansionState.Add( new GroupIdentifier( gi.GroupPath, column ) );
				}
			}
			this.Host.LazyLayout( null );
		}

		internal void LazyLayout()
		{
			this.Host.LazyLayout( null );
		}


		public virtual int ReservedSpaceFromHeaderFirstColumn
		{
			get
			{
				return this.ListControl.Columns.GroupedItems.Count * _groupIndent;
			}
		}


		public void SelectAll()
		{
			List<RowIdentifier> allItems = new List<RowIdentifier>( this.ItemList.Count );
			for( int i = 0; i < this.ItemList.Count; i++ )
			{
				RowIdentifier rowIdentifier = new NonGroupRow( this.ItemList[i] );
				allItems.Add( rowIdentifier );
			}
			this.SelectedItems.ClearAndAdd( allItems.ToArray() );
			this.Invalidate();
		}

		public override Point GetScrollCoordinates()
		{
			Point pt = base.GetScrollCoordinates();
			pt.Y = 0;
			return pt;
		}



		public override void Layout( Section.GraphicsSettings gs, System.Drawing.Size maximumSize )
		{
			this.Size = maximumSize;
			this.CalculateListItems();

			if( this.Columns.Count > 0 && _headerSection.IsVisible )
			{
				this.HorizontalScrollbarVisible = _headerSection.IdealWidth > maximumSize.Width;
			}

			Column[] groupColumns = this.ListControl.Columns.GroupedItems.ToArray();

			//
			// Leave header and options toolbar sections...
			this.DeleteRange( this.ExcludeFirstChildrenFromVScroll, this.Children.Count - this.ExcludeFirstChildrenFromVScroll );

			base.Layout( gs, maximumSize );

			if( this.ItemList.Count > 0 )
			{
				bool stop = false;
				while( !stop )
				{
					Rectangle rowsRectangle = this.WorkingRectangle;
					int yPos = _layoutDirection == Direction.Forward ? rowsRectangle.Top : rowsRectangle.Bottom;

					_countDisplayed = 0;

					//
					//	Create sections until either we have no more items
					//	or go over our height
					int position = this.LineStart;
					foreach( RowIdentifier rowInfo in this.GetRowEnumerator( this.LineStart, _layoutDirection ) )
					{
						object item = rowInfo.Items[0];

						Section newSection;

						int groupIndex = rowInfo.ColumnGroup == null ? -1 : GroupIndexFromGroup( rowInfo.ColumnGroup );

						if( groupIndex != -1 && groupIndex < this.ListControl.Columns.GroupedItems.Count )
						{
							newSection = this.Host.SectionFactory.CreateGroupSection( this.ListControl,
								rowInfo,
								_headerSection,
								position,
								_groupIndent * groupIndex );
							this.Children.Add( newSection );
							newSection.Layout( gs, new Size( this.Size.Width, this.Size.Height ) );
						}
						else
						{
							newSection = this.Host.SectionFactory.CreateRowSection( this.ListControl, rowInfo, _headerSection, position );
							this.Children.Add( newSection );
							newSection.Layout( gs, new Size( this.Size.Width, this.Size.Height ) );
						}

						switch( _layoutDirection )
						{
							case Direction.Forward:
								newSection.Location = new Point( this.Location.X, yPos );
								yPos = newSection.Rectangle.Bottom;
								stop = yPos >= rowsRectangle.Bottom;
								break;
							case Direction.Backward:
								newSection.Location = new Point( this.Location.X, yPos - newSection.Size.Height );
								yPos = newSection.Rectangle.Top;
								stop = yPos <= rowsRectangle.Top;
								break;
						}
						if( stop )
							break;

						_countDisplayed++;
						position++;
					}
					if( !stop  ) // must have run out of things to fill
					{
						switch( _layoutDirection )
						{
							case Direction.Forward:
								if( this.LineStart > 0 )
								{
									this.DeleteRange( this.ExcludeFirstChildrenFromVScroll, this.Children.Count - this.ExcludeFirstChildrenFromVScroll );
									_layoutDirection = Direction.Backward;
									_lineStart = _rowInformation.Count - 1;
								}
								else
								{
									stop = true;
								}
								break;
							case Direction.Backward:
								this.DeleteRange( this.ExcludeFirstChildrenFromVScroll, this.Children.Count - this.ExcludeFirstChildrenFromVScroll );
								_layoutDirection = Direction.Forward;
								_lineStart = 0;
								break;
						}
					}
					else
					{
						break; // all done
					}
				}
			}

			if( this.VScrollbar != null )
			{
				this.VScrollbar.LargeChange = _countDisplayed;
			}

			this.VerticalScrollbarVisible = _countDisplayed < this.RowInformation.Count;
			if( this.Columns.Count == 0 && !_headerSection.IsVisible )
			{
				this.HorizontalScrollbarVisible = this.MaxWidth > maximumSize.Width;
			}

			this.UpdateScrollInfo();
		}

		public int GroupIndexFromGroup( Column groupColumn )
		{
			return this.ListControl.Columns.GroupedItems.IndexOf( groupColumn );
		}


		public SelectedItemsCollection SelectedItems
		{
			get
			{
				return this.ListControl.SelectedItems;
			}
		}


		public override void KeyDown( KeyEventArgs e )
		{
			if( this.RowInformation == null )
				return;

			bool shiftBeingPressed = (e.Modifiers & Keys.Shift) == Keys.Shift;
			bool ctrlBeingPressed = (e.Modifiers & Keys.Control) == Keys.Control;


			int moveFocusBy = 0;
			switch( e.KeyCode )
			{
				case Keys.Left:
					{
						if( this.FocusedItem != null )
						{
							PositionedRowIdentifier groupItem;
							int position = this.FocusedItem.Position;
							if( this.FocusedItem.RowIdentifier.ColumnGroup != null )
							{
								GroupIdentifier gi = this.FocusedItem.RowIdentifier as GroupIdentifier;
								if( GetGroupState( gi ) == GroupState.Collapsed && position > 0 )
								{
									--position;
								}
							}
							groupItem = FindGroupFromRowIdentifier( position );
							if( groupItem != null )
							{
								this.SetFocusWithSelectionCheck( groupItem );
								SetGroupState( groupItem.RowIdentifier, GroupState.Collapsed, true );
							}
						}
					}
					break;
				case Keys.Right:
					{
						if( this.FocusedItem != null )
						{
							PositionedRowIdentifier groupItem = FindGroupFromRowIdentifier( this.FocusedItem.Position );
							if( groupItem != null )
							{
								SetGroupState( groupItem.RowIdentifier, GroupState.Expanded, false );
							}
						}
					}
					break;
				case Keys.Multiply:
					SetGlobalGroupState( GroupState.Expanded );
					break;
				case Keys.Subtract:
					SetGlobalGroupState( GroupState.Collapsed );
					break;
				case Keys.Down:
					moveFocusBy = 1;
					break;
				case Keys.Up:
					moveFocusBy = -1;
					break;
				case Keys.Space:
					if( this.FocusedItem != null )
					{
						if( shiftBeingPressed )
						{
							this.SelectedItems.AddInternal( this.FocusedItem.RowIdentifier );
						}
						else
						{
							this.SelectedItems.ClearAndAdd( this.FocusedItem.RowIdentifier );
						}
						return;
					}
					break;
				case Keys.PageDown:
					moveFocusBy = _countDisplayed;
					break;
				case Keys.PageUp:
					moveFocusBy -= _countDisplayed;
					break;
				case Keys.End:
					moveFocusBy = _focusedItem == null ? this.RowInformation.Count : this.RowInformation.Count - _focusedItem.Position - 1;
					break;
				case Keys.Home:
					moveFocusBy = -_focusedItem.Position;
					break;
			}

			if( moveFocusBy == 0 )
			{
				return; // nothing to do
			}

			int newPos = _focusedItem == null ? 0 : _focusedItem.Position + moveFocusBy;

			//
			// Bounds check
			if( newPos >= this.RowInformation.Count )
			{
				newPos = this.RowInformation.Count - 1;
			}
			if( newPos < 0 )
			{
				newPos = 0;
			}

			if( newPos < this.RowInformation.Count )
			{
				RowIdentifier ri = this.RowInformation[newPos];
				PositionedRowIdentifier newFocusedItem = new PositionedRowIdentifier( ri, newPos );
				if( ctrlBeingPressed )
				{
					this.FocusedItem = newFocusedItem;
				}
				else
				{
					SetFocusWithSelectionCheck( newFocusedItem );
				}
			}
		}

		public override void MouseWheel( MouseEventArgs e )
		{
			base.MouseWheel( e );

			if( this.VerticalScrollbarVisible )
			{
				int newScrollValue = this.VScrollbar.Value - e.Delta * System.Windows.Forms.SystemInformation.MouseWheelScrollLines / 120;

				if( newScrollValue < this.VScrollbar.Minimum )
				{
					newScrollValue = this.VScrollbar.Minimum;
				}

				if( newScrollValue > this.VScrollbar.Maximum - this.VScrollbar.LargeChange )
				{
					newScrollValue = this.VScrollbar.Maximum - this.VScrollbar.LargeChange + 1;
				}

				this.VScrollbar.Value = newScrollValue;
			}
		}

		public override void MouseDown( MouseEventArgs e )
		{
			RowSection rowSectionSelected = GetRowSectionFromPoint( new Point( e.X, e.Y ) );
			if( rowSectionSelected == null )
				return;


			RowIdentifier ri = rowSectionSelected.RowIdentifier;
			PositionedRowIdentifier si = new PositionedRowIdentifier( ri, rowSectionSelected.Position );
			if( e.Button != MouseButtons.Right || !this.SelectedItems.Contains( ri ) )
			{
				SetFocusWithSelectionCheck( si );
			}
			else
			{
				this.FocusedItem = si;
			}
		}



		public override void PaintBackground( Section.GraphicsSettings gs, Rectangle clipRect )
		{
			gs.Graphics.FillRectangle( SystemBrushes.Window, this.Rectangle );
			base.PaintBackground( gs, clipRect );
		}

		#region Group Control
		public enum GroupState { Collapsed, Expanded };
		public GroupState GetGroupState( RowIdentifier ri )
		{
			GroupState opposite = _globalGroupState == GroupState.Expanded ? GroupState.Collapsed : GroupState.Expanded;
			return _groupExpansionState.Contains( ri ) ? opposite : _globalGroupState;
		}
		public void SetGroupState( RowIdentifier ri, GroupState groupState, bool layoutNow )
		{
			System.Diagnostics.Debug.Assert( ri.ColumnGroup != null );
			if( GetGroupState( ri ) != groupState )
			{
				if( _globalGroupState == groupState )
				{
					_groupExpansionState.Remove( ri );
				}
				else
				{
					_groupExpansionState.Add( ri );
				}
				this.ClearRowInformation();
				if( layoutNow )
				{
					this.Layout();
				}
				else
				{
					this.Host.LazyLayout( this );
				}
			}
		}
		public void SetGlobalGroupState( GroupState globalGroupState )
		{
			_groupExpansionState.Clear();
			_globalGroupState = globalGroupState;
			this.ClearRowInformation();
			this.Host.LazyLayout( this );
		}
		#endregion


		public bool HasFocus( RowIdentifier ri )
		{
			return this.Host.FocusedSection == this && _focusedItem != null && _focusedItem.RowIdentifier == ri;
		}


		internal ItemLists.ItemList ItemList
		{
			get
			{
				return this.ListControl.ItemList;
			}
		}

		internal ColumnList Columns
		{
			get
			{
				return this.ListControl.Columns;
			}
		}

		internal void ListUpdated( bool lazyLayout )
		{
			this.ClearRowInformation();
			if( this.Host.IsControlCreated )
			{
				if( lazyLayout )
				{
					this.Host.LazyLayout( this );
				}
				else
				{
					this.Layout();
				}
			}
		}

		internal class GroupIdentifier : RowIdentifier
		{
			public GroupIdentifier( int start, ListSection listSection, int groupIndex, object item )
			{
				EventingList<Column> groupColumns = listSection.Columns.GroupedItems;
				_listItems = listSection.ItemList.ToArray();
				_groupValues = new string[groupIndex + 1];
				_hashCode = 0;
				for( int i = 0; i <= groupIndex; i++ )
				{
					string s = groupColumns[i].GroupItemAccessor( item ).ToString();
					_groupValues[i] = s;
					_hashCode += s.GetHashCode();
				}
				_groupColumn = groupColumns[groupIndex];
				this.Start = start;
				this.End = start;
			}

			/// <summary>
			/// Used only for serialization.
			/// </summary>
			/// <param name="groupValues"></param>
			/// <param name="groupColumn"></param>
			internal GroupIdentifier( string[] groupValues, Column groupColumn )
			{
				_hashCode = 0;
				foreach( object o in groupValues )
				{
					_hashCode += o.GetHashCode();
				}
				this.Start = -1;
				this.End = -1;
				_listItems = null;
				_groupValues = groupValues;
				_groupColumn = groupColumn;
			}
			public int Start;
			public int End;

			public override bool Equals( object obj )
			{
				if( object.ReferenceEquals( this, obj ) )
					return true;

				GroupIdentifier other = obj as GroupIdentifier;
				if( object.ReferenceEquals( other, null ) || other._groupValues.Length != _groupValues.Length )
					return false;

				for( int i = 0; i < _groupValues.Length; i++ )
				{
					if( !_groupValues[i].Equals( other._groupValues[i] ) )
						return false;
				}

				return true;
			}
			public override int GetHashCode()
			{
				return _hashCode;
			}

			public override Column ColumnGroup
			{
				get
				{
					return _groupColumn;
				}
			}

			public string[] GroupValues
			{
				get
				{
					return _groupValues;
				}
			}


			public override object[] Items
			{
				get
				{
					int count = this.End - this.Start;
					object[] listItems = new object[count];
					Array.Copy( _listItems, this.Start, listItems, 0, count );
					return listItems;
				}
			}
			private readonly int _hashCode;
			private readonly string[] _groupValues;
			private readonly Column _groupColumn;
			private object[] _listItems;
		}

		internal class NonGroupRow : RowIdentifier
		{
			public NonGroupRow( object item )
			{
				_item = item;
			}


			public override Column ColumnGroup
			{
				get
				{
					return null;
				}
			}


			public override object[] Items
			{
				get
				{
					return new object[] { _item };
				}
			}
			public override bool Equals( object obj )
			{
				if( object.ReferenceEquals( this, obj ) )
					return true;

				NonGroupRow other = obj as NonGroupRow;
				if( object.ReferenceEquals( other, null ) )
					return false;

				return other._item == _item;
			}
			public override int GetHashCode()
			{
				return _item.GetHashCode();
			}
			private object _item;
		}

		internal PositionedRowIdentifier FocusedItem
		{
			get
			{
				return this.Host.FocusedSection == this ? _focusedItem : null;
			}
			set
			{
				if( _focusedItem != value )
				{
					RowSection oldFocusedItem = _focusedItem == null ? null : RowSectionFromRowIdentifier( _focusedItem.RowIdentifier );

					_focusedItem = value;

					if( oldFocusedItem != null )
					{
						oldFocusedItem.LostFocus();
					}

					if( _focusedItem != null )
					{
						RowSection focusedSection = RowSectionFromRowIdentifier( _focusedItem.RowIdentifier );
						Rectangle rowsRect = this.WorkingRectangle;
						if( focusedSection != null && focusedSection.Rectangle.Top >= rowsRect.Top && focusedSection.Rectangle.Bottom <= rowsRect.Bottom )
						{
							focusedSection.GotFocus();
						}
						else
						{
							//
							// focused item is not in view so we make it visible
							int position = PositionFromRowIdentifier( _focusedItem.RowIdentifier );
							if( position < this.LineStart )
							{
								_layoutDirection = Direction.Forward;
							}
							else
							{
								_layoutDirection = Direction.Backward;
							}
							this.LineStart = position;
						}
					}
					this.ListControl.FireFocusChanged( oldFocusedItem == null ? null : oldFocusedItem.RowIdentifier,
						_focusedItem == null ? null : _focusedItem.RowIdentifier );
				}
			}
		}


		private enum Direction { Forward, Backward };
		private RowSection GetRowSectionFromPoint( Point pt )
		{
			Section section = this.SectionFromPoint( pt );
			while( section != null && !(section is RowSection) )
			{
				section = section.Parent;
			}
			return (RowSection)section;
		}
		private void SetFocusWithSelectionCheck( PositionedRowIdentifier si )
		{
			this.FocusedItem = si;

			bool shiftBeingPressed = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
			bool ctrlBeingPressed = (Control.ModifierKeys & Keys.Control) == Keys.Control;


			if( this.AllowMultiSelect && _lastRowSelected != null && shiftBeingPressed )
			{
				RowIdentifier anchor = _lastRowSelected.RowIdentifier;
				int from;
				int to;
				if( _lastRowSelected.Position < si.Position )
				{
					from = _lastRowSelected.Position;
					to = si.Position;
				}
				else
				{
					to = _lastRowSelected.Position;
					from = si.Position;
				}
				List<RowIdentifier> selectedItems = new List<RowIdentifier>();
				foreach( RowIdentifier rowInfo in this.GetRowEnumerator( from, Direction.Forward ) )
				{
					selectedItems.Add( rowInfo );

					if( from++ == to )
						break;
				}
				if( ctrlBeingPressed )
				{
					this.SelectedItems.AddRangeInternal( selectedItems.ToArray() );
				}
				else
				{
					this.SelectedItems.ClearAndAdd( selectedItems.ToArray() );
				}
			}
			else
			{
				if( ctrlBeingPressed )
				{
					if( this.SelectedItems.IsSelected( si.RowIdentifier ) )
					{
						this.SelectedItems.RemoveInternal( si.RowIdentifier );
					}
					else
					{
						this.SelectedItems.AddInternal( si.RowIdentifier );
					}
				}
				else
				{
					if( !this.SelectedItems.IsSelected( si.RowIdentifier ) || this.SelectedItems.Count != 1 )
					{
						this.SelectedItems.Clear();
						this.SelectedItems.AddInternal( si.RowIdentifier );
					}
				}
				_lastRowSelected = si;
			}
		}



		private IEnumerable<RowIdentifier> GetRowEnumerator( int lineStart, Direction direction )
		{
			if( this.ItemList.Count > _scrollbarMax )
			{
				throw new NotImplementedException();
			}
			else
			{
				if( this.RowInformation == null )
				{
					this.CalculateListItems();
				}
				switch( direction )
				{
					case Direction.Forward:
						for( int i = lineStart; i < this.RowInformation.Count; i++ )
						{
							yield return this.RowInformation[i];
						}
						break;
					case Direction.Backward:
						for( int i = lineStart; i >= 0; i-- )
						{
							if( i < this.RowInformation.Count )
							{
								yield return this.RowInformation[i];
							}
						}
						break;
				}
			}
		}

		private static int ItemInNewGroup( Column[] groupColumns, object itemNow, object itemBefore )
		{
			int iColumn = 0;
			foreach( Column column in groupColumns )
			{
				if( itemBefore == null || !column.GroupItemAccessor( itemNow ).Equals( column.GroupItemAccessor( itemBefore ) ) )
				{
					return iColumn;
				}
				iColumn++;
			}
			return -1;
		}

		private ListControl ListControl
		{
			get
			{
				return (ListControl)this.Host;
			}
		}

		private int LineStart
		{
			get
			{
				return _lineStart;
			}
			set
			{
				if( _lineStart != value )
				{
					_lineStart = value;
					this.Layout();
				}
			}
		}
		#region ILayoutController Members

		int HeaderSection.ILayoutController.ReservedNearSpace
		{
			get
			{
				return this.ListControl.Columns.GroupedItems.Count * _groupIndent;
			}
		}
		void HeaderSection.ILayoutController.HeaderLayedOut()
		{
			if( this.HorizontalScrollbarVisible )
			{
				SetHScrollInfo();
			}
		}
		public int CurrentHorizontalScrollPosition
		{
			get
			{
				return this.HorizontalScrollbarVisible ? this.HScrollbar.Value : 0;
			}
			set
			{
				this.HorizontalScrollbarVisible = true;
				this.HScrollbar.Value = value;
			}
		}


		#endregion

		private void Layout()
		{
			if( _enforceLazyLayout )
			{
				this.Host.LazyLayout( this );
			}
			else
			{
				using( Graphics grfx = this.Host.CreateGraphics() )
				{
					this.Layout( new GraphicsSettings( grfx ), this.Rectangle.Size );
					this.Invalidate();
				}
			}
		}

		protected override void SetVScrollInfo()
		{
			this.VScrollbar.Minimum = 0;
			this.VScrollbar.SmallChange = 1;
			this.VScrollbar.LargeChange = _countDisplayed;

			if( this.ItemList.Count > _scrollbarMax )
			{
				this.VScrollbar.Maximum = _scrollbarMax - 1;
				this.ClearRowInformation();
			}
			else
			{
				this.VScrollbar.Maximum = this.CalculateListItems() - 1;
			}

			int required;

			switch( _layoutDirection )
			{
				case Direction.Forward:
					required = this.LineStart;
					break;
				case Direction.Backward:
					required = this.LineStart - _countDisplayed + 1;
					break;
				default:
					throw new InvalidOperationException();
			}

			required = Math.Min( Math.Max( required, this.VScrollbar.Minimum ), this.VScrollbar.Maximum );

			try
			{
				this.VScrollbar.Value = required;
			}
			catch( ArgumentOutOfRangeException )
			{
			}
		}

		internal class PositionedRowIdentifier
		{
			public PositionedRowIdentifier( RowIdentifier rowIdentifier, int position )
			{
				this.RowIdentifier = rowIdentifier;
				this.Position = position;
			}
			public override bool Equals( object obj )
			{
				return this.RowIdentifier.Equals( obj );
			}
			public override int GetHashCode()
			{
				return this.RowIdentifier.GetHashCode();
			}
			public static bool operator ==( PositionedRowIdentifier lhs, PositionedRowIdentifier rhs )
			{
				if( object.ReferenceEquals( lhs, rhs ) )
					return true;
				if( object.ReferenceEquals( lhs, null ) )
					return false;
				if( object.ReferenceEquals( rhs, null ) )
					return false;

				return lhs.RowIdentifier == rhs.RowIdentifier && lhs.Position == rhs.Position;
			}
			public static bool operator !=( PositionedRowIdentifier lhs, PositionedRowIdentifier rhs )
			{
				return !(lhs == rhs);
			}
			public readonly RowIdentifier RowIdentifier;
			public int Position;
		}

		protected override void SetHScrollInfo()
		{
			this.HScrollbar.LargeChange = this.Rectangle.Width;
			this.HScrollbar.Minimum = 0;
			this.HScrollbar.Maximum = this.MaxWidth;
			this.HScrollbar.SmallChange = 1;
		}

		private int MaxWidth
		{
			get
			{
				if( _headerSection.Columns.Count == 0 && !_headerSection.IsVisible )
				{
					int max = 0;
					foreach( Section section in this.Children )
					{
						if( max < section.Rectangle.Width )
						{
							max = section.Rectangle.Width;
						}
					}
					return max;
				}
				else
				{
					return _headerSection.IdealWidth - 1;
				}
			}
		}

		private void HandleSyncing( RowIdentifier ri, int newPosition, PositionedRowIdentifier[] trackableItems, List<PositionedRowIdentifier> newSelection )
		{
			foreach( PositionedRowIdentifier si in trackableItems )
			{
				if( si != null )
				{
					if( ri.Equals( si.RowIdentifier ) )
					{
						si.Position = newPosition;
					}
				}
			}
			if( this.SelectedItems.Contains( ri ) )
			{
				newSelection.Add( new PositionedRowIdentifier( ri, newPosition ) );
			}
		}

		protected override void OnVScrollValueChanged( int value )
		{
			int newPosition;
			if( this.ItemList.Count > _scrollbarMax )
			{
				newPosition = (int)(value / (float)_scrollbarMax * this.ItemList.Count);
			}
			else
			{
				newPosition = value;
			}
			switch( _layoutDirection )
			{
				case Direction.Forward:
					this.LineStart = newPosition;
					break;
				case Direction.Backward:
					if( this.LineStart - _countDisplayed + 1 != newPosition )
					{
						_layoutDirection = Direction.Forward;
						this.LineStart = newPosition;
					}
					break;
			}
		}


		private void GroupedItems_DataChanged( object sender, EventingList<Column>.EventInfo e )
		{
			//
			// Grouping has changed so clear collapse point, maybe later we can do something to preserve items state.
			_groupExpansionState.Clear();
		}


		private RowSection RowSectionFromRowIdentifier( RowIdentifier ri )
		{
			foreach( Section section in this.Children )
			{
				RowSection rowSection = section as RowSection;
				if( rowSection != null && rowSection.RowIdentifier == ri )
				{
					return rowSection;
				}
			}
			return null;
		}


		private void SetVScrollValue()
		{
			if( this.VScrollbar != null )
			{
				switch( _layoutDirection )
				{
					case Direction.Forward:
						this.VScrollbar.Value = this.LineStart;
						System.Diagnostics.Debug.Assert( this.VScrollbar.Value >= 0 );
						break;
					case Direction.Backward:
						this.VScrollbar.Value = this.LineStart - _countDisplayed + 1;
						System.Diagnostics.Debug.Assert( this.VScrollbar.Value >= 0 );
						break;
				}
			}
		}

		private PositionedRowIdentifier SelectedItemFromRowIdentifer( RowIdentifier ri )
		{
			return new PositionedRowIdentifier( ri, PositionFromRowIdentifier( ri ) );
		}
		private class EnforceLazyLayout : IDisposable
		{
			public EnforceLazyLayout( ListSection listSection )
			{
				_listSection = listSection;
				_savedEnforceLazyLayout = listSection._enforceLazyLayout;
				_listSection._enforceLazyLayout = true;
			}

			public void Dispose()
			{
				_listSection._enforceLazyLayout = _savedEnforceLazyLayout;
			}
			private bool _savedEnforceLazyLayout = false;
			private ListSection _listSection;
		}

		private List<RowIdentifier> _lastCalculatedRowInformation = null;

		/// <summary>
		/// Returns the number of items in the list adjusted for the group items.
		/// This method will also create a bit array of types <seealso cref="_positionTypes"/>
		/// </summary>
		/// <returns></returns>
		private int CalculateListItems()
		{
			using( new EnforceLazyLayout( this ) )
			{
				if( _rowInformation == null )
				{
					PositionedRowIdentifier[] trackableItems = new PositionedRowIdentifier[3];
					trackableItems[0] = _focusedItem;

					if( _lastCalculatedRowInformation != null )
					{
						if( this.LineStart < _lastCalculatedRowInformation.Count )
						{
							trackableItems[1] = new PositionedRowIdentifier( _lastCalculatedRowInformation[this.LineStart], this.LineStart );
							trackableItems[1].Position = 0;
						}
						if( _focusedItem != null )
						{
							int i = _focusedItem.Position + 1;
							for( ; i < _lastCalculatedRowInformation.Count; i++ )
							{
								RowIdentifier ri = _lastCalculatedRowInformation[i];
								//
								// Only settle on same type rows.
								if( (_focusedItem.RowIdentifier.ColumnGroup == null ^ ri.ColumnGroup == null) == false )
								{
									//
									// Only include items that exist now.
									if( this.ItemList.IndexOf( ri.Items[0] ) != -1 )
									{
										trackableItems[2] = new PositionedRowIdentifier( _lastCalculatedRowInformation[i], i );

										break;
									}
								}
							}
						}
					}

					Column[] groupColumns = this.ListControl.Columns.GroupedItems.ToArray();
					object lastItem = null;
					_lastCalculatedRowInformation = _rowInformation = new List<RowIdentifier>( this.ItemList.Count );
					int hideRowGroupIndex = -1;
					List<PositionedRowIdentifier> newSelection = new List<PositionedRowIdentifier>();


					GroupIdentifier[] activeGroups = new GroupIdentifier[groupColumns.Length];
					for( int i = 0; i < this.ItemList.Count; i++ )
					{
						object item = this.ItemList[i];

						int groupIndex = ItemInNewGroup( groupColumns, item, lastItem );
						if( groupIndex != -1 )
						{
							for( int iGroup = groupIndex; iGroup < groupColumns.Length; iGroup++ )
							{
								GroupIdentifier gi = new GroupIdentifier( i, this, iGroup, item );
								int hideGroupIndex = hideRowGroupIndex;
								if( hideRowGroupIndex == -1 || iGroup <= hideRowGroupIndex )
								{
									if( GetGroupState( gi ) == GroupState.Collapsed )
									{
										hideGroupIndex = iGroup;
										if( groupIndex <= hideRowGroupIndex )
										{
											hideRowGroupIndex = -1;
										}
									}
									else
									{
										hideRowGroupIndex = hideGroupIndex = -1;
									}
								}

								if( hideRowGroupIndex == -1 )
								{
									this.HandleSyncing( gi, _rowInformation.Count, trackableItems, newSelection );
									_rowInformation.Add( gi );
									if( activeGroups[iGroup] != null )
									{
										activeGroups[iGroup].End = i;
									}
									activeGroups[iGroup] = gi;
								}
								hideRowGroupIndex = hideGroupIndex;
							}
						}
						if( hideRowGroupIndex == -1 )
						{
							RowIdentifier ri = new NonGroupRow( item );
							this.HandleSyncing( ri, _rowInformation.Count, trackableItems, newSelection );
							_rowInformation.Add( ri );
						}
						lastItem = item;
					}

					foreach( GroupIdentifier gi in activeGroups )
					{
						if( gi != null )
						{
							gi.End = this.ItemList.Count;
						}
					}
					if( this.VerticalScrollbarVisible )
					{
						int newMax = _rowInformation.Count == 0 ? 0 : _rowInformation.Count - 1;
						if( this.VScrollbar.Value >= newMax )
						{
							this.VScrollbar.Value = newMax;
						}
						this.VScrollbar.Maximum = newMax;
					}

					if( trackableItems[1] != null )
					{
						this.LineStart = trackableItems[1].Position;
					}
					if( _focusedItem != null && !IsSelectedItemValid( _focusedItem ) )
					{
						if( trackableItems[2] != null && IsSelectedItemValid( trackableItems[2] ) )
						{
							PositionedRowIdentifier oldFocusedItem = _focusedItem;
							if( _focusedItem != trackableItems[2] )
							{
								_focusedItem = trackableItems[2];
								this.ListControl.FireFocusChanged( oldFocusedItem == null ? null : oldFocusedItem.RowIdentifier,
									_focusedItem == null ? null : _focusedItem.RowIdentifier );
							}
							newSelection.Add( _focusedItem );
						}
						else
						{
							int newPosition;
							if( _focusedItem.Position >= _rowInformation.Count )
							{
								newPosition = _rowInformation.Count - 1;
							}
							else
							{
								newPosition = _focusedItem.Position;
							}
							if( newPosition >= 0 )
							{
								PositionedRowIdentifier si = new PositionedRowIdentifier( _rowInformation[newPosition], newPosition );
								this.FocusedItem = si;
								newSelection.Add( _focusedItem );
							}
							else
							{
								this.FocusedItem = null;
							}
						}
					}
					RowIdentifier[] rowItemsToSelect = new RowIdentifier[newSelection.Count];
					int j = 0;
					foreach( PositionedRowIdentifier pri in newSelection )
					{
						rowItemsToSelect[j++] = pri.RowIdentifier;
					}
					this.SelectedItems.ClearAndAdd( rowItemsToSelect );
					if( this.SelectedItems.Count == 0 && this.FocusedItem == null && this.VScrollbar != null && this.VScrollbar.Visible )
					{
						this.VScrollbar.Value = 0;
					}
				}
				_mapOfPositions = null;
			}
			return _rowInformation.Count;
		}

		private bool IsSelectedItemValid( PositionedRowIdentifier item )
		{
			return item != null && (item.Position < _rowInformation.Count && _rowInformation[item.Position] == item.RowIdentifier);
		}

		private PositionedRowIdentifier FindGroupFromRowIdentifier( int position )
		{
			if( position < this.RowInformation.Count )
			{
				for( int i = position; i >= 0; i-- )
				{
					GroupIdentifier gi = this.RowInformation[i] as GroupIdentifier;
					if( gi != null )
					{
						return new PositionedRowIdentifier( gi, i );
					}
				}
			}
			return null;
		}

		internal List<RowIdentifier> RowInformation
		{
			get
			{
				if( _rowInformation == null )
				{
					this.CalculateListItems();
				}
				return _rowInformation;
			}
		}

		private void ClearRowInformation()
		{
			_rowInformation = null;
			_mapOfPositions = null;
		}
		private Dictionary<RowIdentifier, int> GetRowPosMap()
		{
			Dictionary<RowIdentifier, int> mapOfPositions = _mapOfPositions == null ? null : (Dictionary<RowIdentifier, int>)_mapOfPositions.Target;
			if( mapOfPositions == null )
			{
				List<RowIdentifier> rowInformation = _rowInformation == null ? _lastCalculatedRowInformation : _rowInformation;

				if( rowInformation == null )
				{
					this.CalculateListItems();
				}

				mapOfPositions = new Dictionary<RowIdentifier, int>();
				if( rowInformation != null )
				{
					int i = 0;
					foreach( RowIdentifier ri in rowInformation )
					{
						mapOfPositions[ri] = i++;
					}
				}

				_mapOfPositions = new WeakReference( mapOfPositions );
			}
			return mapOfPositions;
		}
		internal PositionedRowIdentifier GetPositionedIdentifierFromObject( RowIdentifier identifier )
		{
			int position;
			if( GetRowPosMap().TryGetValue( identifier, out position ) )
			{
				return new PositionedRowIdentifier( identifier, position );
			}

			return null;
		}
		private int PositionFromRowIdentifier( RowIdentifier ri )
		{
			int position;
			if( GetRowPosMap().TryGetValue( ri, out position ) )
			{
				return position;
			}

			return -1;
		}



		public bool ShowHeaderSection
		{
			get
			{
				return _headerSection.IsVisible;
			}
			set
			{
				if( _headerSection.IsVisible != value )
				{
					_headerSection.IsVisible = value;
					this.LazyLayout();
				}
			}
		}

		private WeakReference _mapOfPositions = null;
		private List<RowIdentifier> _rowInformation = null;
		private Direction _layoutDirection = Direction.Forward;
		private bool _allowMultiSelect = true;
		private GroupState _globalGroupState = GroupState.Expanded;
		private PositionedRowIdentifier _lastRowSelected = null;
		private PositionedRowIdentifier _focusedItem = null;
		private Set<RowIdentifier> _groupExpansionState = new Set<RowIdentifier>();
		private int _lineStart = 0;
		private int _countDisplayed = 0;
		private const int _scrollbarMax = int.MaxValue;
		private const int _groupIndent = 10;
		private HeaderSection _headerSection;
		private bool _enforceLazyLayout = false;
	}
}