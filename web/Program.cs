using CefSharp;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace web
{
    static class Program
    {
        public static bool LicenseFlag = false;

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //MessageBox.Show("bofore start");

            string CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Cache");
            if (!Directory.Exists(CachePath))
            {
                Directory.CreateDirectory(CachePath);
            }

            if (Licensing.CheckLicense(Properties.Settings.Default.License))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Cef.EnableHighDPISupport();

                try
                {
                    var settings = new CefSettings()
                    {
                        //By default CefSharp will use an in-memory cache, you need to specify a Cache Folder to persist data
                        CachePath = CachePath
                    };

                    //Perform dependency check to make sure all relevant resources are in our output directory.
                    Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);

                    Application.Run(new Form1());
                }
                catch (Exception err)
                {
                    MessageBox.Show($"{err.Message}");
                }
            }
        }
    }
}
