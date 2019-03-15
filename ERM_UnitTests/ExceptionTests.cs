using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities;
namespace ERM_UnitTests
{
    [TestClass]
    public class ExceptionTests
    {
        [TestMethod]
        public void Empty_Folder_Given()
        {
            string CSVSourceFolderPathToUse = @"C:\\temp";
            DataLoader _dataLoader = new DataLoader();

            _dataLoader.ResetFilteredLists();                                    
            _dataLoader.ReadCSVFolderAndLoadLinearProgramFiles(CSVSourceFolderPathToUse);
            _dataLoader.ReadCSVFolderAndTimeOfUsageLoadFiles(CSVSourceFolderPathToUse);

            Assert.AreEqual(_dataLoader.GetFilteredLinearProgramList().Count , 0);
            Assert.AreEqual(_dataLoader.GetFilteredTimeOfUsageList().Count, 0);
        }
    }
}
