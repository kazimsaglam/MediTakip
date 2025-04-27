public class TransparentLabel : Label
{
    protected override void WndProc(ref Message m)
    {
        const int WM_NCHITTEST = 0x84;
        base.WndProc(ref m);

        if (m.Msg == WM_NCHITTEST)
        {
            m.Result = (IntPtr)(-1); // 🔥 Mouse olayını geç
        }
    }
}
