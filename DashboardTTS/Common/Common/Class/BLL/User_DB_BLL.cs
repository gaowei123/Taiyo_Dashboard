using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Taiyo.Enum.Organization;
using Taiyo.Tool.Extension;

namespace Common.Class.BLL
{
    public class User_DB_BLL
    {
        private Common.Class.DAL.User_DB_DAL dal = new DAL.User_DB_DAL();



        public  List<Common.Class.Model.User_DB_Model > GetModelList(string sDepartment, string sEmployeeID, string sUserGroup, string sPassword)
        {
            DataTable dt = dal.GetList(sDepartment, sEmployeeID, sUserGroup,sPassword);
            if (dt ==null)
                return null;


            List<Common.Class.Model.User_DB_Model> modelList = new List<Model.User_DB_Model>();
            foreach (DataRow dr in dt.Rows)
            {
                Common.Class.Model.User_DB_Model model = new Model.User_DB_Model();

                model.USER_GROUP = dr["USER_GROUP"].ToString();
                model.USER_ID = dr["USER_ID"].ToString();
                model.USER_NAME = dr["USER_NAME"].ToString();
                model.PASSWORD = dr["PASSWORD"].ToString();
                model.UPDATED_TIME = DateTime.Parse(dr["UPDATED_TIME"].ToString());
                model.UPDATED_BY = dr["UPDATED_BY"].ToString();
                model.DEPARTMENT = dr["DEPARTMENT"].ToString();
                model.FINGER_TEMPLATE = dr["FINGER_TEMPLATE"].ToString();
                model.SHIFT = dr["SHIFT"].ToString();
                model.FINGER_TEMPLATE_1 = dr["FINGER_TEMPLATE_1"].ToString();
                model.EMPLOYEE_ID = dr["EMPLOYEE_ID"].ToString();
                model.DEPARTMENT_ID = dr["DEPARTMENT_ID"].ToString();
                
                modelList.Add(model);
            }
            return modelList;
        }

