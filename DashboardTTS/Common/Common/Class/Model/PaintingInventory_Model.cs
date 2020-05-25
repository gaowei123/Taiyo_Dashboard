using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Class.Model
{
    public class PaintingInventory_Model
    {
        public PaintingInventory_Model()
        {

        }

        private string _jobNumber;
        private string _partNumber;
        private string _description;
        private double _quantity;
        private DateTime _startOnTime;
        private DateTime _dateTime;
        private int _pqcQuantity;
        private string _day;
        private string _month;
        private string _year;
        private string _showFlag;

        private string _lotno;



        public string ShowFlag
        {
            set { _showFlag = value; }
            get { return _showFlag; }
        }

        public string Lotno
        {
            set { _lotno = value; }
            get { return _lotno; }
        }

        public int PQCQuantity
        {
            set { _pqcQuantity = value; }
            get { return _pqcQuantity; }
        }
        public string JobNumber
        {
            set { _jobNumber = value; }
            get { return _jobNumber; }
        }
        public string partNumber
        {
            set { _partNumber = value; }
            get { return _partNumber; }
        }

        public string description
        {
            set { _description = value; }
            get { return _description; }
        }

        public double quantity
        {
            set { _quantity = value; }
            get { return _quantity; }
        }
        public DateTime startOnTime
        {
            set { _startOnTime = value; }
            get { return _startOnTime; }
        }
        public DateTime dateTime
        {
            set { _dateTime = value; }
            get { return _dateTime; }
        }
        public string day
        {
            set { _day = value; }
            get { return _day; }
        }
        public string month
        {
            set { _month = value; }
            get { return _month; }
        }
        public string year
        {
            set { _year = value; }
            get { return _year; }
        }

    }
}

