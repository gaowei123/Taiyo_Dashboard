using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Class.Model
{
   public class MouldingMaintain_His_Model
    {
        public MouldingMaintain_His_Model() { }

        private string _MachineID;
        private string _CheckPeriod;
        private string _CheckItem;
        private string _CheckResult;
        private string _SpareParts;
        private DateTime? _ChangeTime;
        private DateTime? _CheckDate;
        private string _CheckBy;
        private string _VerifyBy;
        private string _Remark;
        
        public string MachineID
        {
            set { _MachineID = value; }
            get { return _MachineID; }
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
        public string CheckResult
        {
            set { _CheckResult = value; }
            get { return _CheckResult; }
        }
        public string SpareParts
        {
            set { _SpareParts = value; }
            get { return _SpareParts; }
        }

        public DateTime? ChangeTime
        {
            set { _ChangeTime = value; }
            get { return _ChangeTime; }
        }
        public DateTime? CheckDate
        {
            set { _CheckDate = value; }
            get { return _CheckDate; }
        }
        public string CheckBy
        {
            set { _CheckBy = value; }
            get { return _CheckBy; }
        }
        public string VerifyBy
        {
            set { _VerifyBy = value; }
            get { return _VerifyBy; }
        }
        public string Remark
        {
            set { _Remark = value; }
            get { return _Remark; }
        }

    }
}
