using MediTakipApp.Forms;

namespace MediTakipApp.Utils
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new _LoginScreen());
        }
    }
}