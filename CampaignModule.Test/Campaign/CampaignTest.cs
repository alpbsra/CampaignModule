using NUnit.Framework;
using System.Collections.Generic;
using CampaignModule.Core.Objects;
using CampaignModule.App;
using CampaignModule.Core.Factory;

namespace CampaignModule.Test
{
    public class CampaignTest
    {
        private List<Product> _productList;
        private List<Campaign> _campaignList;
        private List<Order> _orderList;
        private Operations _operation;

        [SetUp]
        public void Setup()
        {
            _productList = new List<Product>();
            _campaignList = new List<Campaign>();
            _orderList = new List<Order>();

            _operation = new Operations();
        }

        [Test]
        [TestCase("C1", "P1", 10, 20, 100)]
        [TestCase("C2", "P2", 5, 10, 200)]
        public void CreateCampaignTest(string name, string productCode, int duration, int pmLimit, int targetSaleCount)
        {
            _productList.Add(new Product { Code = "P1", Price = 100, RawPrice = 100, Stock = 1000 });
            _productList.Add(new Product { Code = "P2", Price = 5000, RawPrice = 5000, Stock = 10 });

            string command = string.Format("create_campaign {0} {1} {2} {3} {4}", name, productCode, duration, pmLimit, targetSaleCount);
            string result = string.Format("Campaign created; name {0}, product {1}, duration {2},limit {3}, target sales count {4}", name, productCode, duration, pmLimit, targetSaleCount);

            Assert.AreEqual(_operation.ExecuteCommand(command, new TimeHandler(), _productList, _campaignList, _orderList), result);
        }

        [Test]
        [TestCase("C1", "P1", 10, 20, 100)]
        [TestCase("C2", "P2", 5, 10, 200)]
        [TestCase("C3", "P2", 5, 10, 200)]
        public void CreateCampaignTest_CampaignIsAlreadyExist(string name, string productCode, int duration, int pmLimit, int targetSaleCount)
        {
            _productList.Add(new Product { Code = "P1", Price = 100, RawPrice = 100, Stock = 1000 });
            _campaignList.Add(new Campaign { Name = "C1", ProductCode = "P1", Duration = 10, PMLimit = 20, TargetSalesCount = 100 });
            _campaignList.Add(new Campaign { Name = "C2", ProductCode = "P2", Duration = duration, PMLimit = pmLimit, TargetSalesCount = targetSaleCount });
            _campaignList.Add(new Campaign { Name = "C3", ProductCode = "P5", Duration = duration, PMLimit = pmLimit, TargetSalesCount = targetSaleCount });

            string command = string.Format("create_campaign {0} {1} {2} {3} {4}", name, productCode, duration, pmLimit, targetSaleCount);
            string result = string.Format("Campaign {0} is already exist", name);

            Assert.AreEqual(_operation.ExecuteCommand(command, new TimeHandler(), _productList, _campaignList, _orderList), result);
        }


        [Test]
        [TestCase("C3", "P3", 0, 0, 0)]
        [TestCase("C4", "p4", 10, 50, 100)]
        public void CreateCampaignTest_ProductIsNotExist(string name, string productCode, int duration, int pmLimit, int targetSaleCount)
        {
            string command = string.Format("create_campaign {0} {1} {2} {3} {4}", name, productCode, duration, pmLimit, targetSaleCount);
            string result = string.Format("Product {0} not found", productCode);

            Assert.AreEqual(_operation.ExecuteCommand(command, new TimeHandler(), _productList, _campaignList, _orderList), result);
        }

        [Test]
        public void CreateCampaignTest_ParametersAreMissing()
        {
            string command = string.Format("create_campaign {0} {1} {2} {3}", "C1", "P1", 10, 10);
            string result = "Command cannot be executed : Parameters are missing or invalid";

            Assert.AreEqual(_operation.ExecuteCommand(command, new TimeHandler(), _productList, _campaignList, _orderList), result);
        }

        [Test]
        public void CreateCampaignTest_ParametersAreInvalid()
        {
            string command = string.Format("create_campaign {0} {1} {2} {3} {4}", "C1", "P1", 10, 10, "test");
            string result = "Command cannot be executed : Parameters are missing or invalid";

            Assert.AreEqual(_operation.ExecuteCommand(command, new TimeHandler(), _productList, _campaignList, _orderList), result);
        }

        [Test]
        [TestCase("create_campaign")]
        public void CreateCampaignFactoryCreator_AreSameTest(string commandType)
        {
            FactoryCreator CommandCreator = new FactoryCreator();
            BaseCommands commandModel = CommandCreator.CommandFactory(commandType);

            Assert.AreSame(typeof(CreateCampaign), commandModel.GetType());
        }

        [Test]
        [TestCase("create_product")]
        public void CreateCampaignFactoryCreator_AreNotSameTest(string commandType)
        {
            FactoryCreator CommandCreator = new FactoryCreator();
            BaseCommands commandModel = CommandCreator.CommandFactory(commandType);

            Assert.AreNotSame(typeof(CreateCampaign), commandModel.GetType());
        }

        [Test]
        [TestCase("test")]
        [TestCase("")]
        public void CreateCampaignFactoryCreator_IsNullTest(string commandType)
        {
            FactoryCreator CommandCreator = new FactoryCreator();
            BaseCommands commandModel = CommandCreator.CommandFactory(commandType);

            Assert.IsNull(commandModel);
        }


        [TearDown]
        public void TearDown()
        {
            _productList = null; 
            _campaignList = null;
            _orderList = null;
        }
    }
}
