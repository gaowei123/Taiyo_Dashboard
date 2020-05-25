using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Class.Model
{
    public class LMMSBomDetail_Model
    {
        public LMMSBomDetail_Model()
        {

        }
        private int _sn;
        private string _partNumber;
        private string _materialPartNo;
        private int _partCount;
        private string _userName;
        private DateTime _dateTime;
        private string _remarks;

        public int sn
        {
            set { _sn = value; }
            get { return _sn; }
        }
        public string MaterialPartNo
        {
            set { _materialPartNo = value; }
            get { return _materialPartNo; }
        }

        public int PartCount
        {
            set { _partCount = value; }
            get { return _partCount; }
        }
        public string remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }

        public string partNumber
        {
            set { _partNumber = value; }
            get { return _partNumber; }
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
