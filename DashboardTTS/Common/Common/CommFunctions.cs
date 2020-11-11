using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.UI;
using System.Collections;
using System.Reflection;

namespace Common
{
    public  static class CommFunctions
    {
        public static bool isNumberic(string message)
        {
            if (message == "")
            {
                return false;
            }
            //System.Text.RegularExpressions.Regex rex_double = new System.Text.RegularExpressions.Regex(@"^-?\d+\.\d+$");//double类型
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(@"^(-?\d+)(\.\d+)?$");

            if (rex.IsMatch(message))
            {
                return true;
            }
            else
                return false;
        }

        public static DataTable DataRowToDataTable(DataRow[] Rows)
        {
            if (Rows == null || Rows.Length == 0)
                return null;
            DataTable tmp = Rows[0].Table.Clone();
            foreach (DataRow row in Rows)
            {
                tmp.ImportRow(row);
            }
            return tmp;
        }


        public static void ShowMessage(System.Web.UI.Page page, string sMessage)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "", "alert('"+ sMessage + "');", true);
        }


        public static void ShowMessageAndRedirect(System.Web.UI.Page page, string sMessage,string sURL)
        {
            string URLConvert = sURL.Replace("#", "%23").Replace("&", "%26");

            StringBuilder strJS = new StringBuilder();
            strJS.Append("alert('" + sMessage + "');");
            strJS.Append(" window.location.href = \"" + URLConvert + "\";  ");
            
            page.ClientScript.RegisterStartupScript(page.GetType(), "", strJS.ToString(), true);
        }
        


        // 8.5h --> 8:30:00
        public static string ConvertDateTimeShort(string sTime)
        {
            if (!isNumberic(sTime.Trim('H')))
            {
                return "0";
            }


            string[] strArr_temp = sTime.Trim('H').Split('.');

            double Time_Hour = double.Parse(strArr_temp[0]);

            double Temp_Min = strArr_temp.Length > 1 ? double.Parse("0."+strArr_temp[1]) * 60 : 0;


            strArr_temp = Temp_Min.ToString().Split('.');
            double Temp_Sec = strArr_temp.Length > 1 ? double.Parse("0." + strArr_temp[1]) * 60 : 0;


            double Time_Min = double.Parse(Temp_Min.ToString().Split('.')[0]);
            double Time_Sec = Math.Round(Temp_Sec, 0); 




            string strShortDateTime = "";

            if (Time_Hour != 0)
            {
                string hh = Time_Hour < 10 ? "0" + Time_Hour.ToString() : Time_Hour.ToString();
                strShortDateTime += hh + ":";
            }
            else
            {
                strShortDateTime += "00:";
            }

            if (Time_Min != 0)
            {
                string mm = Time_Min < 10 ? "0" + Time_Min.ToString() : Time_Min.ToString();
                strShortDateTime += mm + ":";
            }
            else
            {
                strShortDateTime += "00:";
            }

            if (Time_Sec != 0)
            {
                string ss = Time_Sec < 10 ? "0" + Time_Sec.ToString() : Time_Sec.ToString();
                strShortDateTime += ss;
            }
            else
            {
                strShortDateTime += "00";
            }

            return strShortDateTime;
        }

        // 8:30:00 --> 8.5
        public static double ConvertDateTimeToDouble(string sTime)
        {
            if (string.IsNullOrEmpty(sTime))
            {
                return 0;
            }

            string[] arrTemp = sTime.Split(':');

            if (arrTemp.Length < 3)
            {
                return 0;
            }

            string sHour = arrTemp[0];
            string sMin = arrTemp[1];
            string sSec = arrTemp[2];


            double dTotalSeconds = double.Parse(sHour) * 3600 + double.Parse(sMin) * 60 + double.Parse(sSec);

            
            return dTotalSeconds;
        }





        public static void ShowWarning(System.Web.UI.WebControls.Label lb, System.Web.UI.WebControls.DataGrid dg, string Level, string Message)
        {
            if (lb != null)
            {
                lb.Visible = true;
                lb.BackColor = System.Drawing.Color.WhiteSmoke;
                lb.ForeColor = System.Drawing.Color.Red;
            }

            if (dg != null)
            {
                dg.Visible = false;
            }

            //Level
            StringBuilder sMessage = new StringBuilder();
            sMessage.Append(Level);
            sMessage.Append("<br>");

            //Error Message
            string temp = string.IsNullOrEmpty(Message) ? "There is no data!" : Message;
            sMessage.Append(temp);


            //Suggestion
            sMessage.Append("<br><br>");
            sMessage.Append("SUGGESTION");
            sMessage.Append("<br>");
            if (Level == StaticRes.Global.ErrorLevel.Exception)
            {
                sMessage.Append("Please contact with developer !");
            }
            else if (Level == StaticRes.Global.ErrorLevel.Error)
            {
                sMessage.Append("Please contact with developer !");
            }
            else if (Level == StaticRes.Global.ErrorLevel.Warning)
            {
                sMessage.Append("Please check searching condition or try again !");
            }
         

            lb.Text = sMessage.ToString();

        }

        public static void HideWarning(System.Web.UI.WebControls.Label lb, System.Web.UI.WebControls.DataGrid dg)
        {
            if (lb != null)
            {
                lb.Visible = false;
            }

            if (dg != null)
            {
                dg.Visible = true;
            }
        }


        public static void SetAutoComplete(System.Web.UI.Page page, string sControlID_Part, string sControlID_Model)
        {
            Common.Class.BLL.LMMSBom_BLL bll = new Common.Class.BLL.LMMSBom_BLL();
            

            //拼JS
            StringBuilder strJS = new StringBuilder();
            //strJS.AppendLine("<script src=\"../../assets/js/jquery-1.7.min.js\" type=\"text/javascript\"></script>");
            strJS.AppendLine("<script src=\"../../assets/js/jquery.bigautocomplete.js?v=2\"></script> ");
            strJS.AppendLine("<link rel=\"stylesheet\" href=\"../../assets/css/jquery.bigautocomplete.css\" type=\"text/css\" />");
            strJS.AppendLine("");





            strJS.AppendLine("<script type=\"text/javascript\">");

            #region part no
            if (sControlID_Part != "")
            {
                List<string> partNoList = bll.GetPatNoList();
                if (partNoList == null)
                    return;


                strJS.AppendLine("  jQuery(function($){  $(function(){");
                strJS.AppendLine(" $(\"" + sControlID_Part + "\").bigAutocomplete({");
                strJS.AppendLine(" data:[ ");
                foreach (string str in partNoList)
                {
                    strJS.Append("{\"title\":\"" + str + "\"},");
                }
                strJS.Remove(strJS.Length - 1, 1);
                strJS.AppendLine(" ], ");

                strJS.AppendLine(" callback:function(data){ }");
                strJS.AppendLine(" }); });});");

                strJS.AppendLine("");
            }
            #endregion

            #region model
            if (sControlID_Model != "")
            {
                List<string> modelList = bll.GetModelList();
                if (modelList == null)
                    return;


                strJS.AppendLine("  jQuery(function($){  $(function(){");
                strJS.AppendLine(" $(\"" + sControlID_Model + "\").bigAutocomplete({");
                strJS.AppendLine(" data:[ ");
                foreach (string str in modelList)
                {
                    strJS.Append("{\"title\":\"" + str + "\"},");
                }
                strJS.Remove(strJS.Length - 1, 1);
                strJS.AppendLine(" ], ");

                strJS.AppendLine(" callback:function(data){ }");
                strJS.AppendLine(" }); });});");

                strJS.AppendLine("");
            }
            #endregion

            #region supplier   

            #endregion

            #region customer

            #endregion


            strJS.AppendLine("</script>");




            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "", strJS.ToString());
        }


        /// <summary>
        /// list to datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static DataTable ListToDt<T>(IEnumerable<T> collection)
        {
            var props = typeof(T).GetProperties();

            DataTable dt = new DataTable();

            foreach (PropertyInfo pi in props)
            {
                Type colType = pi.PropertyType;

                //排除DATASET不支持System.Nullable错误
                if (colType.IsGenericType && colType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    colType = colType.GetGenericArguments()[0];



                dt.Columns.Add(pi.Name, colType);
            }



            //dt.Columns.AddRange(props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());



            if (collection.Count() > 0)
            {
                for (int i = 0; i < collection.Count(); i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in props)
                    {
                        object obj = pi.GetValue(collection.ElementAt(i), null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    dt.LoadDataRow(array, true);
                }
            }

            return dt;
        }




        public static DateTime  GetDefaultReportsSearchingDay()
        {
            //默认显示前一天的.   周日, 周一显示 上周五的.
            DateTime dLastDay = new DateTime();
            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                dLastDay = DateTime.Now.AddDays(-2);
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                dLastDay = DateTime.Now.AddDays(-3);
            }
            else
            {
                dLastDay = DateTime.Now.AddDays(-1);
            }


            return dLastDay;
        }



        //获取总时间,用于计算utilization
        //当天的时间 按照当前时间计算
        public static double GetTotalSeconds(DateTime dDateFrom, DateTime dDateTo, string sShift, string sDateNotIn, bool bExceptWeekend)
        {
            if (dDateFrom >= dDateTo)
                return 0;
            
            double hourPerDay = sShift == "" ? 24 : 12;
            DateTime dateFrom = dDateFrom.Date;
            DateTime dateTo = dDateTo.Date;


            double totalSeconds = 0;
            double todayCurrentSeconds = 0;


            


            if (dateTo <= DateTime.Now.Date)
            {
                double totalHours = (dateTo - dateFrom).Days * hourPerDay;
                totalSeconds = totalHours * 3600;
                todayCurrentSeconds = 0;
            }
            else
            { 
                //除了今天的 所有天 总小时数
                double totalHours = (dateTo.AddDays(-1) - dateFrom).TotalDays * hourPerDay;
                totalSeconds = totalHours * 3600;



                //当前时间 在8:00 - 20:00之间则为 day, else night.
                string curShift = DateTime.Now >= DateTime.Now.Date.AddHours(8) && DateTime.Now < DateTime.Now.Date.AddHours(20) ? StaticRes.Global.Shift.Day : StaticRes.Global.Shift.Night;



                //累加今天当前的总秒数
                if (sShift == "" || sShift.ToUpper() == "ALL")
                {
                    todayCurrentSeconds = (DateTime.Now - DateTime.Now.Date.AddHours(8)).TotalSeconds;
                }
                else if (curShift == StaticRes.Global.Shift.Day && sShift == StaticRes.Global.Shift.Day)
                {
                    todayCurrentSeconds = (DateTime.Now - DateTime.Now.Date.AddHours(8)).TotalSeconds;
                }
                else if (curShift == StaticRes.Global.Shift.Day && sShift == StaticRes.Global.Shift.Night)
                {
                    todayCurrentSeconds = 0;
                }
                else if (curShift == StaticRes.Global.Shift.Night && sShift == StaticRes.Global.Shift.Day)
                {
                    todayCurrentSeconds = 12 * 3600;
                }
                else if (curShift == StaticRes.Global.Shift.Night && sShift == StaticRes.Global.Shift.Night)
                {
                    todayCurrentSeconds = 12 * 3600;
                    todayCurrentSeconds += (DateTime.Now - DateTime.Now.Date.AddHours(20)).TotalSeconds;
                }
            }




            //排除 date not in 的时间 && except weekend 的时间
            double totalDateNotInSeconds = 0;
            double totalExceptWeekendSeconds = 0;
            string[] arrDateNotIn = sDateNotIn.Split(',');

            DateTime dTemp = dateFrom.Date;
            while(dTemp < dateTo)
            {
                //if else 分开周末,  防止重复排除.
                if ((dTemp.DayOfWeek == DayOfWeek.Saturday || dTemp.DayOfWeek == DayOfWeek.Sunday) && bExceptWeekend)
                {
                    //如果是当天, 则把todayCurrentSeconds = 0
                    if (dTemp == DateTime.Now.Date)
                        todayCurrentSeconds = 0;
                    else
                        totalExceptWeekendSeconds += hourPerDay * 3600;
                }
                else
                {
                    if (arrDateNotIn.Contains(dTemp.Day.ToString()))
                    {
                        //如果是当天, 则把todayCurrentSeconds = 0
                        if (dTemp == DateTime.Now.Date)
                            todayCurrentSeconds = 0;
                        else
                            totalDateNotInSeconds += hourPerDay * 3600;
                    }
                }

                dTemp = dTemp.AddDays(1);
            }


            return totalSeconds + todayCurrentSeconds - totalDateNotInSeconds - totalExceptWeekendSeconds;
        }




        public static string GetMonthName(int month, bool isFullName)
        {
            string result = "";

            switch (month)
            {
                case 1:
                    result = isFullName ? "January" : "Jan";
                    break;
                case 2:
                    result = isFullName ? "February" : "Feb";
                    break;
                case 3:
                    result = isFullName ? "March" : "Mar";
                    break;
                case 4:
                    result = isFullName ? "April" : "Apr";
                    break;
                case 5:
                    result = isFullName ? "May" : "May";
                    break;
                case 6:
                    result = isFullName ? "June" : "Jun";
                    break;
                case 7:
                    result = isFullName ? "July" : "Jul";
                    break;
                case 8:
                    result = isFullName ? "August" : "Aug";
                    break;
                case 9:
                    result = isFullName ? "September" : "Sep";
                    break;
                case 10:
                    result = isFullName ? "October" : "Oct";
                    break;
                case 11:
                    result = isFullName ? "November" : "Nov";
                    break;
                case 12:
                    result = isFullName ? "December" : "Dec";
                    break;
            }
            return result;
        }

    }
}
