using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Class.Model
{
    public class PQCBomDetail_Model
    {
        public PQCBomDetail_Model()
        { }

        #region Model
        private int _sn;
        private string _partNumber;
        private string _materialPartNo;
        private decimal _partCount;
        private string _userName;
        private DateTime _dateTime;
        private byte[] _partImage;
        private string _color;
        private string _imagePath;
        private string _imageAbsolutePath;

        private string _materialName;
        private string _customer;
        private int _outerBoxQty;
        private string _packingTrays;
        private string _module;


        public string materialName
        {
            get { return _materialName; }
            set { _materialName = value; }
        }

        public string customer
        {
            get { return _customer; }
            set { _customer = value; }
        }

        public int outerBoxQty
        {
            get { return _outerBoxQty; }
            set { _outerBoxQty = value; }
        }

        public string packingTrays
        {
            get { return _packingTrays; }
            set { _packingTrays = value; }
        }

        public string module
        {
            get { return _module; }
            set { _module = value; }
        }


        public string imageAbsolutePath
        {
            get { return _imageAbsolutePath; }
            set { _imageAbsolutePath = value; }
        }
        public string  imagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; }
        }
        public int sn
        {
            get { return _sn; }
            set { _sn = value; }
        }
        public string partNumber
        {
            get { return _partNumber; }
            set { _partNumber = value; }
        }
        public string materialPartNo
        {
            get { return _materialPartNo; }
            set { _materialPartNo = value; }
        }
        public decimal partCount
        {
            get { return _partCount; }
            set { _partCount = value; }
        }
        public string userName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        public DateTime dateTime
        {
            get { return _dateTime; }
            set { _dateTime = value; }
        }
        public byte[] partImage
        {
            get { return _partImage; }
            set { _partImage = value; }
        }
        public string color
        {
            get { return _color; }
            set { _color = value; }
        }
        #endregion
    }
}
