using NUnit.Framework;
using System.Collections.Generic;
using CampaignModule.Core.Objects;
using CampaignModule.App;

namespace CampaignModule.Test
{
    public class ProductTest
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
        [TestCase("P1", 100, 1000)]
        [TestCase("P2", 250000, 2000)]
        [TestCase("P3", 0, 2000)]
        public void CreateProductTest(string productCode, int price, int stock)
        {
            string command = string.Format("create_product {0} {1} {2}", productCode, price, stock);
            string result = string.Format("Product created; code {0}, price {1}, stock {2}", productCode, price, stock);

            Assert.AreEqual(_operation.ExecuteCommand(command, new TimeHandler(), _productList, _campaignList, _orderList), result);
        }


        [Test]
        public void CreateProductTest_ParametersAreMissing()
        {
            string command = string.Format("create_product {0} {1}", "P1", 10);
            string result = "Command cannot be executed : Parameters are missing or invalid";

            Assert.AreEqual(_operation.ExecuteCommand(command, new TimeHandler(), _productList, _campaignList, _orderList), result);
        }

        [Test]
        public void CreateProductTest_ParametersAreInvalid()
        {
            string command = string.Format("create_product {0} {1} {2}", "P1", 10, "test");
            string result = "Command cannot be executed : Parameters are missing or invalid";

            Assert.AreEqual(_operation.ExecuteCommand(command, new TimeHandler(), _productList, _campaignList, _orderList), result);
        }


        [Test]
        [TestCase("P1", 100, 1000)]
        [TestCase("P1", 250000, 2000)]
        public void CreateProductTest_ProductIsAlreadyExist(string productCode, int price, int stock)
        {
            _productList.Add(new Product { Code = "P1", Price = 100, RawPrice = 100, Stock = 1000 });

            string command = string.Format("create_product {0} {1} {2}", productCode, price, stock);
            string result = string.Format("Product {0} is already exist ", productCode);


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
