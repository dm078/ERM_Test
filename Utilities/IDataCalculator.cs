using System.Collections.Generic;
using DataInterface;

namespace Utilities
{
    public class IDataCalculator
    {
        internal decimal _medianValueForLinearProgram = (decimal)0.00;
        internal decimal _medianValueForTimeOfUsage = (decimal)0.00;

        /// <summary>
        /// Filter the list based on the Linear Program Generic list or the Time Of Usage Generic List accordingly
        /// </summary>
        /// <param name="unfilteredList"></param>
        /// <returns></returns>
        public virtual List<LinearProgram> FilterLinearProgramListAbove20PercentageMedian(List<LinearProgram> unfilteredList)
        {
            //TODO:Dynamic type??? overkill??
            List<LinearProgram> _finalResultList = new List<LinearProgram>();

            List<decimal> _tempDataValues = new List<decimal>();
            decimal _medianValue = (decimal)0.00;
            decimal _medianValueLow = (decimal)0.00;
            decimal _medianValueHigh = (decimal)0.00;

            //Retrieve the Median Value for the List first
            foreach (LinearProgram _item in unfilteredList)
            {
                _tempDataValues = AppendDecimalList(_tempDataValues, _item.DataValue);
            }

            _medianValue = GetDecimalMedianValue(_tempDataValues);
            _medianValueLow = GetDecimalMedianLowValue(_medianValue);
            _medianValueHigh = GetDecimalMedianHighValue(_medianValue);

            //Append the Result List            
            _medianValueForLinearProgram = _medianValue;
            foreach (LinearProgram _item in unfilteredList)
            {
                if (ValueWithin20PercentRange(_item.DataValue, _medianValueLow, _medianValueHigh))
                {
                    _finalResultList.Add(_item);
                }
            }

            return _finalResultList;
        }

        /// <summary>
        /// Validate if the data value is within the 20 percentage range. This take into accounts the Median Low and High Values calculated.
        /// </summary>
        /// <param name="valueToCompare"></param>
        /// <param name="medianValueLow"></param>
        /// <param name="medianValueHigh"></param>
        /// <returns></returns>
        public virtual bool ValueWithin20PercentRange(decimal valueToCompare,decimal medianValueLow, decimal medianValueHigh)
        {
            bool _result = false;

            if ((valueToCompare >= medianValueLow) && (valueToCompare <= medianValueHigh))
            {
                _result = true;
            }

            return _result;
        }

        /// <summary>
        /// Appends the existing Decimal List for the Median Calculation
        /// </summary>
        /// <param name="tempDataValueList"></param>
        /// <param name="decimalValue"></param>
        /// <returns></returns>
        public virtual List<decimal> AppendDecimalList(List<decimal> tempDataValueList, decimal decimalValue)
        {
            List<decimal> _tempDecimalList = tempDataValueList;

            if (decimalValue > 0)
            {
                _tempDecimalList.Add(decimalValue);
            }

            return _tempDecimalList;
        }
        
        /// <summary>
        /// Filter the list based on the Linear Program Generic list or the Time Of Usage Generic List accordingly
        /// </summary>
        /// <param name="unfilteredList"></param>
        /// <returns></returns>
        public virtual List<TimeOfUsage> FilterTimeOfUsageListAbove20PercentageMedian(List<TimeOfUsage> unfilteredList)
        {
            //TODO:Dynamic type??? overkill??
            List<TimeOfUsage> _finalResultList = new List<TimeOfUsage>();

            List<decimal> _tempDataValues = new List<decimal>();
            decimal _medianValue = (decimal)0.00;
            decimal _medianValueLow = (decimal)0.00;
            decimal _medianValueHigh = (decimal)0.00;

            //Retrieve the Median Value for the List first
            foreach (TimeOfUsage _item in (List<TimeOfUsage>)unfilteredList)
            {
                _tempDataValues = AppendDecimalList(_tempDataValues, _item.Energy);
            }

            _medianValue = GetDecimalMedianValue(_tempDataValues);
            _medianValueLow = GetDecimalMedianLowValue(_medianValue);
            _medianValueHigh = GetDecimalMedianHighValue(_medianValue);

            //Append the Result List            
            List<TimeOfUsage> _resultList = new List<TimeOfUsage>();
            _medianValueForTimeOfUsage = _medianValue;
            foreach (TimeOfUsage _item in (List<TimeOfUsage>)unfilteredList)
            {
                if (ValueWithin20PercentRange(_item.Energy, _medianValueLow, _medianValueHigh))
                {
                    _resultList.Add(_item);
                }
            }

            _finalResultList = _resultList;            

            return _finalResultList;
        }

        /// <summary>
        /// Shared function to retrieve the median value based on the provided list of decimal values
        /// </summary>
        /// <param name="listOfDecimalValues"></param>
        /// <returns></returns>
        public virtual decimal GetDecimalMedianValue(List<decimal> listOfDecimalValues)
        {
            decimal _result = (decimal)0.00;

            listOfDecimalValues.Sort();

            int _counter = listOfDecimalValues.Count;
            if (_counter % 2 == 0)
            {
                // count is even, average two middle elements                
                decimal _range1 = listOfDecimalValues[listOfDecimalValues.Count / 2 - 1];
                decimal _range2 = listOfDecimalValues[listOfDecimalValues.Count / 2];
                _result = (_range1 + _range2) / 2m;
            }
            else
            {
                // count is odd, return the middle element
                _result = listOfDecimalValues[_counter / 2];
            }

            return _result;
        }

        /// <summary>
        /// returns the calculated median value for the Linear Program Data
        /// </summary>
        /// <returns></returns>
        public virtual decimal GetCalculatedMedianForLinearProgram()
        {
            return _medianValueForLinearProgram;
        }

        /// <summary>
        /// returns the calculated median value for the Time of Usage Data
        /// </summary>
        /// <returns></returns>
        public virtual decimal GetCalculatedMedianForTimeOfUsage()
        {
            return _medianValueForTimeOfUsage;
        }

        /// <summary>
        /// Calculates the Median value minus 20%
        /// </summary>
        /// <param name="medianValue"></param>
        /// <returns></returns>
        public virtual decimal GetDecimalMedianLowValue(decimal medianValue)
        {
            decimal _result = (decimal)0.00;
            _result = medianValue - (medianValue * 20 / 100);
            return _result;
        }

        /// <summary>
        /// Calculates the Median value plus 20%
        /// </summary>
        /// <param name="medianValue"></param>
        /// <returns></returns>
        public virtual decimal GetDecimalMedianHighValue(decimal medianValue)
        {
            decimal _result = (decimal)0.00;
            _result = medianValue + (medianValue * 20 / 100);
            return _result;
        }
    }
}
