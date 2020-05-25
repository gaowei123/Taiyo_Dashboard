using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Class.Model
{
    /// <summary>
    /// MouldingTransfer:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class MouldingTransfer
    {
        public MouldingTransfer()
        { }
        #region Model
        private string _material_part;
        private string _transfer_to;
        private DateTime? _transfer_date;
        private string _quantity;
        private string _opr_id;
        private DateTime? _production_date;
        private string _annealing_process;
        private DateTime? _annealing_date_from;
        private DateTime? _annealing_date_to;
        private string _annealing_temperature;
        private string _update_user;
        private string _status;
        private string _reffield01;
        private string _reffield02;
        private string _reffield03;
        private string _reffield04;
        private string _reffield05;
        private string _reffield06;
        private string _reffield07;
        private string _remarks;
        /// <summary>
        /// 
        /// </summary>
        public string Material_Part
        {
            set { _material_part = value; }
            get { return _material_part; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Transfer_To
        {
            set { _transfer_to = value; }
            get { return _transfer_to; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? Transfer_Date
        {
            set { _transfer_date = value; }
            get { return _transfer_date; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Quantity
        {
            set { _quantity = value; }
            get { return _quantity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Opr_ID
        {
            set { _opr_id = value; }
            get { return _opr_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? Production_Date
        {
            set { _production_date = value; }
            get { return _production_date; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Annealing_Process
        {
            set { _annealing_process = value; }
            get { return _annealing_process; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? Annealing_Date_From
        {
            set { _annealing_date_from = value; }
            get { return _annealing_date_from; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? Annealing_Date_To
        {
            set { _annealing_date_to = value; }
            get { return _annealing_date_to; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Annealing_Temperature
        {
            set { _annealing_temperature = value; }
            get { return _annealing_temperature; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Update_User
        {
            set { _update_user = value; }
            get { return _update_user; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string refField01
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
        public string refField06
        {
            set { _reffield06 = value; }
            get { return _reffield06; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string refField07
        {
            set { _reffield07 = value; }
            get { return _reffield07; }
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


