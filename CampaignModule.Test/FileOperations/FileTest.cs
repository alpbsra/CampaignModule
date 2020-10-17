using NUnit.Framework;
using System.Collections.Generic;
using CampaignModule.Core.Objects;
using CampaignModule.App;
using System.Linq;

namespace CampaignModule.Test.FileOperations
{
    public class FileTest
    {
        private Operations _operation;
        
        [SetUp]
        public void Setup()
        {
            _operation = new Operations();
        }

        [Test]
        [TestCase("1")]
        [TestCase("2")]
        [TestCase("3")]
        [TestCase("4")]
        public void ReadCommandsFromScenarioFile_ExistingFileTest(string fileNumber)
        {
            var cmdList = _operation.ReadCommandsFromScenarioFile(fileNumber);

            Assert.AreNotEqual(cmdList.Count(), 0);
        }

        [Test]
        [TestCase("5")]
        public void ReadCommandsFromScenarioFileTest_NotExistingFileTest(string fileNumber)
        {
            var cmdList = _operation.ReadCommandsFromScenarioFile(fileNumber);

            Assert.AreEqual(cmdList.Count(), 0);
        }

        [TearDown]
        public void TearDown() 
        {
            _operation = null;
        }
    }
}
