using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Class.Model
{
    public  class User_DB_Model
    {
        private string _EMPLOYEE_ID;
        private string _USER_ID;
        private string _USER_NAME;
        private string _PASSWORD;
        private string _USER_GROUP;
        private DateTime _UPDATED_TIME;
        private string _UPDATED_BY;
        private string _DEPARTMENT;
        private string _FINGER_TEMPLATE;
        private string _SHIFT;
        private string _FINGER_TEMPLATE_1;

        private string _DEPARTMENT_ID;


        public string EMPLOYEE_ID
        {
            set { _EMPLOYEE_ID = value; }
            get { return _EMPLOYEE_ID; }
        }
        public string USER_ID
        {
            set { _USER_ID = value; }
            get { return _USER_ID; }
        }
        public string USER_NAME
        {
            set { _USER_NAME = value; }
            get { return _USER_NAME; }
        }
        public string PASSWORD
        {
            set { _PASSWORD = value; }
            get { return _PASSWORD; }
        }
        public string USER_GROUP
        {
            set { _USER_GROUP = value; }
            get { return _USER_GROUP; }
        }
        public DateTime UPDATED_TIME
        {
            set { _UPDATED_TIME = value; }
            get { return _UPDATED_TIME; }
        }
        public string UPDATED_BY
        {
            set { _UPDATED_BY = value; }
            get { return _UPDATED_BY; }
        }
        public string DEPARTMENT
        {
            set { _DEPARTMENT = value; }
            get { return _DEPARTMENT; }
        }
        public string SHIFT
        {
            set { _SHIFT = value; }
            get { return _SHIFT; }
        }
        public string FINGER_TEMPLATE
        {
            set { _FINGER_TEMPLATE = value; }
            get { return _FINGER_TEMPLATE; }
        }
        public string FINGER_TEMPLATE_1
        {
            set { _FINGER_TEMPLATE_1 = value; }
            get { return _FINGER_TEMPLATE_1; }
        }

        public string DEPARTMENT_ID
        {
            set { _DEPARTMENT_ID = value; }
            get { return _DEPARTMENT_ID; }
        }

    }
}
