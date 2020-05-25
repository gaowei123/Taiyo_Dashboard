using System;
using System.Collections.Generic; 
using System.Text;

namespace Convert
{
    public class DateTimeToString
    {
        public static string FromDateTime(DateTime x)
        {
            string y, m, d, h, mi, s;

            y = x.Year.ToString();

            m = x.Month.ToString();

            if (m.Length == 1)
            {
                m = "0" + m;
            }

            d = x.Day.ToString();

            if (d.Length == 1)
            {
                d = "0" + d;
            }

            h = x.Hour.ToString();

            if (h.Length == 1)
            {
                h = "0" + h;
            }

            mi = x.Minute.ToString();

            if (mi.Length == 1)
            {
                mi = "0" + mi;
            }

            s = x.Second.ToString();

            if (s.Length == 1)
            {
                s = "0" + s;
            }

            return y + m + d + h + mi + s;
        }

        public static string FromDate(DateTime x)
        {
            string y, m, d;

            y = x.Year.ToString();

            m = x.Month.ToString();

            if (m.Length == 1)
            {
                m = "0" + m;
            }

            d = x.Day.ToString();

            if (d.Length == 1)
            {
                d = "0" + d;
            }
            return y + m + d;
        }


      
    }
}
