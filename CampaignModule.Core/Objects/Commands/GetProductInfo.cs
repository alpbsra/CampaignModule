using System.Collections.Generic;
using System.Linq;

namespace CampaignModule.Core.Objects
{
    public class GetProductInfo : BaseCommands
    {
        public GetProductInfo()
        {
            CommandName = "get_product_info";
            ParameterCount = 1;
        }

        // args represent the list of parameters. 
        // 1 parameter is expected;  arg[0] : productCode
        public override string CheckParameters(List<string> args, List<Product> productList, List<Campaign> campaignList, List<Order> orderList)
        {
            string result = string.Empty;

            if (args.Count() != ParameterCount)
            {
                result = "Command cannot be executed : Parameters are missing or invalid";
            }
            else if (args[0] == null || string.IsNullOrEmpty(args[0].ToString()))
            {
                result = "Command cannot be executed : Parameters are missing or invalid";
            }

            return result;
        }

        public override string GetCommandResult(List<string> args, List<Product> productList, List<Campaign> campaignList, List<Order> orderList, TimeHandler timeHandler)
        {
            string result = string.Empty;

            Product product = FindProduct(productList, args[0].ToString());

            if (product == null)
                result = string.Format("Product {0} info; product not found ", args[0].ToString());
            else
                result = string.Format("Product {0} info; price {1}, stock {2}", product.Code, product.Price, product.Stock);

            return result;
        }
    }

}
