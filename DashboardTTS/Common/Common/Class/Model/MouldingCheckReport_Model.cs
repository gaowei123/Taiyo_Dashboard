using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Class.Model
{
    public class MouldingCheckReport_Model
    {
        public MouldingCheckReport_Model()
        { }
        #region Model
        private int _id;
        private string _report;
        private string _verify_user;
        private string _verify_id;
        private DateTime _verify_time;
        private DateTime _reffield01;
        private string _reffield02;
        private string _reffield03;
        private string _reffield04;
        private string _reffield05;
        private string _remarks;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Report
        {
            set { _report = value; }
            get { return _report; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Verify_User
        {
            set { _verify_user = value; }
            get { return _verify_user; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Verify_ID
        {
            set { _verify_id = value; }
            get { return _verify_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Verify_Time
        {
            set { _verify_time = value; }
            get { return _verify_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime refField01
        {
            set { _reffield01 = value; }
            get { return _reffield01; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string refField02
        {
            set { _reffield02 = value; }
            get { return _reffield02; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string refField03
        {
            set { _reffield03 = value; }
            get { return _reffield03; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string refField04
        {
            set { _reffield04 = value; }
            get { return _reffield04; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string refField05
        {
            set { _reffield05 = value; }
            get { return _reffield05; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }
        #endregion Model



    }
}
