using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Class.Model
{
    public class MouldingViHistory_Model
    {
        public MouldingViHistory_Model()
        { }

        private int _id;
        private string _machineID;
        private DateTime _dateTime;
        private string _partNumber;
        private string _partNumberAll;
        private string _jigNo;
        private string _model;
        private double _cavityCount;
        private double _partsWeight;
        private double _parts2Weight;
        private double _cycleTime;
        private double _targetQty;
        private string _userName;
        private string _userID;
        private double _lastQTY;
        private double _acountReading;
        private double _rejectQty;
        private double _QCNGQTY;
        private double _acceptQty;
        private DateTime _startTime;
        private DateTime _stopTime;
        private string _day;
        private string _shift;
        private string _status;
        private string _matPart01;
        private string _matPart02;
        private string _matLot01;
        private string _matLot02;
        private string _opSign01;
        private string _opSign02;
        private string _opSign03;
        private string _opSign04;
        private string _opSign05;
        private string _opSign06;
        private string _opSign07;
        private string _opSign08;
        private string _opSign09;
        private string _opSign10;
        private string _opSign11;
        private string _opSign12;

        private string _qaSign01;
        private string _qaSign02;
        private string _qaSign03;
        private string _qaSign04;
        private string _qaSign05;
        private string _qaSign06;
        private string _qaSign07;
        private string _qaSign08;
        private string _qaSign09;
        private string _qaSign10;
        private string _qaSign11;
        private string _qaSign12;

        private string _customer;
        private DateTime _lastUpdatedTime;
        private string _trackingID;
        private string _remarks;
        private string _Material_MHCheck;
        private DateTime _Material_MHCheckTime;
        private string _Material_Opcheck;
        private DateTime _Material_OpCheckTime;
        private string _Material_LeaderCheck;
        private DateTime _Material_LeaderCheckTime;
        private string _LeaderCheck;
        private DateTime _LeaderCheckTime;
        private string _SupervisorCheck;
        private DateTime _SupervisorCheckTime;


        private string _OkAccumulation;
        private double _setup;
        private double _wastageMaterial01;
        private double _wastageMaterial02;
        private string _refField01;
        private string _refField02;
        private string _refField03;
        private string _refField04;
        private string _refField05;
        public int  ID
        {
            set { _id = value; }
            get { return _id; }
        }

        public string MachineID
        {
            set { _machineID = value; }
            get { return _machineID; }
        }
        public DateTime Datetime
        {
            set { _dateTime = value; }
            get { return _dateTime; }
        }
        public string PartNumber
        {
            set { _partNumber = value; }
            get { return _partNumber; }
        }

        public string PartNumberAll
        {
            set { _partNumberAll = value; }
            get { return _partNumberAll; }
        }

        public string JigNo
        {
            set { _jigNo = value; }
            get { return _jigNo; }
        }
        public string Model
        {
            set { _model = value; }
            get { return _model; }
        }
        public double CavityCount
        {
            set { _cavityCount = value; }
            get { return _cavityCount; }
        }

        public double Parts2Weight
        {
            set { _parts2Weight = value; }
            get { return _parts2Weight; }
        }
        public double PartsWeight
        {
            set { _partsWeight = value; }
            get { return _partsWeight; }
        }
        public double CycleTime
        {
            set { _cycleTime = value; }
            get { return _cycleTime; }
        }
        public double TargetQty
        {
            set { _targetQty = value; }
            get { return _targetQty; }
        }
        public string UserName
        {
            set { _userName = value; }
            get { return _userName; }
        }
        public string UserID
        {
            set { _userID = value; }
            get { return _userID; }
        }

        public string OkAccumulation
        {
            set { _OkAccumulation = value; }
            get { return _OkAccumulation; }
        }
        public double AcountReading
        {
            set { _acountReading = value; }
            get { return _acountReading; }
        }

        public double RejectQty
        {
            set { _rejectQty = value; }
            get { return _rejectQty; }
        }
        public double lastQty
        {
            set { _lastQTY = value; }
            get { return _lastQTY; }
        }
        public double QCNGQTY
        {
            set { _QCNGQTY = value; }
            get { return _QCNGQTY; }
        }
        public double AcceptQty
        {
            set { _acceptQty = value; }
            get { return _acceptQty; }
        }
        public DateTime StartTime
        {
            set { _startTime = value; }
            get { return _startTime; }
        }
        public DateTime StopTime
        {
            set { _stopTime = value; }
            get { return _stopTime; }
        }
        public string Day
        {
            set { _day = value; }
            get { return _day; }
        }
        public string Shift
        {
            set { _shift = value; }
            get { return _shift; }
        }
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }
        public string MatPart01
        {
            set { _matPart01 = value; }
            get { return _matPart01; }
        }
        public string MatPart02
        {
            set { _matPart02 = value; }
            get { return _matPart02; }
        }
        public string MatLot01
        {
            set { _matLot01 = value; }
            get { return _matLot01; }
        }
        public string MatLot02
        {
            set { _matLot02 = value; }
            get { return _matLot02; }
        }
        public string OpSign01
        {
            set { _opSign01 = value; }
            get { return _opSign01; }
        }
        public string OpSign02
        {
            set { _opSign02 = value; }
            get { return _opSign02; }
        }
        public string OpSign03
        {
            set { _opSign03 = value; }
            get { return _opSign03; }
        }
        public string OpSign04
        {
            set { _opSign04 = value; }
            get { return _opSign04; }
        }
        public string OpSign05
        {
            set { _opSign05 = value; }
            get { return _opSign05; }
        }
        public string OpSign06
        {
            set { _opSign06 = value; }
            get { return _opSign06; }
        }
        public string OpSign07
        {
            set { _opSign07 = value; }
            get { return _opSign07; }
        }
        public string OpSign08
        {
            set { _opSign08 = value; }
            get { return _opSign08; }
        }
        public string OpSign09
        {
            set { _opSign09 = value; }
            get { return _opSign09; }
        }
        public string OpSign10
        {
            set { _opSign10 = value; }
            get { return _opSign10; }
        }
        public string OpSign11
        {
            set { _opSign11 = value; }
            get { return _opSign11; }
        }
        public string OpSign12
        {
            set { _opSign12 = value; }
            get { return _opSign12; }
        }
        public string QaSign01
        {
            set { _qaSign01 = value; }
            get { return _qaSign01; }
        }
        public string QaSign02
        {
            set { _qaSign02 = value; }
            get { return _qaSign02; }
        }
        public string QaSign03
        {
            set { _qaSign03 = value; }
            get { return _qaSign03; }
        }
        public string QaSign04
        {
            set { _qaSign04 = value; }
            get { return _qaSign04; }
        }
        public string QaSign05
        {
            set { _qaSign05 = value; }
            get { return _qaSign05; }
        }
        public string QaSign06
        {
            set { _qaSign06 = value; }
            get { return _qaSign06; }
        }
        public string QaSign07
        {
            set { _qaSign07 = value; }
            get { return _qaSign07; }
        }
        public string QaSign08
        {
            set { _qaSign08 = value; }
            get { return _qaSign08; }
        }
        public string QaSign09
        {
            set { _qaSign09 = value; }
            get { return _qaSign09; }
        }
        public string QaSign10
        {
            set { _qaSign10 = value; }
            get { return _qaSign10; }
        }
        public string QaSign11
        {
            set { _qaSign11 = value; }
            get { return _qaSign11; }
        }
        public string QaSign12
        {
            set { _qaSign12 = value; }
            get { return _qaSign12; }
        }


        public string Customer
        {
            set { _customer = value; }
            get { return _customer; }
        }
        public DateTime LastUpdatedTime
        {
            set { _lastUpdatedTime = value; }
            get { return _lastUpdatedTime; }
        }
        public string TrackingID
        {
            set { _trackingID = value; }
            get { return _trackingID; }
        }
        public string Remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }
        public string Material_MHCheck
        {
            set { _Material_MHCheck = value; }
            get { return _Material_MHCheck; }
        }

        public DateTime Material_MHCheckTime
        {
            set { _Material_MHCheckTime = value; }
            get { return _Material_MHCheckTime; }
        }
        public string Material_Opcheck
        {
            set { _Material_Opcheck = value; }
            get { return _Material_Opcheck; }
        }

        public DateTime Material_OpCheckTime
        {
            set { _Material_OpCheckTime = value; }
            get { return _Material_OpCheckTime; }
        }
        public string Material_LeaderCheck
        {
            set { _Material_LeaderCheck = value; }
            get { return _Material_LeaderCheck; }
        }
        public DateTime Material_LeaderCheckTime
        {
            set { _Material_LeaderCheckTime = value; }
            get { return _Material_LeaderCheckTime; }
        }
        public string LeaderCheck
        {
            set { _LeaderCheck = value; }
            get { return _LeaderCheck; }
        }
        public DateTime LeaderCheckTime
        {
            set { _LeaderCheckTime = value; }
            get { return _LeaderCheckTime; }
        }
        public string SupervisorCheck
        {
            set { _SupervisorCheck = value; }
            get { return _SupervisorCheck; }
        }
        public DateTime SupervisorCheckTime
        {
            set { _SupervisorCheckTime = value; }
            get { return _SupervisorCheckTime; }
        }

        public double Setup
        {
            set { _setup = value; }
            get { return _setup; }
        }      

        public double WastageMaterial01
        {
            set { _wastageMaterial01 = value; }
            get { return _wastageMaterial01; }
        }

        public double WastageMaterial02
        {
            set { _wastageMaterial02 = value; }
            get { return _wastageMaterial02; }
        }
        public string refField01
        {
            set { _refField01 = value; }
            get { return _refField01; }
        }
        public string refField02
        {
            set { _refField02 = value; }
            get { return _refField02; }
        }
        public string refField03
        {
            set { _refField03 = value; }
            get { return _refField03; }
        }
        public string refField04
        {
            set { _refField04 = value; }
            get { return _refField04; }
        }
        public string refField05
        {
            set { _refField05 = value; }
            get { return _refField05; }
        }
    }
}
