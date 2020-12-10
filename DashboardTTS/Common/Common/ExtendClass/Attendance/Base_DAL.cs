using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taiyo.SearchParam;
using System.Data.SqlClient;
using System.Data;

namespace Common.ExtendClass.Attendance
{
    internal class Base_DAL
    {
        //internal DataTable GetList(BaseParam param)
        //{
        //    if (param.DateFrom == null || param.DateTo == null)
        //        throw new ArgumentNullException("Date From, Date To can not be null!");
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append(@"select 
        //                    day,
        //                    shift,
        //                    Department,
        //                    EmployeeID,
        //                    UserName,
        //                    Attendance,
        //                    OnLeave
        //                    from LMMSUserAttendanceTracking
        //                    where day >= @dateFrom and day < @dateTo");
        //    SqlParameter[] paras =
        //    {
        //        new SqlParameter("@dateFrom",SqlDbType.DateTime),
        //        new SqlParameter("@dateTo",SqlDbType.DateTime)
        //    };

        //    paras[0].Value = param.DateFrom.Value.Date;
        //    paras[1].Value = param.DateTo.Value.Date;

        //    DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras);
        //    if (ds == null || ds.Tables.Count == 0)
        //        return null;
        //    else
        //        return ds.Tables[0];
        //}

        internal DataTable GetDepartmentAttendanceList(BaseParam param)
        {
            if (param.DateFrom == null || param.DateTo == null)
                throw new ArgumentNullException("Date From, Date To can not be null!");

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select 
Department,
Day,
SUM(case when Shift = 'Day' then 1 else 0 end) as DayShift,
SUM(case when Shift = 'Night' then 1 else 0 end) as NightShift,
SUM(case when Attendance = 'Attendance' then 1 else 0 end ) as TotalPresent,
SUM(case when Attendance = 'Annual Leave' then 1 else 0 end ) as [Annual Leave],
SUM(case when Attendance = 'MC' then 1 else 0 end ) as [MC],
SUM(case when Attendance = 'UPL/UPMC' then 1 else 0 end ) as [UPL/UPMC],
SUM(case when Attendance = 'Maternity' then 1 else 0 end ) as [Maternity],
SUM(case when Attendance = 'Paternity' then 1 else 0 end ) as [Paternity],
SUM(case when Attendance = 'Marriage' then 1 else 0 end ) as [Marriage],
SUM(case when Attendance = 'WFH' then 1 else 0 end ) as [WFH],
SUM(case when Attendance = 'Hospitalization' then 1 else 0 end ) as [Hospitalization],
SUM(case when Attendance = 'Compassionate' then 1 else 0 end ) as [Compassionate],
SUM(case when Attendance = 'Child Care Leave' then 1 else 0 end ) as [Child Care Leave],
SUM(case when Attendance = 'Absent' then 1 else 0 end ) as [Absent],
SUM(case when Attendance = 'Business Trip' then 1 else 0 end ) as [Business Trip],
SUM(case when Attendance = 'Reservist' then 1 else 0 end ) as [Reservist],
SUM(case when Attendance = 'Pending' then 1 else 0 end ) as [Pending],

(select 
	case when attendance != 'attendance' then UserName+'  ---  '+Attendance end + ','
	from LMMSUserAttendanceTracking 
	where day =a.Day and Department = a.Department
	for xml path('') 
) as LeaveReason

from LMMSUserAttendanceTracking a
where day >= @dateFrom and day < @dateTo
group by Department, day ");



            SqlParameter[] paras =
            {
                new SqlParameter("@dateFrom",SqlDbType.DateTime),
                new SqlParameter("@dateTo",SqlDbType.DateTime)
            };

            paras[0].Value = param.DateFrom.Value.Date;
            paras[1].Value = param.DateTo.Value.Date;

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }



        internal DataTable GetDepartmentUserCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select DEPARTMENT, count(1) as UserCount from User_DB group by DEPARTMENT");

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString());
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }

    }
}
