using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;

namespace EMP.Design
{
    public class RoundedButton : Button
    {
        public int BorderRadius { get; set; } = 5;

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            Graphics graphics = pevent.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = ClientRectangle;
            using (GraphicsPath path = RoundedRectangle(rect, BorderRadius))
            using (Pen pen = new Pen(BackColor, 0))
            using (SolidBrush brush = new SolidBrush(BackColor))
            {
                this.Region = new Region(path);
                graphics.FillPath(brush, path);
                graphics.DrawPath(pen, path);
            }

            TextRenderer.DrawText(graphics, Text, Font, rect, ForeColor, BackColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private GraphicsPath RoundedRectangle(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            // Top left arc
            path.AddArc(arc, 180, 90);

            // Top right arc
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // Bottom right arc
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // Bottom left arc
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

    }
}
