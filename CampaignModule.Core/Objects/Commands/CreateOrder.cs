
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CampaignModule.Core.Objects
{
    public class CreateOrder : BaseCommands
    {
        public CreateOrder()
        {
            CommandName = "create_order";
            ParameterCount = 2;
        }

        // args represent the list of parameters. 
        // totally 3 parameters are expected;  arg[0] : productCode, arg[1] : quantity
        public override string CheckParameters(List<string> args, List<Product> productList, List<Campaign> campaignList, List<Order> orderList)
        {
            string result = string.Empty;

            int prmInt = 0;
            if (args.Count() != ParameterCount)
            {
                result = "Command cannot be executed : Parameters are missing or invalid";
            }

            else if (args[0] == null || string.IsNullOrEmpty(args[0].ToString()))
            {
                result = "Command cannot be executed : Parameters are missing or invalid";
            }
            else if (args[1] == null || !int.TryParse(args[1].ToString(), out prmInt) || prmInt == 0)
            {
                result = "Command cannot be executed : Parameters are missing or invalid";
            }
            else if (FindProduct(productList, args[0].ToString()) == null)
            {
                result = string.Format("Product {0} not found", args[0].ToString());
            }
            else if (productList.Find(x => x.Code == args[0].ToString() && x.Stock >= Convert.ToInt32(args[1])) == null)
            {
                result = string.Format("No stock on this product {0}", args[0].ToString());
            }

            return result;
        }


        public override string GetCommandResult(List<string> args, List<Product> productList, List<Campaign> campaignList, List<Order> orderList, TimeHandler timeHandler)
        {
            string result = string.Empty;
            int mQuantity = Convert.ToInt32(args[1]);

            Product prd = FindProduct(productList, args[0].ToString());
            prd.Stock -= mQuantity;

            Order order = new Order { ProductCode = args[0].ToString(), Quantitiy = mQuantity, TotalCost = prd.Price * mQuantity };

            orderList.Add(order);
                       
            result = string.Format("Order created; product {0}, quantity {1}", order.ProductCode, order.Quantitiy);

            return result;
        }

    }

}
