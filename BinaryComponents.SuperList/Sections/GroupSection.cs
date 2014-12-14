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
using System.Collections.Generic;
using System.Text;
using BinaryComponents.Utility.Assemblies;

namespace BinaryComponents.SuperList.Sections
{
	public class GroupSection: RowSection
	{
		public GroupSection( ListControl listControl, RowIdentifier ri, HeaderSection headerSecton, int position, int groupIndentWidth  )
			: base( listControl, ri, headerSecton, position )
		{
			System.Diagnostics.Debug.Assert( ri is ListSection.GroupIdentifier );
			_groupIndentWidth = groupIndentWidth;
		}

		public override void Layout( Section.GraphicsSettings gs, System.Drawing.Size maximumSize )
		{
			SizeF size =	gs.Graphics.MeasureString( this.Text, Font );
			this.Size = new Size( this.HeaderSection.Rectangle.Width, (int)size.Height + _margin + _separatorLineHeight * 2 );
		}

		public override void Paint( Section.GraphicsSettings gs, System.Drawing.Rectangle clipRect )
		{
			Rectangle rcText;
			this.GetDrawRectangles( out rcText, out _buttonRectangle );
			Rectangle rc = this.HostBasedRectangle;

			//
			// Fill indent area if any
			if( _groupIndentWidth > 0 )
			{
				Rectangle rcIndent = new Rectangle( rc.X, rc.Y, _groupIndentWidth, rc.Height );

				PaintIndentArea( gs.Graphics, rcIndent );
			}

			gs.Graphics.DrawIcon( this.DrawIcon, _buttonRectangle.X, _buttonRectangle.Y );

			WinFormsUtility.Drawing.GdiPlusEx.DrawString
				( gs.Graphics, Text, Font, ( this.Host.FocusedSection == this.ListSection && this.IsSelected ) ? SystemColors.HighlightText : SystemColors.WindowText, rcText
				, WinFormsUtility.Drawing.GdiPlusEx.TextSplitting.SingleLineEllipsis, WinFormsUtility.Drawing.GdiPlusEx.Ampersands.Display );

			PaintSeparatorLine( gs.Graphics, rcText );
		}

		public void GetDrawRectangles( out Rectangle textRectangle, out Rectangle buttonRectangle )
		{
			int spacing = 2;
			Rectangle rc = this.HostBasedRectangle;


			rc.X += _groupIndentWidth + spacing;
			rc.Height -= _margin;
			rc.Y += _margin;
			buttonRectangle = new Rectangle( rc.X, rc.Y, this.DrawIcon.Width, this.DrawIcon.Height );
			rc.X += this.DrawIcon.Width + spacing;

			textRectangle = new Rectangle( rc.X, rc.Y - 1, rc.Width, rc.Height );
		}

		private Icon DrawIcon
		{
			get
			{
				Icon drawIcon = null;
				switch( this.GroupState )
				{
					case ListSection.GroupState.Collapsed:
						drawIcon = _resources.ExpandIcon;
						break;
					case ListSection.GroupState.Expanded:
						drawIcon = _resources.CollapseIcon;
						break;
				}
				return drawIcon;
			}
		}

		public override void MouseDown( System.Windows.Forms.MouseEventArgs e )
		{
			if( _buttonRectangle.Contains( new Point( e.X, e.Y ) ) )
			{
				this.GroupState = this.GroupState == ListSection.GroupState.Expanded ? ListSection.GroupState.Collapsed : ListSection.GroupState.Expanded;
			}
			base.MouseDown( e );
		}

		public override void Dispose()
		{
			base.Dispose();
		}

		protected override int IndentWidth
		{
			get
			{
				return _groupIndentWidth;
			}
		}

		protected virtual string Text
		{
			get
			{
				int descendentCount = this.RowIdentifier.Items.Length;
				return string.Format( "{0} ({1} {2})",
					this.RowIdentifier.ColumnGroup.GroupItemAccessor( this.Item ).ToString(),
					descendentCount,
					descendentCount == 1 ? "Item" : "Items" 
				);
			}
		}

		protected virtual Font Font
		{
			get
			{
				return Host.Font;
			}
		}

		protected override void PaintSeparatorLine( Graphics g, Rectangle rc )
		{
			using( Pen pen = new Pen( Color.FromArgb( 123, 164, 224 ), _separatorLineHeight ) )
			{
				g.DrawLine( pen, new Point( rc.Left, rc.Bottom - _separatorLineHeight ), new Point( rc.Right, rc.Bottom - _separatorLineHeight ) );
			}
		}

		protected ListSection.GroupState GroupState
		{
			get
			{
				return this.ListSection.GetGroupState( this.RowIdentifier );
			}
			set
			{
				this.ListSection.SetGroupState( this.RowIdentifier, value, true );
			}
		}

		#region Resources
		/// <summary>
		/// Class that shares out the insertion window resources
		/// </summary>
		private class Resources : IDisposable
		{
			public Resources()
			{
				_referencesCount++;
			}

			public void Dispose()
			{
				if( !_disposed )
				{
					if( --_referencesCount == 0 )
					{
						if( _collapseIcon != null )
						{
							_collapseIcon.Dispose();
							_collapseIcon = null;
						}
						if( _expandIcon != null )
						{
							_expandIcon.Dispose();
							_expandIcon = null;
						}
					}
					System.Diagnostics.Debug.Assert( _referencesCount >= 0 );
					_disposed = true;
				}
			}

			public Icon CollapseIcon
			{
				get
				{
					if( _collapseIcon == null )
					{
						_collapseIcon = _resources.GetIcon( "CollapseGroup.ico" );
					}
					return _collapseIcon;
				}
			}

			public Icon ExpandIcon
			{
				get
				{
					if( _expandIcon == null )
					{
						_expandIcon = _resources.GetIcon( "ExpandGroup.ico" );
					}
					return _expandIcon;
				}
			}

			private bool _disposed = false;
			private static int _referencesCount = 0;
			private static Icon _expandIcon = null;
			private static Icon _collapseIcon = null;
			private ManifestResources _resources = new ManifestResources( "BinaryComponents.SuperList.Resources" );
		}
		#endregion

		const int _separatorLineHeight = 2;
		const int _margin = 7;
		Rectangle _buttonRectangle;
		private int _groupIndentWidth;
		private static Resources _resources = new Resources();
	}
}
