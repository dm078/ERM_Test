using System.IO;
using System;
using DataInterface;
using System.Collections.Generic;

namespace Utilities
{
    public class IDataLoader
    {
        private List<LinearProgram> _filteredLinearProgramList = new List<LinearProgram>();
        private List<TimeOfUsage> _filteredTimeOfUsageList = new List<TimeOfUsage>();
        private decimal _medianValueForLinearProgram = (decimal)0.00;
        private decimal _medianValueForTimeOfUsage = (decimal)0.00;

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
        /// Read the files located in the provided folder and load them into the Business Objects accordingly
        /// </summary>
        /// <param name="folderToUse"></param>
        public virtual void ReadCSVFolderAndLoadLinearProgramFiles(string folderToUse)
        {
            if (ValidateFolderPathToUse(folderToUse))
            {
                string[] _fileEntries = Directory.GetFiles(folderToUse, "LP*.csv", SearchOption.TopDirectoryOnly);
                foreach (string _item in _fileEntries)
                {
                    LoadLinearProgramFileContent(_item);
                }
            }
        }

        /// <summary>
        /// Validate the Folder Path provided is valid and not null. Event Log any issues.
        /// </summary>
        /// <param name="folderToUse"></param>
        /// <returns></returns>
        public virtual bool ValidateFolderPathToUse(string folderToUse)
        {
            bool _result = true;

            if ((folderToUse is null) || (folderToUse.Length < 2) || (!Directory.Exists(folderToUse)))
            {
                _result = false;
                EventLogger _eventLogger = new EventLogger();
                _eventLogger.LogMessage("ERM_TimeOfUse_Filter Application : Folder path provided in invalid. Please investigate.", true);
            }

            return _result;
        }

        /// <summary>
        /// Read the files located in the provided folder and load them into the Business Objects accordingly
        /// </summary>
        /// <param name="folderToUse"></param>
        public virtual void ReadCSVFolderAndTimeOfUsageLoadFiles(string folderToUse)
        {
            if (ValidateFolderPathToUse(folderToUse))
            {
                string[] _fileEntries = Directory.GetFiles(folderToUse, "TOU*.csv", SearchOption.TopDirectoryOnly);
                foreach (string _item in _fileEntries)
                {
                    LoadTimeOfUsageFileContent(_item);                
                }
            }
        }

        /// <summary>
        /// Retrieve the final filtered Linear Program List
        /// </summary>
        /// <returns></returns>
        public virtual List<LinearProgram> GetFilteredLinearProgramList()
        {
            DataCalculator _tempDataCalculator = new DataCalculator();
            List<LinearProgram> _tempList = new List<LinearProgram>();

            if (_filteredLinearProgramList.Count>0)
            {
                _tempList = _tempDataCalculator.FilterLinearProgramListAbove20PercentageMedian(_filteredLinearProgramList);
                _medianValueForLinearProgram = _tempDataCalculator.GetCalculatedMedianForLinearProgram();
            }            

            return _tempList;
        }

        /// <summary>
        /// Retrieve the final filtered Time of Usage List
        /// </summary>
        /// <returns></returns>
        public virtual List<TimeOfUsage> GetFilteredTimeOfUsageList()
        {
            DataCalculator _tempDataCalculator = new DataCalculator();
            List<TimeOfUsage> _tempList = new List<TimeOfUsage>();

            if (_filteredTimeOfUsageList.Count > 0)
            {
                _tempList = _tempDataCalculator.FilterTimeOfUsageListAbove20PercentageMedian(_filteredTimeOfUsageList);
                _medianValueForTimeOfUsage = _tempDataCalculator.GetCalculatedMedianForTimeOfUsage();
            }                
                
            return _tempList;
        }

        /// <summary>
        /// Clear the Final Result Lists before using it
        /// </summary>
        public virtual void ResetFilteredLists()
        {
            _filteredLinearProgramList.Clear();
            _filteredTimeOfUsageList.Clear();
        }

