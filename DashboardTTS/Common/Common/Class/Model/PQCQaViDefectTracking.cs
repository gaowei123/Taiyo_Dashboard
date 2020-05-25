using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Class.Model
{
    public class PQCQaViDefectTracking
    {
        private int _id;
        private string _trackingID;
        private string _machineID;
        private DateTime _dateTime;
        private string _partNumber;
        private string _jigNo;
        private string _model;
        private string _cavityCount;
        private string _userName;
        private string _userID;
        private DateTime _startTime;
        private DateTime _stopTime;
        private DateTime _day;
        private string _shift;
        private string _status;
        private string _matPart01;
        private string _matPart02;
        private string _remark_1;
        private string _remark_2;
        private string _defectCodeID;
        private string _defectCode;
        private string _rejectQty;
        private string _rejectQtyHour01;
        private string _rejectQtyHour02;
        private string _rejectQtyHour03;
        private string _rejectQtyHour04;
        private string _rejectQtyHour05;
        private string _rejectQtyHour06;
        private string _rejectQtyHour07;
        private string _rejectQtyHour08;
        private string _rejectQtyHour09;
        private string _rejectQtyHour10;
        private string _rejectQtyHour11;
        private string _rejectQtyHour12;
        private DateTime _lastUpdatedTime;
        private string _remarks;
        private string _processes;
        private string _jobId;
        private string _totalQty;
        private DateTime _updatedTime;

        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        public string trackingID
        {
            set { _trackingID = value; }
            get { return _trackingID; }
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
        public DateTime startTime
        {
            set { _startTime = value; }
            get { return _startTime; }
        }
        public DateTime stopTime
        {
            set { _stopTime = value; }
            get { return _stopTime; }
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
        public string defectCodeID
        {
            set { _defectCodeID = value; }
            get { return _defectCodeID; }
        }
        public string defectCode
        {
            set { _defectCode = value; }
            get { return _defectCode; }
        }
        public string rejectQty
        {
            set { _rejectQty = value; }
            get { return _rejectQty; }
        }
        public string rejectQtyHour01
        {
            set { _rejectQtyHour01 = value; }
            get { return _rejectQtyHour01; }
        }
        public string rejectQtyHour02
        {
            set { _rejectQtyHour02 = value; }
            get { return _rejectQtyHour02; }
        }
        public string rejectQtyHour03
        {
            set { _rejectQtyHour03 = value; }
            get { return _rejectQtyHour03; }
        }
        public string rejectQtyHour04
        {
            set { _rejectQtyHour04 = value; }
            get { return _rejectQtyHour04; }
        }
        public string rejectQtyHour05
        {
            set { _rejectQtyHour05 = value; }
            get { return _rejectQtyHour05; }
        }
        public string rejectQtyHour06
        {
            set { _rejectQtyHour06 = value; }
            get { return _rejectQtyHour06; }
        }
        public string rejectQtyHour07
        {
            set { _rejectQtyHour07 = value; }
            get { return _rejectQtyHour07; }
        }
        public string rejectQtyHour08
        {
            set { _rejectQtyHour08 = value; }
            get { return _rejectQtyHour08; }
        }
        public string rejectQtyHour09
        {
            set { _rejectQtyHour09 = value; }
            get { return _rejectQtyHour09; }
        }
        public string rejectQtyHour10
        {
            set { _rejectQtyHour10 = value; }
            get { return _rejectQtyHour10; }
        }
        public string rejectQtyHour11
        {
            set { _rejectQtyHour11 = value; }
            get { return _rejectQtyHour11; }
        }
        public string rejectQtyHour12
        {
            set { _rejectQtyHour12 = value; }
            get { return _rejectQtyHour12; }
        } 
        public DateTime lastUpdatedTime
        {
            set { _lastUpdatedTime = value; }
            get { return _lastUpdatedTime; }
        }
        public string remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }
      
        public string processes
        {
            set { _processes = value; }
            get { return _processes; }
        }
        public string jobId
        {
            set { _jobId = value; }
            get { return _jobId; }
        }
        public string totalQty
        {
            set { _totalQty = value; }
            get { return _totalQty; }
        }
        public DateTime updatedTime
        {
            set { _updatedTime = value; }
            get { return _updatedTime; }
        }
    }
}
