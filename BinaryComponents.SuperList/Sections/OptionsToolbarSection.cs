/////////////////////////////////////////////////////////////////////////////
//
// (c) 2007 BinaryComponents Ltd.  All Rights Reserved.
//
// http://www.binarycomponents.com/
//
/////////////////////////////////////////////////////////////////////////////

using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Text;
using BinaryComponents.Utility.Assemblies;
using BinaryComponents.Utility.Collections;

namespace BinaryComponents.SuperList.Sections
{
	public abstract class OptionsToolbarSection : Section
	{
		protected OptionsToolbarSection( ListControl listControl )
			: base( listControl )
		{
		}

		public abstract int IdealWidth
		{
			get;
		}

		public abstract int MinimumWidth
		{
			get;
		}

		public abstract bool Visible
		{
			get;
			set;
		}
	}

	public class ToolStripOptionsToolbarSection : OptionsToolbarSection
	{
		public ToolStripOptionsToolbarSection( ListControl listControl )
			: base( listControl )
		{
			_listControl = listControl;

			_toolStrip = new ToolStrip();
			_toolStrip.AutoSize = true;
			_toolStrip.Visible = false;
			_toolStrip.Anchor = System.Windows.Forms.AnchorStyles.None;
			_toolStrip.Dock = System.Windows.Forms.DockStyle.None;
			_toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			_toolStrip.TabIndex = 0;
			_toolStrip.Text = "List options";
			_toolStrip.CanOverflow = true;
			_toolStrip.Renderer = new MyRenderer();
			_toolStrip.BackColor = Color.Transparent;
			_toolStrip.Layout += new LayoutEventHandler( _toolStrip_Layout );
			AddDefaultToolStripButtons();
			_toolStrip.CreateControl();

			listControl.Controls.Add( _toolStrip );

			listControl.Columns.GroupedItems.DataChanged += new EventHandler<EventingList<Column>.EventInfo>( GroupedItems_DataChanged );
			HandleEnablingGroupButtons();
		}

		public override void Dispose()
		{
			if( _columnButton != null )
			{
				_columnButton.Click -= new EventHandler( _columnButton_Click );
				_columnButton = null;
			}
			if( _collapseGroupButton != null )
			{
				_collapseGroupButton.Click -= new EventHandler( _collapseGroupButton_Click );
				_collapseGroupButton = null;
			}
			if( _expandGroupsButton != null )
			{
				_expandGroupsButton.Click -= new EventHandler( _expandGroupsButton_Click );
				_expandGroupsButton = null;
			}
			if( _toolStrip != null )
			{
				_toolStrip.Dispose();
				_toolStrip = null;
			}
		}

		public void AddToolStripItems( params ToolStripItem[] items )
		{
			this._toolStrip.Items.AddRange( items );
			_toolStrip.AutoSize = true;
		}

		public override int IdealWidth
		{
			get
			{
				return _toolStrip.PreferredSize.Width;
			}
		}

		public override int MinimumWidth
		{
			get
			{
				return 12;
			}
		}

		public override void Layout( Section.GraphicsSettings gs, Size maximumSize )
		{
			_toolStrip.MaximumSize = new Size( maximumSize.Width, 0 );
			this.Size = new Size(_toolStrip.Size.Width, maximumSize.Height );
		}

		public override Point Location
		{
			get
			{
				return base.Location;
			}
			set
			{
				base.Location = value;
				_toolStrip.Location = new Point( value.X, value.Y + (this.Size.Height - _toolStrip.Size.Height) / 2 );
			}
		}

		public override bool Visible
		{
			get
			{
				return _toolStrip.Visible;
			}
			set
			{
				_toolStrip.Visible = value;
			}
		}

		public ListSection ListSection
		{
			get
			{
				return ((ListControl)this.Host).ListSection;
			}
		}

