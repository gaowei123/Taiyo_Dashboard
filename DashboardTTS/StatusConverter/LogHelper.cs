using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Taiyo.Tool
{
    public static class LogHelper
    {
       

        public static void Log(string msg)
        {
            string path = "./Log";
            if (!Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);


            var file = $"{path}/{DateTime.Now.ToString("yyyy-MM-dd")}.txt";


            StreamWriter tw = File.AppendText(file);

            tw.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss tt -- ") + msg);

            tw.Flush();
            tw.Dispose();
            tw.Close();
        }
    }
}
