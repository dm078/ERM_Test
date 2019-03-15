using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities;

namespace ERM_UnitTests
{
    [TestClass]
    public class PositiveTests
    {
        [TestMethod]
        public void Standard_File_Load()
        {
            string CSVSourceFolderPathToUse = @"C:\\Users\\Alex Tan\\source\\repos\\ERM_TimeOfUse_Filter\\ERM_TimeOfUse_Filter\\Data Files";
            DataLoader _dataLoader = new DataLoader();
            _dataLoader.ResetFilteredLists();
            _dataLoader.ReadCSVFolderAndLoadLinearProgramFiles(CSVSourceFolderPathToUse);
            _dataLoader.ReadCSVFolderAndTimeOfUsageLoadFiles(CSVSourceFolderPathToUse);

            Assert.IsTrue(_dataLoader.GetFilteredLinearProgramList().Count > 0);
            Assert.IsTrue(_dataLoader.GetFilteredTimeOfUsageList().Count > 0);            
        }

        [TestMethod]
        public void Standard_File_Load_Record_Count_LinearProgram()
        {
            string CSVSourceFolderPathToUse = @"C:\\Users\\Alex Tan\\source\\repos\\ERM_TimeOfUse_Filter\\ERM_TimeOfUse_Filter\\Data Files";
            DataLoader _dataLoader = new DataLoader();
            _dataLoader.ResetFilteredLists();
            _dataLoader.ReadCSVFolderAndLoadLinearProgramFiles(CSVSourceFolderPathToUse);            
            Assert.AreEqual(_dataLoader.GetFilteredLinearProgramList().Count,179);
        }

        [TestMethod]
        public void Standard_File_Load_Record_Count_TimeOfUsage()
        {
            string CSVSourceFolderPathToUse = @"C:\\Users\\Alex Tan\\source\\repos\\ERM_TimeOfUse_Filter\\ERM_TimeOfUse_Filter\\Data Files";
            DataLoader _dataLoader = new DataLoader();
            _dataLoader.ResetFilteredLists();            
            _dataLoader.ReadCSVFolderAndTimeOfUsageLoadFiles(CSVSourceFolderPathToUse);

            Assert.AreEqual(_dataLoader.GetFilteredTimeOfUsageList().Count, 6);
        }
    }
}
