using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Class.Model
{
    public class MouldingMachineInformation_Model
    {
        public MouldingMachineInformation_Model()
        {

        }
        private string _partModel;

        private string _machineID;
        private string _type;
        private string _makerModel;
        private string _maker;
        private string _model;
        private string _serialNo;
        private string _date;
        private string _CTRL;
        private DateTime _updatedDate;
        private string _controllerType;
        private string _controllerSerialNo;
        private DateTime _DateOfManu;

        private string _info;
        private decimal? _ScrewDiameter;
        private decimal? _MaxOPNStroke;
        private decimal? _EJTStroke;
        private string _TiebarDistance;
        private string _MinMoldSize;
        private decimal? _MinMoldThickness;
        private string _Dimensions;


        public string Info
        {
            set { _info = value; }
            get { return _info; }
        }
        public decimal? ScrewDiameter
        {
            set { _ScrewDiameter = value; }
            get { return _ScrewDiameter; }
        }
        public decimal? MaxOPNStroke
        {
            set { _MaxOPNStroke = value; }
            get { return _MaxOPNStroke; }
        }
        public string Dimensions
        {
            set { _Dimensions = value; }
            get { return _Dimensions; }
        }
        public decimal? EJTStroke
        {
            set { _EJTStroke = value; }
            get { return _EJTStroke; }
        }
        public string TiebarDistance
        {
            set { _TiebarDistance = value; }
            get { return _TiebarDistance; }
        }

        public string MinMoldSize
        {
            set { _MinMoldSize = value; }
            get { return _MinMoldSize; }
        }
        public decimal? MinMoldThickness
        {
            set { _MinMoldThickness = value; }
            get { return _MinMoldThickness; }
        }

      

        public string PartModel
        {
            set { _partModel = value; }
            get { return _partModel; }
        }
        public DateTime DateOfManu
        {
            set { _DateOfManu = value; }
            get { return _DateOfManu; }
        }

        public string MachineID
        {
            set { _machineID = value; }
            get { return _machineID; }
        }

        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
        public string MakerModel
        {
            set { _makerModel = value; }
            get { return _makerModel; }
        }
        public string Maker
        {
            set { _maker = value; }
            get { return _maker; }
        }
        public string Model
        {
            set { _model = value; }
            get { return _model; }
        }
        public string SerialNo
        {
            set { _serialNo = value; }
            get { return _serialNo; }
        }
        public string  Date
        {
            set { _date = value; }
            get { return _date; }
        }

        public string CTRL
        {
            set { _CTRL = value; }
            get { return _CTRL; }
        }

        public DateTime UpdatedDate
        {
            set { _updatedDate = value; }
            get { return _updatedDate; }
        }
        public string ControllerType
        {
            set { _controllerType = value; }
            get { return _controllerType; }
        }
        public string ControllerSerialNo
        {
            set { _controllerSerialNo = value; }
            get { return _controllerSerialNo; }
        }
    }
}
