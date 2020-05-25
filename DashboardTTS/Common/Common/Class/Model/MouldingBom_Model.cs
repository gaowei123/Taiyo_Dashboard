using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Class.Model
{
     public  class MouldingBom_Model
    {

        public MouldingBom_Model()
        {

        }

        private string _partNumberAll;
        private string _partNumber;
        private string _matPart01;
        private string _matPart02;
        
        private double _materialWeight01;
        private double _materialWeight02;
        
        private string _customer;
        private string _model;
        private string _jigNo;
        private double _cavityCount;
        private double _partsWeight;
        private double _cycleTime;
        private decimal? _blockCount;
        private decimal? _unitCount;
        private string _machineID;
        private string _userName;
        private DateTime _dateTime;
        private string _remarks;
        private string _suppiller;
        private string _refField01;
        private string _refField02;
        private string _refField03;
        private string _refField04;
        private string _refField05;






        public double materialWeight01
        {
            set { _materialWeight01 = value; }
            get { return _materialWeight01; }
        }
        public double materialWeight02
        {
            set { _materialWeight02 = value; }
            get { return _materialWeight02; }
        }




        public string partNumberAll
        {
            set { _partNumberAll = value; }
            get { return _partNumberAll; }
        }

        public string jigNo
        {
            set { _jigNo = value; }
            get { return _jigNo; }
        }


        public string partNumber
        {
            set { _partNumber = value; }
            get { return _partNumber; }
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
        public string customer
        {
            set { _customer = value; }
            get { return _customer; }
        }
        public string model
        {
            set { _model = value; }
            get { return _model; }
        }
        public double cavityCount
        {
            set { _cavityCount = value; }
            get { return _cavityCount; }
        }
        public double partsWeight
        {
            set { _partsWeight = value; }
            get { return _partsWeight; }
        }

        public double cycleTime
        {
            set { _cycleTime = value; }
            get { return _cycleTime; }
        }
        public decimal? blockCount
        {
            set { _blockCount = value; }
            get { return _blockCount; }
        }

        public decimal? unitCount
        {
            set { _unitCount = value; }
            get { return _unitCount; }
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

        public string remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }

        public string suppiller
        {
            set { _suppiller = value; }
            get { return _suppiller; }
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
