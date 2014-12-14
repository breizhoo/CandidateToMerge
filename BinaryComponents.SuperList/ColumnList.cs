/////////////////////////////////////////////////////////////////////////////
//
// (c) 2007 BinaryComponents Ltd.  All Rights Reserved.
//
// http://www.binarycomponents.com/
//
/////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using BinaryComponents.Utility.Collections;
using System.Text;

namespace BinaryComponents.SuperList
{
	public class ColumnList : EventingList<Column>
	{

		public ColumnList()
		{
			this.DataChanged += new EventHandler<EventingList<Column>.EventInfo>( ColumnList_DataChanged );
			this.GroupedItems.DataChanged += new EventHandler<EventingList<Column>.EventInfo>( GroupedItems_DataChanged );
			this.VisibleItems.DataChanged += new EventHandler<EventingList<Column>.EventInfo>( VisibleItems_DataChanged );
		}

		public delegate void ColumnDataChangedHandler( object sender, Column.ColumnDataChangedEventArgs eventArgs );
		public event ColumnDataChangedHandler ColumnDataChanged;

		public EventingList<Column> GroupedItems
		{
			get
			{
				return _groupedItems;
			}
		}

		public EventingList<Column> VisibleItems
		{
			get
			{
				return _visibleItems;
			}
		}

		public Column FromName( string name )
		{
			foreach( Column c in this )
			{
				if( c.Name == name )
				{
					return c;
				}
			}
			return null;
		}

		#region Implementation
		protected virtual void OnColumnDataChanged( Column.ColumnDataChangedEventArgs eventArgs )
		{
			if( this.ColumnDataChanged != null )
			{
				this.ColumnDataChanged( this, eventArgs );
			}
		}
		private void ColumnList_DataChanged( object sender, EventingList<Column>.EventInfo e )
		{
			_inColumnsChanged = true;
			try
			{
				switch( e.EventType )
				{
					case EventingList<Column>.EventType.Deleted:
						_visibleItems.RemoveRange( e.Items );
						_groupedItems.RemoveRange( e.Items );
						foreach( Column c in e.Items )
						{
							c.DataChanged -= new Column.ColumnDataChangedHandler( column_DataChanged );
						}
						break;
					case EventingList<Column>.EventType.Added:
						foreach( Column c in e.Items )
						{
							if( c.IsVisible && _visibleItems.IndexOf( c ) == -1 )
							{
								_visibleItems.Add( c );
							}
							if( c.IsGrouped && _groupedItems.IndexOf( c ) == -1 )
							{
								_groupedItems.Add( c );
							}
							c.DataChanged += new Column.ColumnDataChangedHandler( column_DataChanged );
						}
						break;
					default:
						break;
				}
			}
			finally
			{
				_inColumnsChanged = false;
			}
		}

		void column_DataChanged( object sender, Column.ColumnDataChangedEventArgs e )
		{
			OnColumnDataChanged( e );

			if( _inColumnsChanged )
				return;
			_inColumnsChanged = true;
			try
			{
				switch( e.WhatChanged )
				{
					case Column.WhatPropertyChanged.IsGroup:
						if( e.Column.IsGrouped )
						{
							_groupedItems.Add( e.Column );
						}
						else
						{
							_groupedItems.Remove( e.Column );
						}
						break;
					case Column.WhatPropertyChanged.IsVisible:
						if( e.Column.IsVisible )
						{
							_visibleItems.Add( e.Column );
						}
						else
						{
							_visibleItems.Remove( e.Column );
						}
						break;
				}
			}
			finally
			{
				_inColumnsChanged = false;
			}
		}

		private void VisibleItems_DataChanged( object sender, EventingList<Column>.EventInfo e )
		{
			if( _inColumnsChanged )
				return;
			_inColumnsChanged = true;
			try
			{
				switch( e.EventType )
				{
					case EventingList<Column>.EventType.Deleted:
						foreach( Column c in e.Items )
						{
							c.IsVisible = false;
						}
						break;
					case EventingList<Column>.EventType.Added:
						foreach( Column c in e.Items )
						{
							c.IsVisible = true;
							if( c.MoveBehaviour == Column.MoveToGroupBehaviour.Move )
							{
								c.IsGrouped = false;
								_groupedItems.Remove( c );
							}
							if( this.IndexOf( c ) == -1 )
							{
								this.Add( c );
							}
						}
						break;
					default:
						break;
				}
			}
			finally
			{
				_inColumnsChanged = false;
			}
		}

		private void GroupedItems_DataChanged( object sender, EventingList<Column>.EventInfo e )
		{
			if( _inColumnsChanged )
				return;
			_inColumnsChanged = true;
			try
			{

				switch( e.EventType )
				{
					case EventingList<Column>.EventType.Deleted:
						foreach( Column c in e.Items )
						{
							c.IsGrouped = false;
						}
						break;
					case EventingList<Column>.EventType.Added:
						foreach( Column c in e.Items )
						{
							c.IsGrouped = true;
							if( c.MoveBehaviour == Column.MoveToGroupBehaviour.Move )
							{
								c.IsVisible = false;
							}
							if( this.IndexOf( c ) == -1 )
							{
								this.Add( c );
							}
						}
						break;
					default:
						break;
				}
			}
			finally
			{
				_inColumnsChanged = false;
			}
		}
		private EventingList<Column> _visibleItems = new EventingList<Column>();
		private EventingList<Column> _groupedItems = new EventingList<Column>();
		private bool _inColumnsChanged = false;
		#endregion
	}
}
