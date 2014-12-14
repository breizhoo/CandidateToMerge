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
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using BinaryComponents.Utility.Collections;
using BinaryComponents.SuperList.Sections;
using BinaryComponents.SuperList.ItemLists;
using BinaryComponents.Utility.Assemblies;

namespace BinaryComponents.SuperList
{
	public  class ListControl : SectionContainerControl
	{
		public ListControl( SectionFactory sectionFactory )
			: base( sectionFactory )
		{
			this.InitializeComponent();

			_itemList = new ItemLists.BufferedList();
			_itemList.ListControl = this;
			_selectedItems = new SelectedItemsCollection( this );

			_customiseListSection = this.SectionFactory.CreateCustomiseListSection( this );
			_listSection = this.SectionFactory.CreateListSection( this );

			this.Canvas.Children.Add( _customiseListSection );
			this.Canvas.Children.Add( _listSection );
		}

		public ListControl()
			: this( new SectionFactory() )
		{
		}
		public override SectionFactory SectionFactory
		{
			set
			{
				base.SectionFactory = value;

				this.Canvas.Children.Remove( _customiseListSection );
				this.Canvas.Children.Remove( _listSection );
				if( _customiseListSection != null )
				{
					_customiseListSection.Dispose();
				}
				bool headerShown = _listSection.ShowHeaderSection;
				_listSection.Dispose();

				if( this.ShowCustomizeSection )
				{
					_customiseListSection = this.SectionFactory.CreateCustomiseListSection( this );
					this.Canvas.Children.Add( _customiseListSection );
				}


				_listSection = this.SectionFactory.CreateListSection( this );
				_listSection.ShowHeaderSection = headerShown;
				this.Canvas.Children.Add( _listSection );
			}
		}

		public virtual void SerializeState( System.IO.TextWriter writer )
		{
			ListSection.SerializeState( writer );
		}

		public virtual void DeSerializeState( System.IO.TextReader reader )
		{
			ListSection.DeSerializeState( reader );
		}

		public void LayoutSections()
		{
			this.Canvas.Host.LazyLayout( this.Canvas );
		}

		public bool MultiSelect
		{
			get
			{
				return _listSection.AllowMultiSelect;
			}
			set
			{
				_listSection.AllowMultiSelect = value;
			}
		}

		public bool ShowHeaderSection
		{
			get
			{
				return _listSection.ShowHeaderSection;
			}
			set
			{
				_listSection.ShowHeaderSection = value;
			}
		}

		public bool ShowCustomizeSection
		{
			get
			{
				return _customiseListSection != null;
			}
			set
			{
				if( this.ShowCustomizeSection != value )
				{
					if( _customiseListSection == null )
					{
						_customiseListSection = this.SectionFactory.CreateCustomiseListSection( this );
						this.Canvas.Children.Insert( 0, _customiseListSection );
					}
					else
					{
						this.Canvas.Children.Remove( _customiseListSection );
						_customiseListSection.Dispose();
						_customiseListSection = null;
					}
					this.Canvas.Host.LazyLayout( this.Canvas );
				}
			}
		}
				
		public ColumnList Columns
		{
			get
			{
				return _columns;
			}
		}

		public ItemLists.ItemList Items
		{
			get
			{
				return _itemList;
			}
		}

		public SelectedItemsCollection SelectedItems
		{
			get
			{
				return  _selectedItems;
			}
		}

		public void EnsureVisible( object o )
		{
		}

		#region RowFocusChangedEvent
		public class RowFocusChangedEventArgs: EventArgs
		{
			public RowFocusChangedEventArgs( RowIdentifier oldFocus, RowIdentifier newFocus )
			{
				this.OldFocus = oldFocus;
				this.NewFocus = newFocus;
			}

			public readonly RowIdentifier OldFocus;
			public readonly RowIdentifier NewFocus;
		}
		public delegate void RowFocusChangedEventArgsHandler( object sender, RowFocusChangedEventArgs eventArgs );

		public event RowFocusChangedEventArgsHandler RowFocusChanged;
		protected virtual void OnRowFocusChanged( RowFocusChangedEventArgs eventArgs )
		{
			if( this.RowFocusChanged != null )
			{
				this.RowFocusChanged( this, eventArgs );
			}
		}
		#endregion 

