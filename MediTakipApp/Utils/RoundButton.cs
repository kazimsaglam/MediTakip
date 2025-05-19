using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MediTakipApp.Forms.PharmacyPanelContent
{
    public class RoundButton : Button
    {
        protected override void OnPaint(PaintEventArgs pevent)
        {
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Oval kenarlar için path
            using var path = new GraphicsPath();
            var r = ClientRectangle;
            int d = r.Height;
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            Region = new Region(path);

            // Dolgu
            using var brush = new SolidBrush(BackColor);
            pevent.Graphics.FillPath(brush, path);

            // Taban buton çizimini bırak
            base.OnPaint(pevent);
        }
    }
}
