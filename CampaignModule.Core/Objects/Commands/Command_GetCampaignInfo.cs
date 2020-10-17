
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CampaignModule.Core.Objects
{
    public class Command_GetCampaignInfo : BaseCommands
    {
        public Command_GetCampaignInfo()
        {
            CommandName = "get_campaign_info";
            ParameterCount = 1;
        }

        // args represent the list of parameters. 
        // 1 parameter is expected;  arg[0] : campaignName
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
            else if (campaignList.Find(x => x.Name == args[0].ToString()) == null)
            {
                result = string.Format("Campaign {0} not found ", args[0].ToString());
            }

            return result;
        }


        public override string GetCommandResult(List<string> args, List<Product> productList, List<Campaign> campaignList, List<Order> orderList, TimeHandler timeHandler)
        {
            string result = string.Empty;

            Campaign campaign = campaignList.Find(x => x.Name == args[0].ToString());

            if (campaign == null)
            {
                result = string.Format("Campaign {0} info; campaign not found ", args[0].ToString());
            }
            else
            {
                int totalSales = orderList.Where(x => x.ProductCode == campaign.ProductCode).Sum(x => x.Quantitiy);
                decimal totalCost = orderList.Where(x => x.ProductCode == campaign.ProductCode).Sum(x => x.TotalCost);
                decimal averageItemPrice = totalCost > 0 ? decimal.Round(totalCost / totalSales, 2) : 0;
                decimal turnover = totalSales * averageItemPrice;

                result = string.Format("Campaign {0} info; Status {1}, Target Sales {2},Total Sales {3}, Turnover {4}, Average Item Price {5}",
                   campaign.Name, campaign.IsActive ? "Active" : "Passive", campaign.TargetSalesCount, totalSales, turnover, averageItemPrice);

            }


            return result;
        }

    }

}
