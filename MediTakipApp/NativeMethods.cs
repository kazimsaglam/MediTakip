using System;
using System.Runtime.InteropServices;

namespace MediTakipApp
{
    public static class NativeMethods
    {
        [DllImport("gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,    // Sol kenar koordinatı
            int nTopRect,     // Üst kenar koordinatı
            int nRightRect,   // Sağ kenar koordinatı
            int nBottomRect,  // Alt kenar koordinatı
            int nWidthEllipse, // Elips genişliği (köşe eğimi)
            int nHeightEllipse // Elips yüksekliği (köşe eğimi)
        );
    }
}
