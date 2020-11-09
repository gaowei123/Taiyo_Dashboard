using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taiyo.Tool
{
    public static class DateTimeConventor
    {
        public static string GetWeekName(DateTime datetime, bool isFullName)
        {
            string result = "";
            switch (datetime.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    result = isFullName ? DayOfWeek.Sunday.ToString() : "Sun";
                    break;
                case DayOfWeek.Monday:
                    result = isFullName ? DayOfWeek.Monday.ToString() : "Mon";
                    break;
                case DayOfWeek.Tuesday:
                    result = isFullName ? DayOfWeek.Tuesday.ToString() : "Tue";
                    break;
                case DayOfWeek.Wednesday:
                    result = isFullName ? DayOfWeek.Wednesday.ToString() : "Wed";
                    break;
                case DayOfWeek.Thursday:
                    result = isFullName ? DayOfWeek.Thursday.ToString() : "Thu";
                    break;
                case DayOfWeek.Friday:
                    result = isFullName ? DayOfWeek.Friday.ToString() : "Fri";
                    break;
                case DayOfWeek.Saturday:
                    result = isFullName ? DayOfWeek.Saturday.ToString() : "Sat";
                    break;
                default:
                    break;
            }


            return result;
        }


        public static string GetMonthName(DateTime datetime, bool isFullName)
        {
            string result = "";

            switch (datetime.Month)
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
