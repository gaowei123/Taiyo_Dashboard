using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Class.Model
{
    public class LMMSUserEventLog
    {
        public LMMSUserEventLog()
        {

        }

        private string _userID;
        private DateTime _dateTime;
        private DateTime _startTime;
        private DateTime _endTime;
        private string _eventType;
        private string _pageName;
        private string _action;
        private string _jobnumber;
        private string _material;
        private string _temp1;
        private string _temp2;
        private string _temp3;


        public string material
        {
            set { _material = value; }
            get { return _material; }
        }
        public string userID
        {
            set { _userID = value; }
            get { return _userID; }
        }

        public DateTime dateTime
        {
            set { _dateTime = value; }
            get { return _dateTime; }
        }

        public DateTime startTime
        {
            set { _startTime = value; }
            get { return _startTime; }
        }

        public DateTime endTime
        {
            set { _endTime = value; }
            get { return _endTime; }
        }

        public string eventType
        {
            set { _eventType = value; }
            get { return _eventType; }
        }

        public string pageName
        {
            set { _pageName = value; }
            get { return _pageName; }
        }

        public string action
        {
            set { _action = value; }
            get { return _action; }
        }

        public string jobnumber
        {
            set { _jobnumber = value; }
            get { return _jobnumber; }
        }

        public string temp1
        {
            set { _temp1 = value; }
            get { return _temp1; }
        }

        public string temp2
        {
            set { _temp2 = value; }
            get { return _temp2; }
        }

        public string temp3
        {
            set { _temp3 = value; }
            get { return _temp3; }
        }

    }
}
