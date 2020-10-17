using NUnit.Framework;
using System.Collections.Generic;
using CampaignModule.Core.Objects;
using CampaignModule.App;

namespace CampaignModule.Test
{
    public class OrderTest
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
        [TestCase("P1", 100)]
        [TestCase("P2", 20)]
        public void CreateOrderTest(string productCode, int Quantitiy)
        {
            _productList.Add(new Product { Code = "P1", Price = 100, RawPrice = 100, Stock = 1000 });
            _productList.Add(new Product { Code = "P2", Price = 100, RawPrice = 100, Stock = 50 });

            string command = string.Format("create_order {0} {1}", productCode, Quantitiy);
            string result = string.Format("Order created; product {0}, quantity {1}", productCode, Quantitiy);

            Assert.AreEqual(_operation.ExecuteCommand(command, new TimeHandler(), _productList, _campaignList, _orderList), result);
        }


        [Test]
        public void CreateOrderTest_ParametersAreMissing()
        {
            string command = string.Format("create_order {0}", "P1");
            string result = "Command cannot be executed : Parameters are missing or invalid";

            Assert.AreEqual(_operation.ExecuteCommand(command, new TimeHandler(), _productList, _campaignList, _orderList), result);
        }

        [Test]
        public void CreateOrderTest_ParametersAreInvalid()
        {
            string command = string.Format("create_order {0} {1}", "P1", "test");
            string result = "Command cannot be executed : Parameters are missing or invalid";

            Assert.AreEqual(_operation.ExecuteCommand(command, new TimeHandler(), _productList, _campaignList, _orderList), result);
        }

        [Test]
        [TestCase("P1", 100)]
        public void CreateOrderTest_ParametersAreInvalid(string productCode, int Quantitiy)
        {
            string command = string.Format("create_order {0} {1}", "P1", "test");
            string result = "Command cannot be executed : Parameters are missing or invalid";

            Assert.AreEqual(_operation.ExecuteCommand(command, new TimeHandler(), _productList, _campaignList, _orderList), result);
        }

        [Test]
        [TestCase("P1", 0)]
        public void CreateOrderTest_InvalidQuantity(string productCode, int Quantitiy)
        {
            _productList.Add(new Product { Code = "P1", Price = 100, RawPrice = 100, Stock = 1000 });

            string command = string.Format("create_order {0} {1}", productCode, Quantitiy);
            string result = "Command cannot be executed : Parameters are missing or invalid";

            Assert.AreEqual(_operation.ExecuteCommand(command, new TimeHandler(), _productList, _campaignList, _orderList), result);
        }

        [Test]
        [TestCase("P1", 100)]
        [TestCase("P2", 100)]
        public void CreateOrderTest_NotEnoughStock(string productCode, int Quantitiy)
        {
            _productList.Add(new Product { Code = "P1", Price = 100, RawPrice = 100, Stock = 90 });
            _productList.Add(new Product { Code = "P2", Price = 100, RawPrice = 100, Stock = 0 });

            string command = string.Format("create_order {0} {1}", productCode, Quantitiy);
            string result = string.Format("No stock on this product {0}", productCode);

            Assert.AreEqual(_operation.ExecuteCommand(command, new TimeHandler(), _productList, _campaignList, _orderList), result);
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
