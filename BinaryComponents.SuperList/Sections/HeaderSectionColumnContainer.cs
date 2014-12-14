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
using BinaryComponents.Utility.Assemblies;

namespace BinaryComponents.SuperList.Sections
{
	public class HeaderColumnSectionContainer : SectionContainer
	{
		public HeaderColumnSectionContainer( ISectionHost hostControl, EventingList<Column> columns )
			: base( hostControl )
		{
			_columns = columns;
		}


		public override void Dispose()
		{
			base.Dispose();
			if( _insertionArrows != null )
			{
				_insertionArrows.Dispose();
				_insertionArrows = null;
			}
		}

		public interface ILayoutController
		{
			int ReservedNearSpace { get; }
			void HeaderLayedOut();
			int CurrentHorizontalScrollPosition { get; set; }
		}

		public virtual SortOrder GetColumnSortOrder( Column column )
		{
			return column.SortOrder;
		}

		public virtual void  SetColumnSortOrder( Column column, SortOrder sortOrder )
		{
			column.SortOrder = sortOrder;
		}

		public virtual bool AllowColumnsToBeDroppedInVoid
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// Called by the list to reserve space for the grouping rows.
		/// </summary>
		public ILayoutController LayoutController
		{
			get
			{
				return _layoutController;
			}
			set
			{
				_layoutController = value;
			}
		}

		private HeaderColumnSection GetHeaderColumnSectionFromDragData( IDataObject dataObject )
		{
			string[] formats = dataObject.GetFormats();

			foreach( string format in formats )
			{
				HeaderColumnSection hcs = dataObject.GetData( format ) as HeaderColumnSection;

				if( hcs != null )
				{
					return hcs;
				}
			}
			return null;
		}

		public override void Drop( DragEventArgs eventArgs, Point dropPoint )
		{
			HeaderColumnSection hcs = GetHeaderColumnSectionFromDragData( eventArgs.Data );

			HeaderColumnSection before;
			HeaderColumnSection after;

			this.GetDropBoundsSections( this.ToWorldPoint( dropPoint ), out before, out after );

			int insertionIndex = 0;

			if( hcs.Parent != this || hcs.Parent.Columns.IndexOf( hcs.Column ) != insertionIndex - 1 )
			{
				if( hcs.Parent.ShouldRemoveColumnOnDrop( hcs.Column ) )
				{
					hcs.Parent.Columns.Remove( hcs.Column );
				}
				if( before != null )
				{
					insertionIndex = this.Columns.IndexOf( before.Column ) + 1;
				}

				int columnIndexFound = this.Columns.IndexOf( hcs.Column );

				if( columnIndexFound != -1 )
				{
					this.Columns.RemoveAt( columnIndexFound );
					if( insertionIndex > columnIndexFound )
					{
						insertionIndex--;
					}
				}
				this.Columns.Insert( insertionIndex, hcs.Column );
			}
		}

		public virtual bool ShouldRemoveColumnOnDrop( Column column )
		{
			return column.MoveBehaviour == Column.MoveToGroupBehaviour.Move;
		}

		public override void DroppedInVoid()
		{
			base.DroppedInVoid();
		}

		public override void DragingOver( Point pt, IDataObject objectToDrop )
		{
			if( this.CanDrop( objectToDrop ) && GetHeaderColumnSectionFromDragData( objectToDrop ) != null )
			{
				this.PositionDropWindows( this.ToWorldPoint( pt ) );
			}
		}

		public override void DragLeave()
		{
			this.ShowingMouseDropPoint = false;
		}

		public override bool CanDrop( IDataObject objectToDrop )
		{
			bool canDrop = GetHeaderColumnSectionFromDragData( objectToDrop ) != null;

			if( canDrop )
			{
				this.ShowingMouseDropPoint = true;
			}
			return canDrop;
		}

		public EventingList<Column> Columns
		{
			get
			{
				return _columns;
			}
		}

		protected virtual HeaderColumnSection CreateHeaderColumnSection( HeaderColumnSection.DisplayMode displayModeToCreate, Column column )
		{
			return this.Host.SectionFactory.CreateHeaderColumnSection( this.Host, displayModeToCreate, column );
		}
		
		protected void SyncSectionsToColumns( HeaderColumnSection.DisplayMode displayModeToCreate )
		{
			List<HeaderColumnSection> newHeaderList = new List<HeaderColumnSection>();
			Dictionary<Column, HeaderColumnSection> columnToHcs = new Dictionary<Column, HeaderColumnSection>();

			foreach( Section s in this.Children )
			{
				HeaderColumnSection hcs = s as HeaderColumnSection;

				if( hcs != null )
				{
					columnToHcs[hcs.Column] = hcs;
				}
			}

			//
			// Add  additions
			foreach( Column column in _columns )
			{
				HeaderColumnSection hcs;

				if( !columnToHcs.TryGetValue( column, out hcs ) )
				{
					hcs = this.CreateHeaderColumnSection( displayModeToCreate, column );
				}
				newHeaderList.Add( hcs );
			}

			//
			// Remove any ones that doesn't exists any more
			foreach( Section s in this.Children )
			{
				HeaderColumnSection hcs = s as HeaderColumnSection;

				if( hcs != null )
				{
					if( newHeaderList.IndexOf( hcs ) == -1 )
					{
						hcs.Dispose();
					}
				}
			}

			this.Children.Clear();
			this.Children.AddRange( newHeaderList.ToArray() );
		}