        /// <summary>
        /// Reads the file provided and load in into the Liner Program Business Object. It will also filter out the header and not process it.
        /// </summary>
        /// <param name="filepath"></param>        
        public virtual void LoadLinearProgramFileContent(string filepath)
        {
            //TODO:Dynamic type??? overkill??
            using (StreamReader _reader = new StreamReader(filepath))
            {
                string _line = null;
                while (null != (_line = _reader.ReadLine()))
                {
                    string[] _values = _line.Split(',');

                    //bypass the header - both files the first column header starts with Meter
                    if (_values[0].ToString().ToLower().Substring(0, 5) != "meter")
                    {                       
                        LinearProgram _tempLinearProgram = new LinearProgram();
                        _tempLinearProgram.FileName = Path.GetFileName(filepath);
                        _tempLinearProgram.MeterPointCode = _values[0].ToString();
                        _tempLinearProgram.SerialNumber = _values[1].ToString();
                        _tempLinearProgram.PlantCode = _values[2].ToString();
                        _tempLinearProgram.Timestamp = Convert.ToDateTime(_values[3].ToString());
                        _tempLinearProgram.DataType = _values[4].ToString();
                        _tempLinearProgram.DataValue = Convert.ToDecimal(_values[5].ToString());
                        _tempLinearProgram.Units = (UnitType)Enum.Parse(typeof(UnitType), _values[6].ToString(), true);
                        _tempLinearProgram.Status = _values[7].ToString();
                        _filteredLinearProgramList.Add(_tempLinearProgram);                     
                    }
                }
            }
        }

        /// <summary>
        /// Reads the file provided and load in into the Time Of Usage Business Object. It will also filter out the header and not process it.
        /// </summary>
        /// <param name="filepath"></param>        
        public virtual void LoadTimeOfUsageFileContent(string filepath)
        {
            //TODO:Dynamic type??? overkill??
            using (StreamReader _reader = new StreamReader(filepath))
            {
                string _line = null;
                while (null != (_line = _reader.ReadLine()))
                {
                    string[] _values = _line.Split(',');

                    //bypass the header - both files the first column header starts with Meter
                    if (_values[0].ToString().ToLower().Substring(0, 5) != "meter")
                    {                                              
                        TimeOfUsage _tempTimeOfUsage = new TimeOfUsage();
                        _tempTimeOfUsage.FileName = Path.GetFileName(filepath);
                        _tempTimeOfUsage.MeterPointCode = _values[0].ToString();
                        _tempTimeOfUsage.SerialNumber = _values[1].ToString();
                        _tempTimeOfUsage.PlantCode = _values[2].ToString();
                        _tempTimeOfUsage.Timestamp = Convert.ToDateTime(_values[3].ToString());
                        _tempTimeOfUsage.DataType = _values[4].ToString();
                        _tempTimeOfUsage.Energy = Convert.ToDecimal(_values[5].ToString());
                        _tempTimeOfUsage.MaximumDemand = Convert.ToDecimal(_values[6].ToString());
                        _tempTimeOfUsage.TimeOfMaxDemand = Convert.ToDateTime(_values[7].ToString());
                        _tempTimeOfUsage.Units = (UnitType)Enum.Parse(typeof(UnitType), _values[8].ToString(), true);
                        _tempTimeOfUsage.Status = _values[9].ToString();
                        _tempTimeOfUsage.UsagePeriod = (Period)Enum.Parse(typeof(Period), _values[10].ToString().Replace(" ", ""), true);
                        _tempTimeOfUsage.DLSActive = Convert.ToBoolean(_values[11].ToString());
                        _tempTimeOfUsage.BillingResetCount = Convert.ToInt32(_values[12].ToString());
                        _tempTimeOfUsage.BillingResetDateTime = Convert.ToDateTime(_values[13].ToString());
                        _tempTimeOfUsage.Rate = (RateType)Enum.Parse(typeof(RateType), _values[14].ToString().Replace(" ", ""), true);
                        _filteredTimeOfUsageList.Add(_tempTimeOfUsage);                       
                    }
                }
            }
        }
    }
}
