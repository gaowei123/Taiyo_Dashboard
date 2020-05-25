using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Class.Model
{
    public class LMMSBom_Model
    {
        public LMMSBom_Model()
        {

        }

        private string _partNumber;
        private double _cycleTime;
        private double _blockUnit;
        private int _blockCount;
        private int _unitCount;
        private string _module;
        private string _machineID;
        private string _userName;
        private DateTime _dateTime;
        private string _remarks;
        private string _type;
        private string _customer;

        private string _lighting;
        private string _camera;
        private string _currentPower;

        private string _supplier;
        private string _partBelongTo;

        private string _number;




        public string Supplier
        {
            set { _supplier = value; }
            get { return _supplier; }
        }
        public string PartBelongTo
        {
            set { _partBelongTo = value; }
            get { return _partBelongTo; }
        }

        public string Number
        {
            set { _number = value; }
            get { return _number; }
        }






        public string Customer
        {
            set { _customer = value; }
            get { return _customer; }
        }
        
        public string Lighting
        {
            set { _lighting = value; }
            get { return _lighting; }
        }

        public string Camera
        {
            set { _camera = value; }
            get { return _camera; }
        }

        public string CurrentPower
        {
            set { _currentPower = value; }
            get { return _currentPower; }
        }

        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }

        public string  module
        {
            set { _module = value; }
            get { return _module; }
        }
        public int blockCount
        {
            set { _blockCount = value; }
            get { return _blockCount; }
        }

        public int unitCount
        {
            set { _unitCount = value; }
            get { return _unitCount; }
        }
        public string remarks
        {
            set { _remarks = value; }
            get {return _remarks;}
        }

        public string partNumber
        {
            set { _partNumber = value; }
            get { return _partNumber; }
        }

        public double cycleTime
        {
            set { _cycleTime = value; }
            get { return _cycleTime; }
        }
        public double blockUnit
        {
            set { _blockUnit = value; }
            get { return _blockUnit; }
        }
        public string machineID
        {
            set { _machineID = value; }
            get { return _machineID; }
        }
        public string userName
        {
            set { _userName = value; }
            get { return _userName; }
        }
        public DateTime dateTime
        {
            set { _dateTime = value; }
            get { return _dateTime; }
        }

    }
}
