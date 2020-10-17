using CampaignModule.Core.Objects;

namespace CampaignModule.Core.Factory
{
    public class FactoryCreator
    {
        public BaseCommands CommandFactory(string commandType)
        {
            BaseCommands command = null;
            switch (commandType)
            {
                case "create_product":
                    command = new Command_CreateProduct();
                    break;
                case "create_campaign":
                    command = new Command_CreateCampaign();
                    break;
                case "create_order":
                    command = new Command_CreateOrder();
                    break;
                case "get_product_info":
                    command = new Command_GetProductInfo();
                    break;
                case "get_campaign_info":
                    command = new Command_GetCampaignInfo();
                    break;
                default:
                    break;
            }
            return command;
        }

    }

}
