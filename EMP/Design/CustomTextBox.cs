using System.Drawing;
using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace EMP.Design
{
    public class CustomTextBox : TextBox
    {
        private string _placeholderText = "Enter text here...";
        private Color _borderColor = Color.Gray;
        private bool _isPlaceholderActive = true;
        private int _borderRadius = 10; // Default border radius

        [Category("Custom Properties")]
        public string PlaceholderText
        {
            get { return _placeholderText; }
            set
            {
                _placeholderText = value;
                if (_isPlaceholderActive)
                {
                    this.Text = _placeholderText;
                }
            }
        }

        [Category("Custom Properties")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                this.Invalidate();
            }
        }

        [Category("Custom Properties")]
        public int BorderRadius
        {
            get { return _borderRadius; }
            set
            {
                _borderRadius = value;
                this.Invalidate();
            }
        }

        public CustomTextBox()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.Text = _placeholderText;
            this.ForeColor = Color.Gray;
            this.Enter += RemovePlaceholder;
            this.Leave += SetPlaceholder;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Create a rounded rectangle path
            using (GraphicsPath path = new GraphicsPath())
            {
                Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                path.AddArc(rect.X, rect.Y, _borderRadius, _borderRadius, 180, 90);
                path.AddArc(rect.Right - _borderRadius, rect.Y, _borderRadius, _borderRadius, 270, 90);
                path.AddArc(rect.Right - _borderRadius, rect.Bottom - _borderRadius, _borderRadius, _borderRadius, 0, 90);
                path.AddArc(rect.X, rect.Bottom - _borderRadius, _borderRadius, _borderRadius, 90, 90);
                path.CloseFigure();

                // Draw the border
                using (Pen pen = new Pen(_borderColor))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.DrawPath(pen, path);
                }
            }

            // Draw the placeholder text if applicable
            if (_isPlaceholderActive)
            {
                TextRenderer.DrawText(e.Graphics, _placeholderText, this.Font, new Rectangle(0, 0, this.Width, this.Height), Color.Gray, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
            }
        }

        private void RemovePlaceholder(object sender, EventArgs e)
        {
            if (_isPlaceholderActive)
            {
                this.Text = "";
                this.ForeColor = Color.Black;
                _isPlaceholderActive = false;
            }
        }

        private void SetPlaceholder(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.Text))
            {
                _isPlaceholderActive = true;
                this.Text = _placeholderText;
                this.ForeColor = Color.Gray;
            }
        }
    }
}