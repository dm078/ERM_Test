using System;

namespace DataInterface
{        
    public enum UnitType
    {
        A,
        deg,
        kVa,
        kvar,
        kvarh,
        kW,
        kWh,
        V
    }

    public class LinearProgram
    {
        private string _filename = "";
        private string _meterPointCode = "";
        private string _serialNumber = "";
        private string _plantCode = "";
        private DateTime _timestamp = DateTime.MinValue;
        private string _dataType = "";
        private decimal _dataValue = (decimal)0.00;
        private UnitType _units;
        private string _status = "";

        public string FileName
        {
            get { return _filename; }
            set { _filename = value; }
        }

        public string MeterPointCode
        {
            get { return _meterPointCode; }
            set { _meterPointCode = value; }
        }

        public string SerialNumber
        {
            get { return _serialNumber; }
            set { _serialNumber = value; }
        }

        public string PlantCode
        {
            get { return _plantCode; }
            set { _plantCode = value; }
        }

        public DateTime Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }

        public string DataType
        {
            get { return _dataType; }
            set { _dataType = value; }
        }

        public Decimal DataValue
        {
            get { return _dataValue; }
            set { _dataValue = value; }
        }

        public UnitType Units
        {
            get { return _units; }
            set { _units = value; }
        }

        /// <summary>
        /// Please note that the status can be a empty string, PL II or GN
        /// </summary>
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
    }
}
