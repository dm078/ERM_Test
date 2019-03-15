using System;

namespace DataInterface
{
    public enum Period
    {
        Total,
        BillingTotal,
        Current,
        Previous
    }

    public enum RateType
    {
        Unified,
        Rate1,
        Rate2,
        Rate3
    }

    public class TimeOfUsage
    {
        private string _fileName = "";
        private string _meterPointCode = "";
        private string _serialNumber = "";
        private string _plantCode = "";
        private DateTime _timestamp = DateTime.MinValue;
        private string _dataType = "";
        private decimal _energy = (decimal)0.00;
        private decimal _maximumDemand = (decimal)0.00;
        private DateTime _timeOfMaxDemand = DateTime.MinValue;
        private UnitType _units;
        private string _status = "";
        private Period _usagePeriod;
        private bool _dlsActive;
        private int _billingResetCount = 0;
        private DateTime _billingResetDateTime = DateTime.MinValue;
        private RateType _rate;

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
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

        public Decimal Energy
        {
            get { return _energy; }
            set { _energy = value; }
        }

        public Decimal MaximumDemand
        {
            get { return _maximumDemand; }
            set { _maximumDemand = value; }
        }

        public DateTime TimeOfMaxDemand
        {
            get { return _timeOfMaxDemand; }
            set { _timeOfMaxDemand = value; }
        }

        public UnitType Units
        {
            get { return _units; }
            set { _units = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public Period UsagePeriod
        {
            get { return _usagePeriod; }
            set { _usagePeriod = value; }
        }

        public bool DLSActive
        {
            get { return _dlsActive; }
            set { _dlsActive = value; }
        }

        public int BillingResetCount
        {
            get { return _billingResetCount; }
            set { _billingResetCount = value; }
        }

        public DateTime BillingResetDateTime
        {
            get { return _billingResetDateTime; }
            set { _billingResetDateTime = value; }
        }

        public RateType Rate
        {
            get { return _rate; }
            set { _rate = value; }
        }
    }

}