		public bool ShowingMouseDropPoint
		{
			get
			{
				return _showingDropPoint;
			}
			set
			{
				if( _showingDropPoint != value )
				{
					if( _showingDropPoint )
					{
						_insertionArrows.UpArrowWindow.Visible = false;
						_insertionArrows.DownArrowWindow.Visible = false;
					}
					_showingDropPoint = value;
				}
			}
		}

		protected void GetDropBoundsSections( Point pt, out HeaderColumnSection before, out HeaderColumnSection after )
		{
			before = null;
			after = null;

			foreach( HeaderColumnSection s in this.Children )
			{
				if( pt.X > s.Rectangle.Right && (before == null || s.Rectangle.Right > before.Rectangle.Right) )
				{
					before = s;
				}
				if( pt.X < s.Rectangle.Right && (after == null || s.Rectangle.Right < after.Rectangle.Right) )
				{
					after = s;
				}
			}
		}

		private void PositionDropWindows( Point pt )
		{
			//
			// Find the before and after sections from the point given
			HeaderColumnSection before;
			HeaderColumnSection after;

			this.GetDropBoundsSections( pt, out before, out after );

			//
			// Calculate the x position of the insertion arrows
			int xPos;

			if( before != null && after != null )
			{
				xPos = before.HostBasedRectangle.Right + (after.HostBasedRectangle.Left - before.HostBasedRectangle.Right) / 2;
			}
			else if( before == null )
			{
				xPos = this.HostBasedRectangle.Left;
				if( after != null )
				{
					xPos += (after.HostBasedRectangle.Left - this.HostBasedRectangle.Left) / 2;
				}
			}
			else 
			{
				xPos = before.HostBasedRectangle.Right;
			}

			//
			// Calculate the y position of the insertion arrows
			int top;
			int bottom;

			if( after != null )
			{
				top = after.Rectangle.Top;
				bottom = after.Rectangle.Bottom;
			}
			else if( before != null )
			{
				top = before.Rectangle.Top;
				bottom = before.Rectangle.Bottom;
			}
			else
			{
				top = this.Rectangle.Top;
				bottom = this.Rectangle.Bottom;
			}

			//
			// Now we have all the info actally position them.
			Helper.ImageWindow downArrowWindow = _insertionArrows.DownArrowWindow;
			Helper.ImageWindow upArrowWindow = _insertionArrows.UpArrowWindow;
			Point downArrowPoint = new Point( xPos - downArrowWindow.Width / 2, top - downArrowWindow.Height );

			if( this.LayoutController != null )
			{
				if( xPos < 0 )
				{
					this.LayoutController.CurrentHorizontalScrollPosition = xPos + this.GetAbsoluteScrollCoordinates().X;
				}
			}

			downArrowWindow.Location = this.Host.PointToScreen(
				downArrowPoint
				);

			Point upArrowPoint = new Point( xPos - upArrowWindow.Width / 2, bottom );
			upArrowWindow.Location = this.Host.PointToScreen( upArrowPoint );

			//
			// Finally make them 
			if( !upArrowWindow.Visible )
			{
				upArrowWindow.Visible = true;
			}
			if( !downArrowWindow.Visible )
			{
				downArrowWindow.Visible = true;
			}
		}

		#region InsertArrowResources
		/// <summary>
		/// Class that shares out the insertion window resources
		/// </summary>
		private class InsertArrowResources: IDisposable
		{
			public InsertArrowResources()
			{
				_referencesCount++;
			}

			public void Dispose()
			{
				if( !_disposed )
				{
					if( --_referencesCount == 0 )
					{
						if( _upArrowWindow != null )
						{
							_upArrowWindow.Close();
							_upArrowWindow = null;
						}
						if( _downArrowWindow != null )
						{
							_downArrowWindow.Close();
							_downArrowWindow = null;
						}
					}
					System.Diagnostics.Debug.Assert( _referencesCount >= 0 );
					_disposed = true;
				}
			}

			public Helper.ImageWindow UpArrowWindow
			{
				get
				{
					if( _upArrowWindow == null )
					{
						_upArrowWindow = new Helper.ImageWindow( _resources.GetIcon( "UpArrow.ico" ) );
					}
					return _upArrowWindow;
				}
			}

			public Helper.ImageWindow DownArrowWindow
			{
				get
				{
					if( _downArrowWindow == null )
					{
						_downArrowWindow = new Helper.ImageWindow( _resources.GetIcon( "DownArrow.ico" ) );
					}
					return _downArrowWindow;
				}
			}

			private bool _disposed = false;
			private static int _referencesCount = 0;
			private static Helper.ImageWindow _upArrowWindow = null;
			private static Helper.ImageWindow _downArrowWindow = null;
			private ManifestResources _resources = new ManifestResources( "BinaryComponents.SuperList.Resources" );
		}
		#endregion

		private InsertArrowResources _insertionArrows = new InsertArrowResources();
		private bool _showingDropPoint = false;
		private EventingList<Column> _columns;
		private ILayoutController _layoutController = null;
	}
}
