
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CampaignModule.Core.Objects
{
    public class CreateProduct : BaseCommands
    {
        public CreateProduct()
        {
            CommandName = "create_product";
            ParameterCount = 3;
        }

        // args represent the list of parameters. 
        // totally 3 parameters are expected;  arg[0] : productCode, arg[1] : price, arg[2]  : stock
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
            else if (args[1] == null || !decimal.TryParse(args[1].ToString(), out decimal prmDecimal))
            {
                result = "Command cannot be executed : Parameters are missing or invalid";
            }
            else if (args[2] == null || !int.TryParse(args[2].ToString(), out int prmInt))
            {
                result = "Command cannot be executed : Parameters are missing or invalid";
            }
            //else if (productList.Find(x => x.Code == args[0].ToString()) != null)
            else if (FindProduct(productList, args[0].ToString()) != null)
            {
                result = string.Format("Product {0} is already exist ", args[0].ToString());
            }

            return result;
        }


        public override string GetCommandResult(List<string> args, List<Product> productList, List<Campaign> campaignList, List<Order> orderList, TimeHandler timeHandler)
        {
            string result = string.Empty;

            Product product = new Product { Code = args[0].ToString(), Price = Convert.ToDecimal(args[1]), RawPrice = Convert.ToDecimal(args[1]), Stock = Convert.ToInt32(args[2]) };

            productList.Add(product);

            result = string.Format("Product created; code {0}, price {1}, stock {2}", product.Code, product.Price, product.Stock); 

            return result;
        }

    }
}
