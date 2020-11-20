using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Taiyo.Tool
{
    public static class LogHelper
    {
        public static void JobScheduleLog(string msg)
        {
            var file = $"./{DateTime.Now.ToString("yyyy-MM-dd")}.txt";


            StreamWriter tw = File.AppendText(file);

            tw.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss tt -- ") + msg);

            tw.Flush();
            tw.Dispose();
            tw.Close();
        }
    }
}
