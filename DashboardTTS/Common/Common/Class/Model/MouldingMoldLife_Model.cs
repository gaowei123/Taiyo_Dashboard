using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Class.Model
{
    public class MouldingMoldLife_Model
    {

        private string _MoldID;
        private string _MachineID;
        private string _PartNumberAll;
        private int _Accumulate;
        private int _Clean1Qty;
        private DateTime? _Clean1Time;
        private string _Clean1TimeBy;
        private int _Clean2Qty;
        private DateTime? _Clean2Time;
        private string _Clean2TimeBy;
        private int _Clean3Qty;
        private DateTime? _Clean3Time;
        private string _Clean3TimeBy;
        private int _Clean4Qty;
        private DateTime? _Clean4Time;
        private string _Clean4TimeBy;
        private int _Clean5Qty;
        private DateTime? _Clean5Time;
        private string _Clean5TimeBy;
        private int _ChangeQty;
        private DateTime? _ChangeTime;
        private string _ChangeBy;
        private DateTime _CreateTime;
        private DateTime _UpdatedTime;



       


       

        public string MoldID
        {
            set { _MoldID = value; }
            get { return _MoldID; }
        }
      
        public string MachineID
        {
            set { _MachineID = value; }
            get { return _MachineID; }
        }
        public string PartNumberAll
        {
            set { _PartNumberAll = value; }
            get { return _PartNumberAll; }
        }

        public int Accumulate
        {
            set { _Accumulate = value; }
            get { return _Accumulate; }
        }


        public int Clean1Qty
        {
            set { _Clean1Qty = value; }
            get { return _Clean1Qty; }
        }
        public DateTime? Clean1Time
        {
            set { _Clean1Time = value; }
            get { return _Clean1Time; }
        }
        public int Clean2Qty
        {
            set { _Clean2Qty = value; }
            get { return _Clean2Qty; }
        }
        public DateTime? Clean2Time
        {
            set { _Clean2Time = value; }
            get { return _Clean2Time; }
        }
        public int Clean3Qty
        {
            set { _Clean3Qty = value; }
            get { return _Clean3Qty; }
        }
        public DateTime? Clean3Time
        {
            set { _Clean3Time = value; }
            get { return _Clean3Time; }
        }
        public int Clean4Qty
        {
            set { _Clean4Qty = value; }
            get { return _Clean4Qty; }
        }
        public DateTime? Clean4Time
        {
            set { _Clean4Time = value; }
            get { return _Clean4Time; }
        }
        public int Clean5Qty
        {
            set { _Clean5Qty = value; }
            get { return _Clean5Qty; }
        }
        public DateTime? Clean5Time
        {
            set { _Clean5Time = value; }
            get { return _Clean5Time; }
        }

        public int ChangeQty
        {
            set { _ChangeQty = value; }
            get { return _ChangeQty; }
        }
        public DateTime? ChangeTime
        {
            set { _ChangeTime = value; }
            get { return _ChangeTime; }
        }

        public string Clean1TimeBy
        {
            set { _Clean1TimeBy = value; }
            get { return _Clean1TimeBy; }
        }
        public string Clean2TimeBy
        {
            set { _Clean2TimeBy = value; }
            get { return _Clean2TimeBy; }
        }
        public string Clean3TimeBy
        {
            set { _Clean3TimeBy = value; }
            get { return _Clean3TimeBy; }
        }
        public string Clean4TimeBy
        {
            set { _Clean4TimeBy = value; }
            get { return _Clean4TimeBy; }
        }
        public string Clean5TimeBy
        {
            set { _Clean5TimeBy = value; }
            get { return _Clean5TimeBy; }
        }
        public string ChangeBy
        {
            set { _ChangeBy = value; }
            get { return _ChangeBy; }
        }
        public DateTime CreateTime
        {
            set { _CreateTime = value; }
            get { return _CreateTime; }
        }
        public DateTime UpdatedTime
        {
            set { _UpdatedTime = value; }
            get { return _UpdatedTime; }
        }

     
    }
}
