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

namespace BinaryComponents.SuperList
{
	public class SectionContainerControl: UserControl, ISectionHost
	{
		public SectionContainerControl( Sections.SectionFactory sectionFactory  )
		{
			this.InitializeComponent();

			this.SetStyle( ControlStyles.AllPaintingInWmPaint, true );
			this.SetStyle( ControlStyles.DoubleBuffer, true );
			this.SetStyle( ControlStyles.ResizeRedraw, true );
			this.SetStyle( ControlStyles.SupportsTransparentBackColor, true );

			_canvas = this.CreateCanvas();

			if( sectionFactory == null )
			{
				_sectionFactory = new Sections.SectionFactory();
			}
			else
			{
				_sectionFactory = sectionFactory;
			}
		}

		public SectionContainerControl()
			: this( null )
		{
		}

		public virtual void LayoutControl()
		{
			StopLazyUpdateTimer();
			_canvas.Location = Point.Empty;
			using( Graphics grfx = this.CreateGraphics() )
			{
				_canvas.Layout( new Section.GraphicsSettings( grfx ), this.ClientRectangle.Size );
			}
			this.Invalidate();
		}

		public virtual Sections.SectionFactory SectionFactory
		{
			get
			{
				return _sectionFactory;
			}
			set
			{
				_sectionFactory = value;
			}
		}


		protected virtual SectionContainer CreateCanvas()
		{
			return new SectionContainer( this );
		}

		protected SectionContainer Canvas
		{
			get
			{
				return _canvas;
			}
		}

		#region Implementation

		#region ISectionHost Members

		void ISectionHost.Invalidate( Section section )
		{
			this.Invalidate( section.HostBasedRectangle );
		}

		Section ISectionHost.SectionFromClientPoint( Point ptClient )
		{
			return this.Canvas.SectionFromPoint( ptClient );
		}


		Point ISectionHost.PointToScreen( Point pt )
		{
			return this.PointToScreen( pt );
		}
		Point ISectionHost.PointToClient( Point pt )
		{
			return this.PointToClient( pt );
		}
		bool ISectionHost.IsControlCreated
		{
			get
			{
				return this.IsHandleCreated;
			}
		}

		void ISectionHost.DoDragDropOperation( Section sectionToDrag )
		{
			_draggingSection = sectionToDrag;

			_imageWindowOffX = sectionToDrag.HostBasedRectangle.X - CursorClientPosition.X;
			_imageWindowOffY = sectionToDrag.HostBasedRectangle.Y - CursorClientPosition.Y;
			
			_imageWindow = CreateDragImageWindow( sectionToDrag );

			if( this.DoDragDrop( sectionToDrag, DragDropEffects.Copy | DragDropEffects.Move ) == DragDropEffects.None )
			{
				Point cursorPos = this.PointToClient( Cursor.Position );
				if( _lastDragAction == DragAction.Drop && _draggingSection.CanDropInVoid && !_draggingSection.HostBasedRectangle.Contains( cursorPos ) )
				{
					_draggingSection.DroppedInVoid();
				}
			}
			FinishDropOperation();
		}


		void ISectionHost.StartMouseCapture( Section section )
		{
			if( _sectionWithMouseCapture != null )
			{
				_sectionWithMouseCapture.CancelMouseCapture();
			}
			_sectionWithMouseCapture = section;
			this.Capture = true;
		}

		void ISectionHost.EndMouseCapture()
		{
			_sectionWithMouseCapture = null;
			this.Capture = false;
		}

		Section ISectionHost.SectionWithMouseCapture
		{
			get
			{
				return _sectionWithMouseCapture;
			}
		}

		Cursor ISectionHost.Cursor
		{
			get
			{
				return this.Cursor;
			}
			set
			{
				this.Cursor = value;
			}
		}

		bool ISectionHost.IsInDragOperation
		{
			get
			{
				return _draggingSection != null;
			}
		}

		Control.ControlCollection ISectionHost.ControlCollection
		{
			get
			{
				return this.Controls;
			}
		}

		Graphics ISectionHost.CreateGraphics()
		{
			return this.IsHandleCreated ? CreateGraphics() : null;
		}
		Font ISectionHost.Font
		{
			get
			{
				return this.Font;
			}
		}
		Color ISectionHost.TextColor
		{
			get
			{
				return this.ForeColor;
			}
		}

