using System.Collections.Generic;

namespace CampaignModule.Core.Objects
{
    public abstract class BaseCommands
    {
        public string CommandName { get; set; }
        public int ParameterCount { get; set; }

        public abstract string CheckParameters(List<string> args, List<Product> productList, List<Campaign> campaignList, List<Order> orderList);
        public abstract string GetCommandResult(List<string> args, List<Product> productList, List<Campaign> campaignList, List<Order> orderList, TimeHandler timeHandler);
        public virtual Product FindProduct(List<Product> productList, string productCode)
        {
            return productList.Find(x => x.Code == productCode);

        }
    }

}
