using System;
using System.Collections.Generic;
using System.Linq;

namespace CampaignModule.Core.Objects
{
    public class Command_CreateCampaign : BaseCommands
    {
        public Command_CreateCampaign()
        {
            CommandName = "create_campaign";
            ParameterCount = 5;
        }

        // args represent the list of parameters. 
        // totally 5 parameters are expected;  arg[0] : name, arg[1] : productCode, arg[2]  : duration, arg[3] : pmlimit, arg[4] : targetSalesCount

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
            else if (args[1] == null || string.IsNullOrEmpty(args[1].ToString()))
            {
                result = "Command cannot be executed : Parameters are missing or invalid";
            }
            else if (args[2] == null || !int.TryParse(args[2].ToString(), out int prmInt))
            {
                result = "Command cannot be executed : Parameters are missing or invalid";
            }
            else if (args[3] == null || !int.TryParse(args[3].ToString(), out prmInt))
            {
                result = "Command cannot be executed : Parameters are missing or invalid";
            }
            else if (args[4] == null || !int.TryParse(args[4].ToString(), out prmInt))
            {
                result = "Command cannot be executed : Parameters are missing or invalid";
            }
            else if (campaignList.Find(x => x.Name == args[0].ToString()) != null)
            {
                result = string.Format("Campaign {0} is already exist", args[0].ToString());
            }
            else if (FindProduct(productList, args[1].ToString()) == null)
            {
                result = string.Format("Product {0} not found", args[1].ToString());
            }

            return result;
        }


        public override string GetCommandResult(List<string> args, List<Product> productList, List<Campaign> campaignList, List<Order> orderList, TimeHandler timeHandler)
        {
            string result = string.Empty;

            Campaign campaign = new Campaign
            {
                Name = args[0].ToString(),
                ProductCode = args[1].ToString(),
                Duration = Convert.ToInt32(args[2]),
                PMLimit = Convert.ToInt32(args[3]),
                TargetSalesCount = Convert.ToInt32(args[4]),
                IsActive = true,
                IncreaseOrDecrease = Enum.DirectionOfChange.DECREASE
            };

            campaignList.Add(campaign);

            campaign.AddObserver(FindProduct(productList, args[1].ToString()));

            timeHandler.AddObserver(campaign);

            result = string.Format("Campaign created; name {0}, product {1}, duration {2},limit {3}, target sales count {4}",
                campaign.Name, campaign.ProductCode, campaign.Duration, campaign.PMLimit, campaign.TargetSalesCount);

            return result;
        }

    }


}
