﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taiyo.SearchParam;
using System.Data;
using Taiyo.Enum.Organization;
using Taiyo.Tool.Extension;
using Taiyo.Enum;
using Taiyo.Enum.Organization;

namespace Common.ExtendClass.Attendance
{
    public class Base_BLL
    {
        private readonly Base_DAL _dal;
        public Base_BLL()
        {
            _dal = new Base_DAL();
        }


        //public List<Base_Model> GetBaseList(BaseParam param)
        //{
        //    DataTable dt = _dal.GetList(param);
        //    if (dt == null || dt.Rows.Count == 0)
        //        return null;

        //    List<Base_Model> list = new List<Base_Model>();

        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        Base_Model model = new Base_Model();
        //        model.Day = DateTime.Parse(dr["day"].ToString());
        //        model.Shift = (CommonEnum.Shift)Enum.Parse(typeof(CommonEnum.Shift), dr["shift"].ToString());
        //        model.Department = (Department)Enum.Parse(typeof(Department), dr["Department"].ToString().Replace("/","_"));
        //        model.EmployeeID = dr["EmployeeID"].ToString();
        //        model.Username = dr["UserName"].ToString();
        //        model.Attendance = dr["Attendance"].ToString();
        //        model.OnLeave = dr["OnLeave"].ToString();

        //        list.Add(model);
        //    }

        //    return list;
        //}


        public List<Attendance_Model> GetDepartmentAttendanceList(BaseParam param)
        {
            DataTable dt = _dal.GetDepartmentAttendanceList(param);
            if (dt == null || dt.Rows.Count == 0)
                return null;

            List<Attendance_Model> list = new List<Attendance_Model>();

            foreach (DataRow dr in dt.Rows)
            {
                Attendance_Model model = new Attendance_Model();

                model.Department = (Department)Enum.Parse(typeof(Department), dr["Department"].ToString().Replace("/", "_"));
                model.Day = DateTime.Parse(dr["day"].ToString());

                model.DayShift = decimal.Parse(dr["DayShift"].ToString());
                model.NightShift = decimal.Parse(dr["NightShift"].ToString());
                model.TotalPresent = decimal.Parse(dr["TotalPresent"].ToString());
                

                model.AnnualLeavel = decimal.Parse(dr["Annual Leave"].ToString());
                model.MC = decimal.Parse(dr["MC"].ToString());
                model.UPL_UPMC = decimal.Parse(dr["UPL/UPMC"].ToString());
                model.Maternity = decimal.Parse(dr["Maternity"].ToString());
                model.Paternity = decimal.Parse(dr["Paternity"].ToString());
                model.Marriage = decimal.Parse(dr["Marriage"].ToString());
                model.WFH = decimal.Parse(dr["WFH"].ToString());
                model.Hospitalization = decimal.Parse(dr["Hospitalization"].ToString());
                model.Compassionate = decimal.Parse(dr["Compassionate"].ToString());
                model.ChildCareLeave = decimal.Parse(dr["Child Care Leave"].ToString());
                model.Absent = decimal.Parse(dr["Absent"].ToString());
                model.BusinessTrip = decimal.Parse(dr["Business Trip"].ToString());
                model.Reservist = decimal.Parse(dr["Reservist"].ToString());
                model.Pending = decimal.Parse(dr["Pending"].ToString());

                model.LeaveReason = dr["LeaveReason"].ToString();



                list.Add(model);
            }

            return list;
        }

        public Dictionary<Department, decimal> GetDepartmentUserCount()
        {
            DataTable dt = _dal.GetDepartmentUserCount();
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                Dictionary<Department, decimal> dicUser = new Dictionary<Department, decimal>();
                foreach (DataRow dr in dt.Rows)
                {
                    Department department =(Department) Enum.Parse(typeof(Department), dr["DEPARTMENT"].ToString().Replace("/","_")) ;
                    decimal userCount = decimal.Parse(dr["UserCount"].ToString());

                    dicUser.Add(department, userCount);
                }


              

                foreach (Department  item in Enum.GetValues(typeof(Department)))
                {
                    if (!dicUser.ContainsKey(item))
                    {
                        dicUser.Add(item, 0);
                    }
                }

                return dicUser;
            }
        }
    }
}