		void ISectionHost.LazyLayout( Section s )
		{
			if( !this.IsHandleCreated )
				return;

			if( s == null )
			{
				s = _canvas;
			}

			bool found = false;
			if( s == _canvas )
			{
				_lazyLayouts.Clear();
			}
			else
			{
				//
				// Check to see if this section or any of its parents are going to be laid out.
				for( Section sectionToCheck = s; sectionToCheck != null; sectionToCheck = sectionToCheck.Parent )
				{
					if( _lazyLayouts.Contains( sectionToCheck ) )
					{
						found = true;
						break;
					}
				}
			}
			if( !found )
			{
				_lazyLayouts.Add( s );
			}
			StartLazyUpdateTimer();
		}

		protected override void OnGotFocus( EventArgs e )
		{
			base.OnGotFocus( e );

			if( _focusedSection != null )
			{
				_focusedSection.GotFocus();
			}

			this.Invalidate();
		}

		protected override void OnLostFocus( EventArgs e )
		{
			base.OnLostFocus( e );

			if( _focusedSection != null )
			{
				_focusedSection.LostFocus();
			}

			this.Invalidate();
		}

		Section ISectionHost.SectionMouseOver
		{
			get
			{
				return _sectionMouseOver;
			}
		}
		Section ISectionHost.FocusedSection
		{
			get
			{
				if( this.Focused )
				{
					if( _sectionWithMouseCapture != null )
					{
						return _sectionWithMouseCapture;
					}
					else
					{
						return _focusedSection;
					}
				}
				else
				{
					return null;
				}
			}
			set
			{
				if( _focusedSection != value )
				{
					Section oldFocusedSection = _focusedSection;
					
					_focusedSection = value;

					if( oldFocusedSection != null )
					{
						oldFocusedSection.LostFocus();
					}
					if( _focusedSection != null )
					{
						_focusedSection.GotFocus();
					}
				}
			}
		}

		object ISectionHost.Tag
		{
			get
			{
				return this.Tag;
			}
		}

		#endregion

		#region Mouse Handling

		protected override void OnMouseCaptureChanged( EventArgs e )
		{
			base.OnMouseCaptureChanged( e );
			if( _sectionWithMouseCapture != null )
			{
				_sectionWithMouseCapture.CancelDrag();
			}
		}

		protected override void OnMouseDown( MouseEventArgs e )
		{
			base.OnMouseDown( e );

			Section s = this.SectionFromPoint( new Point( e.X, e.Y ) );
			this.SectionMouseOver = s;
			if( s != null )
			{
				s.MouseDown( e );
				_sectionMouseButtonPressed = s;
			}
		}

		protected override void OnMouseUp( MouseEventArgs e )
		{
			base.OnMouseUp( e );

			if( _sectionMouseButtonPressed != null )
			{
				_sectionMouseButtonPressed.MouseUp( e );
				_sectionMouseButtonPressed = null;
			}
		}

		protected override void OnMouseMove( MouseEventArgs e )
		{
			base.OnMouseMove( e );
			Point mousePosition = new Point( e.X, e.Y );
			Section s = this.SectionFromPoint( mousePosition );
			this.SectionMouseOver = s;
			if( s != null )
			{
				s.MouseMove( mousePosition, e );
			}
		}

		protected override void OnMouseLeave( EventArgs e )
		{
			base.OnMouseLeave( e );
			this.SectionMouseOver = null;
		}
		#endregion

		#region Keyboard Handling
	
		protected override void OnKeyDown( KeyEventArgs e )
		{
			base.OnKeyDown( e );

			Section focusedSection = ((ISectionHost)this).FocusedSection;

			if( focusedSection != null )
			{
				focusedSection.KeyDown( e );
			}
		}
		#endregion

		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
			if( disposing )
			{
				if( _canvas != null )
				{
					_canvas.Dispose();
					_canvas = null;
				}

				StopLazyUpdateTimer();
			}
		}

		protected override void OnHandleCreated( EventArgs e )
		{
			base.OnHandleCreated( e );
			this.LayoutControl();
		}

