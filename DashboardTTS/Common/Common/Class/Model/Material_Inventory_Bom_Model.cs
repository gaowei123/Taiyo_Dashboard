using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Common.Model
{
    /// <summary>
    /// Material_Inventory_Bom:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public  class Material_Inventory_Bom
    {
        public Material_Inventory_Bom()
        { }
        #region Model
        private string _material_part;
        private decimal? _unit_price_usd;
        private decimal? _unit_price;
        private string _updated_user;
        private DateTime _updated_time;
        private string _ref_field01; // Resin Type

        private decimal? _makeUp;
        private decimal? _exchangeRate;

        private string _ref_field02;
        private string _ref_field03;
        private string _ref_field04;
        private string _ref_field05;
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
        /// 

        public decimal? Unit_Price_USD
        {
            set { _unit_price_usd = value; }
            get { return _unit_price_usd; }
        }

        public decimal? Unit_Price
        {
            set { _unit_price = value; }
            get { return _unit_price; }
        }


        public decimal? MakeUp
        {
            set { _makeUp = value; }
            get { return _makeUp; }
        }

        public decimal? ExchangeRate
        {
            set { _exchangeRate = value; }
            get { return _exchangeRate; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Updated_User
        {
            set { _updated_user = value; }
            get { return _updated_user; }
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
        public string REF_FIELD01
        {
            set { _ref_field01 = value; }
            get { return _ref_field01; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string REF_FIELD02
        {
            set { _ref_field02 = value; }
            get { return _ref_field02; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string REF_FIELD03
        {
            set { _ref_field03 = value; }
            get { return _ref_field03; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string REF_FIELD04
        {
            set { _ref_field04 = value; }
            get { return _ref_field04; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string REF_FIELD05
        {
            set { _ref_field05 = value; }
            get { return _ref_field05; }
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

