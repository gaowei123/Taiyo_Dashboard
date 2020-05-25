using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Class.Model
{
    /// <summary>
    /// Material_Inventory:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Material_Inventory
    {
        public Material_Inventory()
        { }
        #region Model
        private string _material_No;
        private string _material_LotNo;
        private decimal? _inventory_weight;
        private decimal? _transaction_weight;
        private string _last_event;
        private DateTime? _load_time;
        private DateTime _updated_time;
        private string _supplier;
        private string _machineID;
        private string _remarks;
        private string _user_name;
        private string _REF_FIELD01;
        private string _REF_FIELD02;
        private string _REF_FIELD03;


        /// <summary>
        /// 
        /// </summary>
        public string Material_No
        {
            set { _material_No = value; }
            get { return _material_No; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Inventory_Weight
        {
            set { _inventory_weight = value; }
            get { return _inventory_weight; }
        }

        public decimal? Transaction_Weight
        {
            set { _transaction_weight = value; }
            get { return _transaction_weight; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string User_Name
        {
            set { _user_name = value; }
            get { return _user_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Last_Event
        {
            set { _last_event = value; }
            get { return _last_event; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? Load_Time
        {
            set { _load_time = value; }
            get { return _load_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Updated_Time
        {
            set { _updated_time = value; }
            get { return _updated_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Material_LotNo
        {
            set { _material_LotNo = value; }
            get { return _material_LotNo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Supplier
        {
            set { _supplier = value; }
            get { return _supplier; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MachineID
        {
            set { _machineID = value; }
            get { return _machineID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }

        public string REF_FIELD01
        {
            set { _REF_FIELD01 = value; }
            get { return _REF_FIELD01; }
        }

        public string REF_FIELD02
        {
            set { _REF_FIELD02 = value; }
            get { return _REF_FIELD02; }
        }

        public string REF_FIELD03
        {
            set { _REF_FIELD03 = value; }
            get { return _REF_FIELD03; }
        }

        #endregion Model

    }
}

