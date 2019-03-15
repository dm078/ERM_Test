using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities;

namespace ERM_UnitTests
{
    [TestClass]
    public class NegativeTests
    {
        [TestMethod]
        public void Invalid_Null_Folder_Given()
        {            
            DataLoader _dataLoader = new DataLoader();                        
            Assert.AreEqual(_dataLoader.ValidateFolderPathToUse(null), false);            
        }

        [TestMethod]
        public void Invalid_Non_Existing_Folder_Given()
        {
            string CSVSourceFolderPathToUse = @"C:\\Use2222rs\\Alex Tan\\source";
            DataLoader _dataLoader = new DataLoader();
            Assert.AreEqual(_dataLoader.ValidateFolderPathToUse(CSVSourceFolderPathToUse), false);
        }
    }
}