		protected override void OnSizeChanged( EventArgs e )
		{
			base.OnSizeChanged( e );
			if( IsHandleCreated )
			{
				this.LayoutControl();
			}
		}

		protected override void OnPaint( PaintEventArgs e )
		{
			//System.Diagnostics.Debug.WriteLine( string.Format( "Painting: W{0} CR{1}", this.ClientRectangle, e.ClipRectangle ) );
			Section.GraphicsSettings gs = new Section.GraphicsSettings( e.Graphics );
			_canvas.PaintBackground( gs, e.ClipRectangle );
			_canvas.Paint( gs, e.ClipRectangle );
		}

		private Section SectionMouseOver
		{
			get
			{
				return _sectionMouseOver;
			}
			set
			{
				if( _sectionMouseOver != value )
				{
					if( _sectionMouseOver != null )
					{
						_sectionMouseOver.MouseLeave();
					}

					_sectionMouseOver = value;

					if( _sectionMouseOver != null )
					{
						_sectionMouseOver.MouseEnter();
					}
				}
			}
		}

		private void StartLazyUpdateTimer()
		{
			if( this.IsHandleCreated )
			{
				if( _lazyUpdateTimer == null )
				{
					_lazyUpdateTimer = new Timer();
					_lazyUpdateTimer.Interval = 100;
					_lazyUpdateTimer.Tick += new EventHandler( LazyUpdateTimerTick );
					_lazyUpdateTimer.Start();
				}
			}
		}

		protected void StopLazyUpdateTimer()
		{
			if( _lazyUpdateTimer != null )
			{
				_lazyUpdateTimer.Tick -= new EventHandler( LazyUpdateTimerTick );
				_lazyUpdateTimer.Stop();
				_lazyUpdateTimer.Dispose();
				_lazyUpdateTimer = null;
			}
		}


		void LazyUpdateTimerTick( object sender, EventArgs e )
		{

			if( _lazyLayouts.Count == 1 && _lazyLayouts.ToArray()[0] == _canvas )
			{
				this.LayoutControl();
				_lazyLayouts.Clear();
			}
			else
			{
				using( Graphics grfx = this.CreateGraphics() )
				{
					Section.GraphicsSettings grfxSettings = new Section.GraphicsSettings( grfx );
					Section []sectionsToLayout = _lazyLayouts.ToArray();
					_lazyLayouts.Clear();
					foreach( Section s in sectionsToLayout )
					{
						s.Layout( grfxSettings, s.Size );
						s.Invalidate();
					}
				}
			}
			this.StopLazyUpdateTimer();
		}

		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// SectionContainerControl
			// 
			this.AllowDrop = true;
			this.Name = "SectionContainerControl";

