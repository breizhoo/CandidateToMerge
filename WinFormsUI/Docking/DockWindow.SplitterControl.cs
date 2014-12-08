using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WeifenLuo.WinFormsUI.Docking
{
    public partial class DockWindow
    {
        private class SplitterControl : SplitterBase
        {
            protected override int SplitterSize
            {
                get { return Measures.SplitterSize; }
            }

            protected override void StartDrag()
            {
                DockWindow window = Parent as DockWindow;
                if (window == null)
                    return;

                window.DockPanel.BeginDrag(window, window.RectangleToScreen(Bounds));
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                Rectangle rect = ClientRectangle;

                if (Dock==DockStyle.Left || Dock==DockStyle.Right)
                    e.Graphics.FillRectangle(SystemBrushes.ControlLight, rect.X+Measures.SplitterSize/2 -1, rect.Y, 
                        Measures.SplitterSize/3, rect.Height);
                else
                    if (Dock==DockStyle.Top || Dock==DockStyle.Bottom)
                        e.Graphics.FillRectangle(SystemBrushes.ControlLight, rect.X, rect.Y,
                            rect.Width, Measures.SplitterSize);

            }
        }
    }
}
