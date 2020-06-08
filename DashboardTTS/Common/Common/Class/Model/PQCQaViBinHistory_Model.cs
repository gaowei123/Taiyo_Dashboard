using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Class.Model
{
    public partial class PQCQaViBinHistory_Model
    {
        public PQCQaViBinHistory_Model()
        {

        }

        #region Model
        private string _id;
        private string _trackingid;
        private string _processes;
        private string _jobid;
        private string _partnumber;
        private string _materialpartno;
        private string _materialname;
        private string _shipto;
        private string _model;
        private string _jigno;
        private decimal? _materialqty;
        private string _status;
        private string _nextviflag;
        private DateTime? _datetime;
        private DateTime? _day;
        private string _shift;
        private string _username;
        private string _userid;
        private string _remark_1;
        private string _remark_2;
        private string _remark_3;
        private string _remark_4;
        private string _remarks;
        private DateTime? _updatedtime;
        private decimal? _materialFromQty;
        /// <summary>
        /// 
        /// </summary>
        public string id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string trackingID
        {
            set { _trackingid = value; }
            get { return _trackingid; }
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
        public string jobId
        {
            set { _jobid = value; }
            get { return _jobid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PartNumber
        {
            set { _partnumber = value; }
            get { return _partnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string materialPartNo
        {
            set { _materialpartno = value; }
            get { return _materialpartno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string materialName
        {
            set { _materialname = value; }
            get { return _materialname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string shipTo
        {
            set { _shipto = value; }
            get { return _shipto; }
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
        public decimal? materialQty
        {
            set { _materialqty = value; }
            get { return _materialqty; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string nextViFlag
        {
            set { _nextviflag = value; }
            get { return _nextviflag; }
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
        public DateTime? day
        {
            set { _day = value; }
            get { return _day; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string shift
        {
            set { _shift = value; }
            get { return _shift; }
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
        public string userID
        {
            set { _userid = value; }
            get { return _userid; }
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
        /// <summary>
        /// 
        /// </summary>
        public DateTime? updatedTime
        {
            set { _updatedtime = value; }
            get { return _updatedtime; }
        }

        public decimal? materialFromQty
        {
            get
            {
                return _materialFromQty;
            }

            set
            {
                _materialFromQty = value;
            }
        }
        #endregion Model
    }
}
