using System;
using System.Configuration;
using Utilities;
using DataInterface;

namespace ERM_TimeOfUse_Filter
{
    class Program
    {
        static void Main(string[] args)
        {
            //Load the data into the appropriate Business Objects as required
            DataLoader _dataLoader = new DataLoader();
            _dataLoader.ResetFilteredLists();
            _dataLoader.ReadCSVFolderAndLoadLinearProgramFiles(ConfigurationSettings.AppSettings["CSVSourceFolderPath"].ToString());
            _dataLoader.ReadCSVFolderAndTimeOfUsageLoadFiles(ConfigurationSettings.AppSettings["CSVSourceFolderPath"].ToString());

            //Write the Header for the console
            Console.WriteLine("      File Name                          Time Stamp           Value    Median Value  ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------");

            //Write out the results into the Console + Filtering of the results
            foreach (LinearProgram _item in _dataLoader.GetFilteredLinearProgramList())
            {
                Console.WriteLine(String.Format("{0,38}", _item.FileName) + "  " + _item.Timestamp.ToString("MM/dd/yyyy hh:mm tt") + "  " + _item.DataValue + "  " + _dataLoader.GetCalculatedMedianForLinearProgram().ToString());
            }
            foreach (TimeOfUsage _item in _dataLoader.GetFilteredTimeOfUsageList())
            {
                Console.WriteLine(String.Format("{0,38}", _item.FileName) + "  " + _item.Timestamp.ToString("MM/dd/yyyy hh:mm tt") + "  " + _item.Energy + "  " + _dataLoader.GetCalculatedMedianForTimeOfUsage().ToString());
            }

            //Wait for the ENTER key press to close the console
            Console.WriteLine();            
            Console.WriteLine("  Yeah! Baby! It's Alive! ");
            Console.ReadLine();
        }
    }
}
