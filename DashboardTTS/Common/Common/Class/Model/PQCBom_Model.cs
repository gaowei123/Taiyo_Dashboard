
using System;
namespace Common.Class.Model
{
    /// <summary>
    /// PQCBom_Model:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class PQCBom_Model
    {
        public PQCBom_Model()
        { }
        #region Model
        private string _partnumber;
        private string _customer;
        private string _model;
        private string _jigno;
        private decimal? _cavitycount;
        private decimal? _cycletime;
        private int? _blockcount;
        private int? _unitcount;
        private string _machineid;
        private string _username;
        private string _color;
        private string _processes;
        private DateTime? _datetime;
        private string _remark_1;
        private string _remark_2;
        private string _remark_3;
        private string _remark_4;
        private string _remarks;

        private string _shipTo;
        private string _type;

        private string _description;
        private string _coat;

        private string _number;
        private decimal _unitCost;


        public string Number
        {
            set { _number = value; }
            get { return _number; }
        }

        public decimal UnitCost
        {
            set { _unitCost = value; }
            get { return _unitCost; }
        }


        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }

        public string Coating
        {
            set { _coat = value; }
            get { return _coat; }
        }


        public string ShipTo
        {
            set { _shipTo = value; }
            get { return _shipTo; }
        }

        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }


        public string partNumber
        {
            set { _partnumber = value; }
            get { return _partnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string customer
        {
            set { _customer = value; }
            get { return _customer; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string model
        {
            set { _model = value; }
            get { return _model; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string jigNo
        {
            set { _jigno = value; }
            get { return _jigno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? cavityCount
        {
            set { _cavitycount = value; }
            get { return _cavitycount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? cycleTime
        {
            set { _cycletime = value; }
            get { return _cycletime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? blockCount
        {
            set { _blockcount = value; }
            get { return _blockcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? unitCount
        {
            set { _unitcount = value; }
            get { return _unitcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string machineID
        {
            set { _machineid = value; }
            get { return _machineid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string userName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dateTime
        {
            set { _datetime = value; }
            get { return _datetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string color
        {
            set { _color = value; }
            get { return _color; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string processes
        {
            set { _processes = value; }
            get { return _processes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string remark_1
        {
            set { _remark_1 = value; }
            get { return _remark_1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string remark_2
        {
            set { _remark_2 = value; }
            get { return _remark_2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string remark_3
        {
            set { _remark_3 = value; }
            get { return _remark_3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string remark_4
        {
            set { _remark_4 = value; }
            get { return _remark_4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }
        #endregion Model
    }
}

