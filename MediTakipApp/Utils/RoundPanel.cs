using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;

namespace MediTakipApp.Forms.PharmacyPanelContent
{
    public class RoundPanel : Panel
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int BorderRadius { get; set; } = 20;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int BorderSize { get; set; } = 0;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color BorderColor { get; set; } = Color.Transparent;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = ClientRectangle;
            using var path = new GraphicsPath();
            int d = BorderRadius * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            Region = new Region(path);

            // Dolgu
            using (var brush = new SolidBrush(BackColor))
                e.Graphics.FillPath(brush, path);
            // Kenar
            if (BorderSize > 0)
                using (var pen = new Pen(BorderColor, BorderSize))
                    e.Graphics.DrawPath(pen, path);
        }
    }
}