        public bool Add(Common.Class.Model.User_DB_Model model)
        {
        

            int result = dal.Add(model);



            //如果Department是Moulding,PQC需要再添加到Moulding DB, PQC DB中,
            //Moulding/PQC Client会使用各自database的user_db表登入
            if (model.DEPARTMENT == Department.Moulding.GetDescription())
            {
                DBHelp.Reports.LogFile.Log("UserManagement", "start add moulding or pqc!");
                int temp = dal.Add(model, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
                if (temp < 1)
                {
                    DBHelp.Reports.LogFile.Log("UserManagement", "Add user for moulding db fail!");
                }
            }
            else if (model.DEPARTMENT == Department.PQC.GetDescription())
            {
                int temp = dal.Add(model, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
                if (temp < 1)
                {
                    DBHelp.Reports.LogFile.Log("UserManagement", "Add user for pqc db fail!");
                }
            }


        
                

            if (result > 0)
                return true;
            else
                return false;
        }

        public bool Update(Common.Class.Model.User_DB_Model model)
        {
           

            int result = dal.Update(model);



            //moulding, pqc client 会使用各自database的user_db表登入
            if (model.DEPARTMENT == StaticRes.Global.Department.Moulding)
            {
                int temp = dal.Update(model, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
                if (temp < 1)
                {
                    DBHelp.Reports.LogFile.Log("User_DB_BLL", "Add user for moulding db fail!");
                }
            }
            else if (model.DEPARTMENT == StaticRes.Global.Department.PQC)
            {
                int temp = dal.Update(model, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
                if (temp < 1)
                {
                    DBHelp.Reports.LogFile.Log("User_DB_BLL", "Add user for pqc db fail!");
                }
            }





            if (result > 0)
                return true;
            else
                return false;
        }

        public bool Delete(string sEmployeeID)
        {
            int result = dal.Delete(sEmployeeID);

            //从moulding, pqc的user db中也删一次.
            dal.Delete(sEmployeeID, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
            dal.Delete(sEmployeeID, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            

            if (result > 0)
                return true;
            else
                return false;
        }

        public bool Exist(string sEmployeeID)
        {
            DataTable dt = dal.GetList("", sEmployeeID, "","");

            if (dt == null || dt.Rows.Count == 0)
                return false;
            else
                return true;
        }
        
        //admin不包括在内.
        public List<string> GetUsernameList(string sDepartment)
        {
            DataTable dt = dal.GetList(sDepartment, "", "","");
            if (dt == null)
                return null;


            List<string> userNameList = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["USER_GROUP"].ToString().ToUpper() == "ADMIN")
                    continue;

                string userName = dr["USER_NAME"].ToString();
                userNameList.Add(userName);
            }

            return userNameList;
        }

        public List<string> GetUserIDList(string sDepartment)
        {
            DataTable dt = dal.GetList(sDepartment, "", "", "");
            if (dt == null)
                return null;

            List<string> userIDList = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["USER_GROUP"].ToString().ToUpper() == "ADMIN")
                    continue;

                string userName = dr["USER_ID"].ToString();
                userIDList.Add(userName);
            }

            return userIDList;
        }

        
        public bool Login(string sEmployeeID, string Password, out string ErrorStr, string sDepartment, string Group)
        {

            //check id
            DataTable dt = dal.GetList("", sEmployeeID, "", "");
            if (dt == null || dt.Rows.Count == 0)
            {
                ErrorStr = "This User is not exist!";
                return false;
            }

            //check id&password
            dt = dal.GetList("", sEmployeeID, "", Password);
            if (dt == null || dt.Rows.Count == 0)
            {
                ErrorStr = "This password is wrong!";
                return false;
            }


            //check Authority
            string userGroup = dt.Rows[0]["USER_GROUP"].ToString();
            if (IsAuthorityAllow(userGroup, Group))
            {
                ErrorStr = "";
                return true;
            }
            else
            {
                ErrorStr = "Your authority is not enough!";
                return false;
            }
        }

        private bool IsAuthorityAllow(string UserCurGroup, string Group)
        {
            //UserCurGroup = UserCurGroup.ToUpper();
            bool result = false;
            switch (Group)
            {
                case StaticRes.Global.UserGroup.ADMIN:
                    {
                        if (UserCurGroup == StaticRes.Global.UserGroup.ADMIN)
                        {
                            result = true;
                        }
                    }
                    break;
                case StaticRes.Global.UserGroup.SUPERVISOR:
                    {
                        if (UserCurGroup == StaticRes.Global.UserGroup.ADMIN ||
                            UserCurGroup == StaticRes.Global.UserGroup.SUPERVISOR)
                        {
                            result = true;
                        }
                    }
                    break;
                case StaticRes.Global.UserGroup.ENGINEER:
                    {
                        if (UserCurGroup == StaticRes.Global.UserGroup.ADMIN ||
                            UserCurGroup == StaticRes.Global.UserGroup.SUPERVISOR ||
                            UserCurGroup == StaticRes.Global.UserGroup.ENGINEER)
                        {
                            result = true;
                        }
                    }
                    break;

                case StaticRes.Global.UserGroup.TECHNICIAN:
                    {
                        if (UserCurGroup == StaticRes.Global.UserGroup.ADMIN ||
                            UserCurGroup == StaticRes.Global.UserGroup.SUPERVISOR ||
                            UserCurGroup == StaticRes.Global.UserGroup.TECHNICIAN)
                        {
                            result = true;
                        }
                    }
                    break;

                case StaticRes.Global.UserGroup.LEADER:
                    {
                        if (UserCurGroup == StaticRes.Global.UserGroup.ADMIN ||
                            UserCurGroup == StaticRes.Global.UserGroup.SUPERVISOR ||
                            UserCurGroup == StaticRes.Global.UserGroup.TECHNICIAN ||
                            UserCurGroup == StaticRes.Global.UserGroup.LEADER)
                        {
                            result = true;
                        }
                    }
                    break;

                case StaticRes.Global.UserGroup.OPERATOR:
                    {
                        if (UserCurGroup == StaticRes.Global.UserGroup.ADMIN ||
                            UserCurGroup == StaticRes.Global.UserGroup.SUPERVISOR ||
                            UserCurGroup == StaticRes.Global.UserGroup.ENGINEER ||
                            UserCurGroup == StaticRes.Global.UserGroup.OPERATOR ||
                            UserCurGroup == StaticRes.Global.UserGroup.TECHNICIAN ||
                            UserCurGroup == StaticRes.Global.UserGroup.LEADER ||
                            UserCurGroup == StaticRes.Global.UserGroup.MH ||
                            UserCurGroup == StaticRes.Global.UserGroup.CHECKER)
                        {
                            result = true;
                        }
                    }
                    break;
                case StaticRes.Global.UserGroup.MH:
                    {
                        if (UserCurGroup == StaticRes.Global.UserGroup.ADMIN ||
                            UserCurGroup == StaticRes.Global.UserGroup.SUPERVISOR ||
                            UserCurGroup == StaticRes.Global.UserGroup.ENGINEER ||
                            UserCurGroup == StaticRes.Global.UserGroup.LEADER ||
                             UserCurGroup == StaticRes.Global.UserGroup.MH)
                        {
                            result = true;
                        }
                    }
                    break;
                default:
                    result = false;
                    break;
            }

            return result;
        }
        
    }
}
