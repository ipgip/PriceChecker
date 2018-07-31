using System;
using System.IO;
using System.Management;
using System.Windows.Forms;

namespace web
{
    public class Licensing
    {
        public static bool CheckLicense(string Cpu)
        {
            if (Properties.Settings.Default.License != Licensing.CPU())
            {
                Program.LicenseFlag = false;
                return (Math.Abs((DateTime.Now - StartDate()).Days) < 7);
            }
            else
            {
                Program.LicenseFlag = true;
                return true;
            }
        }

        public static string CPU()
        {
            string serial = String.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT ProcessorId FROM Win32_Processor");
            //Caption,CurrentClockSpeed,LoadPercentage,Manufacturer,MaxClockSpeed,Name,ProcessorId,SocketDesignation,SystemName,Version FROM Win32_Processor");
            foreach (ManagementObject queryObj in searcher.Get())
            {
                serial = queryObj["ProcessorId"].ToString();
            }
            return serial;
        }

        public static DateTime StartDate()
        {
            DateTime ret = DateTime.MinValue;
            try
            {
                ret = File.GetLastWriteTime(Application.ExecutablePath).Date;
                //ret = File.GetCreationTime(Application.ExecutablePath).Date;
            }
            catch (Exception err)
            {
                MessageBox.Show($"{err.Message}");
            }
            return ret;
        }
    }
}