		public void ShowColumnChooser( Point pt )
		{
			Helper.AvailableSectionsForm columnChooserForm = CreateAvailableSectionsForm();

			columnChooserForm.ColumnList = _listControl.Columns;
			columnChooserForm.Location = pt;
			columnChooserForm.Show( _listControl );
		}

		#region Default buttons

		protected virtual void AddDefaultToolStripButtons()
		{
			ManifestResources res = new ManifestResources( "BinaryComponents.SuperList.Resources" );

			_columnButton = new ToolStripButton();
			_columnButton.Name = "_columnButton";
			_columnButton.Image = res.GetIcon( "ColumnsButton.ico" ).ToBitmap();
			_columnButton.Text = "Columns";
			_columnButton.Click += new EventHandler( _columnButton_Click );
			_columnButton.ToolTipText = "Click to display available columns that you can either group on or show in the actual list";
			_columnButton.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;

			_collapseGroupButton = new ToolStripButton();
			_collapseGroupButton.Visible = true;
			_collapseGroupButton.Name = "collapseAllGroupsButton";
			_collapseGroupButton.Image = res.GetIcon( "CollapseAllGroupsButton.ico" ).ToBitmap();
			_collapseGroupButton.Click += new EventHandler( _collapseGroupButton_Click );
			_collapseGroupButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			_collapseGroupButton.ToolTipText = "Click to collapse all groups within the list";

			_expandGroupsButton = new ToolStripButton();
			_expandGroupsButton.Visible = true;
			_expandGroupsButton.Name = "expandAllGroupsButton";
			_expandGroupsButton.Image = res.GetIcon( "ExpandAllGroupsButton.ico" ).ToBitmap();
			_expandGroupsButton.Click += new EventHandler( _expandGroupsButton_Click );
			_expandGroupsButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			_expandGroupsButton.ToolTipText = "Click to expand all groups within the list";

			AddToolStripItems( new ToolStripItem[] { _columnButton, _collapseGroupButton, _expandGroupsButton } );
		}

		private void _columnButton_Click( object sender, EventArgs e )
		{
			ShowColumnChooser( new Point( Cursor.Position.X, Rectangle.Bottom + _listControl.PointToScreen( this.Location ).Y ) );
		}

		private void _collapseGroupButton_Click( object sender, EventArgs e )
		{
			this.ListSection.SetGlobalGroupState( ListSection.GroupState.Collapsed );
		}

		private void _expandGroupsButton_Click( object sender, EventArgs e )
		{
			this.ListSection.SetGlobalGroupState( ListSection.GroupState.Expanded );
		}

		#endregion

		protected virtual Helper.AvailableSectionsForm CreateAvailableSectionsForm()
		{
			return new Helper.AvailableSectionsForm();
		}

		private void _toolStrip_Layout( object sender, LayoutEventArgs e )
		{
			this.Host.LazyLayout( null );
		}

		private void GroupedItems_DataChanged( object sender, EventingList<Column>.EventInfo e )
		{
			HandleEnablingGroupButtons();
		}

		private void HandleEnablingGroupButtons()
		{
			if( _collapseGroupButton != null )
			{
				_collapseGroupButton.Enabled = _listControl.Columns.GroupedItems.Count > 0;
			}
			if( _expandGroupsButton != null )
			{
				_expandGroupsButton.Enabled = _listControl.Columns.GroupedItems.Count > 0;
			}
		}

		private class MyRenderer : ToolStripProfessionalRenderer
		{
			public MyRenderer()
			{
			}

			protected override void OnRenderToolStripBorder( ToolStripRenderEventArgs e )
			{
				if( e.ToolStrip is ToolStripOverflow )
				{
					base.OnRenderToolStripBorder( e );
				}
			}
		}

		private delegate void ButtonClicked();

		private ToolStrip _toolStrip;
		private ToolStripButton _collapseGroupButton;
		private ToolStripButton _expandGroupsButton;
		private ToolStripButton _columnButton;
		private ListControl _listControl;
	}
}
