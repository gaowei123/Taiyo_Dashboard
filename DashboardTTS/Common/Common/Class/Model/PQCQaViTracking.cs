using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Class.Model
{
    public class PQCQaViTracking
    {
        private int _id;
        private string _machineID;
        private DateTime _dateTime;
        private string _partNumber;
        private string _jobId;
        private string _processes;
        private string _jigNo;
        private string _model;
        private string _cavityCount;
        private string _partsWeight;
        private string _cycleTime;
        private string _targetQty;
        private string _userName;
        private string _userID;
        private string _TotalQty;
        private string _rejectQty;
        private string _acceptQty;
        private DateTime _startTime;
        private DateTime? _stopTime;
        private string _nextViFlag;
        private DateTime _day;
        private string _shift;
        private string _status;
        private string _matPart01;
        private string _matPart02;
        private string _remark_1;
        private string _remark_2;
        private string _refField01;
        private string _refField02;
        private string _refField03;
        private string _refField04;
        private string _refField05;
        private string _refField06;
        private string _refField07;
        private string _refField08;
        private string _refField09;
        private string _refField10;
        private string _refField11;
        private string _refField12;
        private string _customer;
        private DateTime _lastUpdatedTime;
        private string _trackingID;
        private string _lastTrackingID;
        private string _remarks;
        private string _department;
        private string _totalRejectQty;
        private DateTime _updatedTime;

        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        public string machineID
        {
            set { _machineID = value; }
            get { return _machineID; }
        }
        public DateTime dateTime
        {
            set { _dateTime = value; }
            get { return _dateTime; }
        }
        public string partNumber
        {
            set { _partNumber = value; }
            get { return _partNumber; }
        }
        public string jobId
        {
            set { _jobId = value; }
            get { return _jobId; }
        }
        public string processes
        {
            set { _processes = value; }
            get { return _processes; }
        }
        public string jigNo
        {
            set { _jigNo = value; }
            get { return _jigNo; }
        }
        public string model
        {
            set { _model = value; }
            get { return _model; }
        }
        public string cavityCount
        {
            set { _cavityCount = value; }
            get { return _cavityCount; }
        }
        public string partsWeight
        {
            set { _partsWeight = value; }
            get { return _partsWeight; }
        }
        public string cycleTime
        {
            set { _cycleTime = value; }
            get { return _cycleTime; }
        }
        public string targetQty
        {
            set { _targetQty = value; }
            get { return _targetQty; }
        }
        public string userName
        {
            set { _userName = value; }
            get { return _userName; }
        }
        public string userID
        {
            set { _userID = value; }
            get { return _userID; }
        }
        public string TotalQty
        {
            set { _TotalQty = value; }
            get { return _TotalQty; }
        }
        public string rejectQty
        {
            set { _rejectQty = value; }
            get { return _rejectQty; }
        }
        public string acceptQty
        {
            set { _acceptQty = value; }
            get { return _acceptQty; }
        }
        public DateTime startTime
        {
            set { _startTime = value; }
            get { return _startTime; }
        }
        public DateTime? stopTime
        {
            set { _stopTime = value; }
            get { return _stopTime; }
        }
        public string nextViFlag
        {
            set { _nextViFlag = value; }
            get { return _nextViFlag; }
        }
        public DateTime day
        {
            set { _day = value; }
            get { return _day; }
        }
        public string shift
        {
            set { _shift = value; }
            get { return _shift; }
        }
        public string status
        {
            set { _status = value; }
            get { return _status; }
        }
        public string matPart01
        {
            set { _matPart01 = value; }
            get { return _matPart01; }
        }
        public string matPart02
        {
            set { _matPart02 = value; }
            get { return _matPart02; }
        }
        public string remark_1
        {
            set { _remark_1 = value; }
            get { return _remark_1; }
        }
        public string remark_2
        {
            set { _remark_2 = value; }
            get { return _remark_2; }
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
        public string refField06
        {
            set { _refField06 = value; }
            get { return _refField06; }
        }
        public string refField07
        {
            set { _refField07 = value; }
            get { return _refField07; }
        }
        public string refField08
        {
            set { _refField08 = value; }
            get { return _refField08; }
        }
        public string refField09
        {
            set { _refField09 = value; }
            get { return _refField09; }
        }
        public string refField10
        {
            set { _refField10 = value; }
            get { return _refField10; }
        }
        public string refField11
        {
            set { _refField11 = value; }
            get { return _refField11; }
        }
        public string refField12
        {
            set { _refField12 = value; }
            get { return _refField12; }
        }
        public string customer
        {
            set { _customer = value; }
            get { return _customer; }
        }
        public DateTime lastUpdatedTime
        {
            set { _lastUpdatedTime = value; }
            get { return _lastUpdatedTime; }
        }
        public string trackingID
        {
            set { _trackingID = value; }
            get { return _trackingID; }
        }
        public string lastTrackingID
        {
            set { _lastTrackingID = value; }
            get { return _lastTrackingID; }
        }
        public string remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }
        public string department
        {
            set { _department = value; }
            get { return _department; }
        }
        public string totalRejectQty
        {
            set { _totalRejectQty = value; }
            get { return _totalRejectQty; }
        }
        public DateTime updatedTime
        {
            set { _updatedTime = value; }
            get { return _updatedTime; }
        }
    }
}
