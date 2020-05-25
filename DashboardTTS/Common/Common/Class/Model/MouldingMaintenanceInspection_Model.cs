using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Class.Model
{
   public class MouldingMaintenanceInspection_Model
    {
        public MouldingMaintenanceInspection_Model()
        {

        }

        private int _UniqID;
        private int _No;
        private string _CheckPeriod;
        private string _CheckItem;
        private string _InspectionDescription;
        private string _MaintainenceDescription;
        private DateTime _DateTime;
        private DateTime _UpdateTime;


        public int UniqID
        {
            set { _UniqID = value; }
            get { return _UniqID; }
        }
        public int No
        {
            set { _No = value; }
            get { return _No; }
        }

        public string CheckPeriod
        {
            set { _CheckPeriod = value; }
            get { return _CheckPeriod; }
        }
        public string CheckItem
        {
            set { _CheckItem = value; }
            get { return _CheckItem; }
        }
        public string InspectionDescription
        {
            set { _InspectionDescription = value; }
            get { return _InspectionDescription; }
        }
        public string MaintainenceDescription
        {
            set { _MaintainenceDescription = value; }
            get { return _MaintainenceDescription; }
        }
        public DateTime DateTime
        {
            set { _DateTime = value; }
            get { return _DateTime; }
        }
        public DateTime UpdateTime
        {
            set { _UpdateTime = value; }
            get { return _UpdateTime; }
        }

    }
}
