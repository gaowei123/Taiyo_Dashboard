using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Class.Model
{
    public class MouldingMachineStatus_Model
    {

        public MouldingMachineStatus_Model()
        {

        }

        private string _machineID;
        private DateTime _day;
        private string  _shift;
        private string _machineStatus;
        private string _OEEStatus;
        private DateTime _StartTime;
        private DateTime _EndTime;
        private string _PartNo;
        private string  _UserID;
        private string _Remark;


        public string MachineID
        {
            set { _machineID = value; }
            get { return _machineID; }
        }

        public DateTime Day
        {
            set { _day = value; }
            get { return _day; }
        }
        public string Shift
        {
            set { _shift = value; }
            get { return _shift; }
        }
        public string MachineStatus
        {
            set { _machineStatus = value; }
            get { return _machineStatus; }
        }

        public string OEEStatus
        {
            set { _OEEStatus = value; }
            get { return _OEEStatus; }
        }
        public DateTime StartTime
        {
            set { _StartTime = value; }
            get { return _StartTime; }
        }
        public DateTime EndTime
        {
            set { _EndTime = value; }
            get { return _EndTime; }
        }


        public string PartNo
        {
            set { _PartNo = value; }
            get { return _PartNo; }
        }
        public string UserID
        {
            set { _UserID = value; }
            get { return _UserID; }
        }
        public string Remark
        {
            set { _Remark = value; }
            get { return _Remark; }
        }

    }
}