			this.ResumeLayout( false );
		}

		#region Drag Drop

		protected override void OnDragDrop( DragEventArgs e )
		{
			base.OnDragDrop( e );

			Point dropPoint = this.PointToClient( new Point( e.X, e.Y ) );
			Section s = this.GetDroppableSection( dropPoint, e.Data );

			if( s != null )
			{
				s.Drop( e, dropPoint );
			}
		}


		protected override void OnDragLeave( EventArgs e )
		{
			base.OnDragLeave( e );

			if( _currentDropSection != null )
			{
				_currentDropSection.DragLeave();
				_currentDropSection = null;
			}
		}

		protected override void OnDragOver( DragEventArgs e )
		{
			base.OnDragOver( e );

			Point pt = this.PointToClient( new Point( e.X, e.Y ) );
			Section s = this.GetDroppableSection( pt, e.Data );

			if( _currentDropSection != null && s != _currentDropSection )
			{
				_currentDropSection.DragLeave();
			}
			_currentDropSection = s;
			if( s != null )
			{
				bool canDrop = s != null && s.CanDrop( e.Data );
				if( canDrop )
				{
					e.Effect = e.AllowedEffect;
					s.DragingOver( pt, e.Data );
					return;
				}
			}
			e.Effect = DragDropEffects.None;
		}

		protected override void OnGiveFeedback( GiveFeedbackEventArgs gfbevent )
		{
			base.OnGiveFeedback( gfbevent );

			Point cursorPos = Cursor.Position;
			if( _imageWindow != null )
			{
				_imageWindow.Location = new Point( cursorPos.X + _imageWindowOffX, cursorPos.Y + _imageWindowOffY );
				if( !_imageWindow.Visible )
				{
					_imageWindow.Show();
				}
			}
		}

		protected override void OnQueryContinueDrag( QueryContinueDragEventArgs e )
		{
			base.OnQueryContinueDrag( e );
			
			_lastDragAction = e.Action;
		}



		private enum CursorType { CanDrag, CantDrag };
		private void FinishDropOperation()
		{
			if( _currentDropSection != null )
			{
				_currentDropSection.DragLeave();
				_currentDropSection = null;
			}

			if( _sectionMouseButtonPressed != null )
			{
				_sectionMouseButtonPressed.MouseUp( new MouseEventArgs( MouseButtons.Left, 1, 0, 0, 0 ) );
				_sectionMouseButtonPressed = null;
			}
			_draggingSection = null;
			this.Cursor = Cursors.Arrow;
			if( _imageWindow!= null )
			{
				_imageWindow.Dispose();
				_imageWindow = null;
			}
			_currentDropSection = null;
		}

		private static Helper.ImageWindow CreateDragImageWindow( Section section )
		{
			Rectangle rcSection = _draggingSection.HostBasedRectangle;
			using( Bitmap bmpOfSection = new Bitmap( rcSection.Width, rcSection.Height ) )
			using( Bitmap finalBmp = new Bitmap( bmpOfSection.Width, bmpOfSection.Height ) )
			using( Graphics grfxBmpOfSection = Graphics.FromImage( bmpOfSection ) )
			using( Graphics grfxFinalBmp = Graphics.FromImage( finalBmp ) )
			{
				//
				// Draw section into its bmp
				grfxBmpOfSection.TranslateTransform( -rcSection.X, -rcSection.Y );

				grfxBmpOfSection.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

				_draggingSection.Paint( new Section.GraphicsSettings( grfxBmpOfSection ), rcSection );

				//
				// Now draw a transparent verison of it onto the final bmp
				ImageAttributes ia = new ImageAttributes();
				ColorMatrix cm = new ColorMatrix();
				cm.Matrix33 = 0.7f;
				ia.SetColorMatrix( cm );
				grfxFinalBmp.DrawImage( bmpOfSection,
					new Rectangle( 0, 0, bmpOfSection.Width, bmpOfSection.Height ),
					0,
					0,
					finalBmp.Width,
					finalBmp.Height,
					GraphicsUnit.Pixel,
					ia );

				//
				// Make the cursor from our final bmp
				return new Helper.ImageWindow( Icon.FromHandle( finalBmp.GetHicon() ) );
			}
		}


		private Point CursorClientPosition
		{
			get
			{
				return this.PointToClient( Cursor.Position );
			}
		}

		#endregion

		private HeaderSection HeaderSection
		{
			get
			{
				return (HeaderSection)_canvas.Children[1];
			}
		}

		private Section GetDroppableSection( Point pt, IDataObject dataObject )
		{
			for( Section s = this.SectionFromPoint( pt ); s != null; s = s.Parent )
			{
				if( s.CanDrop( dataObject ) )
				{
					return s;
				}
			}
			return null;
		}

		protected Section SectionFromPoint( Point pt )
		{
			if( _sectionWithMouseCapture != null )
			{
				return _sectionWithMouseCapture;
			}
			else
			{
				return _canvas.SectionFromPoint( pt );
			}
		}
		
		private Section _focusedSection = null;
		private Section _sectionWithMouseCapture = null;
		private static DragAction _lastDragAction = DragAction.Cancel;
		private static Section _draggingSection = null;
		private Helper.ImageWindow _imageWindow = null;
		private int _imageWindowOffX = 1, _imageWindowOffY = 1;
		private static Section _currentDropSection = null;

		private Set<Section> _lazyLayouts = new Set<Section>();
		private Timer _lazyUpdateTimer = null;
		private Section _sectionMouseButtonPressed = null;
		private Section _sectionMouseOver = null;
		private SectionContainer _canvas = null;
		private SectionFactory _sectionFactory;
		#endregion
	}
}