		#region RowMouseClickedEvent
		public class RowEventArgs : EventArgs
		{
			public RowEventArgs( RowIdentifier row )
			{
				this.Row = row;
			}

			public readonly RowIdentifier Row;
		}
		public delegate void RowMouseClickedEventArgsHandler( object sender, RowEventArgs eventArgs );

		public event RowMouseClickedEventArgsHandler MouseClickedRow;
		protected virtual void OnMouseClickedRow( RowEventArgs eventArgs )
		{
			if( this.MouseClickedRow != null )
			{
				this.MouseClickedRow( this, eventArgs );
			}
		}
		#endregion 

		public object FocusedItem
		{
			get
			{
				return _listSection.FocusedItem == null ? null : _listSection.FocusedItem.RowIdentifier.Items[0];
			}
			set
			{
				if( value != null )
				{
					ListSection.PositionedRowIdentifier si = _listSection.GetPositionedIdentifierFromObject( new ListSection.NonGroupRow( value ) );

					if( si != null )
					{
						int pos = si.Position - 1;

						while( pos >= 0 )
						{
							RowIdentifier gi = _listSection.RowInformation[pos];

							if( gi is Sections.ListSection.GroupIdentifier )
							{
								_listSection.FocusedItem = new ListSection.PositionedRowIdentifier( gi, pos );

								--pos;
							}
							else
							{
								break;
							}
						}

						_listSection.FocusedItem = si;
					}
				}
				else
				{
					_listSection.FocusedItem = null;
				}
			}
		}

		#region Implementation

		public void PerformMouseWheel( int delta )
		{
			if( _listSection != null )
			{
				_listSection.MouseWheel( new MouseEventArgs( MouseButtons.None, 0, 0, 0, delta ) );
			}
		}

		protected override void OnMouseWheel( MouseEventArgs e )
		{
			base.OnMouseWheel( e );

			if( _listSection != null )
			{
				_listSection.MouseWheel( e );
			}
		}

		protected internal ItemLists.ItemList ItemList
		{
			get
			{
				return _itemList;
			}
		}

		protected override void OnHandleCreated( EventArgs e )
		{
			base.OnHandleCreated( e );

			_timer = new Timer();
			_timer.Interval = 50;
			_timer.Tick += new EventHandler( _timer_Tick );
			_timer.Start();
		}

		protected override void OnHandleDestroyed( EventArgs e )
		{
			base.OnHandleDestroyed( e );

			_timer.Tick -= new EventHandler( _timer_Tick );
			_timer.Stop();
			_timer.Dispose();
			_timer = null;
		}

		private void _timer_Tick( object sender, EventArgs e )
		{
			if( WinFormsUtility.Controls.ControlUtils.IsClientRectangleVisible( this, this.ClientRectangle ) )
			{
				this.ItemList.DoHouseKeeping();
			}
		}

		protected override bool IsInputKey( Keys keyData )
		{
			Keys[] keysWereInterestedIn = new Keys[]{ Keys.Space, Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.Multiply, Keys.Subtract };

			foreach( Keys key in keysWereInterestedIn )
			{
				if( ( keyData & key ) == key )
				{
					return true;
				}
			}
			return false;
		}

		public ListSection ListSection
		{
			get
			{
				return _listSection;
			}
		}

		internal void UpdateListSection( bool lazyLayout )
		{
			_listSection.ListUpdated( lazyLayout );
		}

		protected override void OnMouseClick( MouseEventArgs e )
		{
			base.OnMouseClick( e );

			Section section = this.SectionFromPoint( e.Location );
			while( section != null && !(section is RowSection) )
			{
				section = section.Parent;
			}

			if( section != null )
			{
				this.OnMouseClickedRow( new RowEventArgs( ((RowSection)section).RowIdentifier ) );
			}
		}


		private void InitializeComponent()
		{
			this.SuspendLayout();

			this.Name = "ListControl";
			this.ResumeLayout( false );
		}

		internal void FireFocusChanged( RowIdentifier oldFocusItem, RowIdentifier newFocusItem )
		{
			this.OnRowFocusChanged( new RowFocusChangedEventArgs( oldFocusItem, newFocusItem ) );
		}


		private Timer _timer;
		private ListSection _listSection;
		private Section _customiseListSection;
		private ItemLists.ItemList _itemList;
		private ColumnList _columns = new ColumnList();
		private SelectedItemsCollection _selectedItems;
		#endregion
	}
}
