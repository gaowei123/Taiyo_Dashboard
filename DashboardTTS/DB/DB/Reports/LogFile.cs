using System;
using System.Collections.Generic; 
using System.Text;
using System.IO;

namespace DBHelp.Reports
{
    public class LogFile
    {
        //20176 12 01 add debug log
        private const string _sDebugLog = "[|Debug Log|]";
        private static  string _sDebugLogPath = System.Configuration.ConfigurationManager.AppSettings["Debug_Log"].ToString();
        private static string _sProdLogPath = System.Configuration.ConfigurationManager.AppSettings["Prod_Log"].ToString();
        public static void DebugLog(string LogType, string sNameSpace, string sClassName, string sFunctionName , string msg)
        {
           Log(LogType, _sDebugLog + "[" + sNameSpace + "][" + sClassName + "]["+ sFunctionName +"][" +  msg + "]");
        }
 

        public static void Log(string LogType, string msg)
        {
            try
            {
                string datePatt = @"yyyyMMdd";
                string path ="";
                string file = "";


                #region File.
                //path = ".\\Log\\"; //Test Mode
                if (msg.StartsWith(_sDebugLog))
                {
                    path = _sDebugLogPath;
                }
                else
                {
                    path = _sProdLogPath; //Production Mode
                }
                if (!Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                file = path + LogType + "(" + DateTime.Now.ToString(datePatt) + ").txt";

                #endregion

                StreamWriter tw = File.AppendText(file);

                // Write a line of text to the file.
                tw.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss tt -- ") + msg);

                // Close the stream.
                tw.Flush();
                tw.Dispose();
                tw.Close();
            }
            catch(Exception ee)
            {
                //throw ee;
           }
        }




       
    }
}
