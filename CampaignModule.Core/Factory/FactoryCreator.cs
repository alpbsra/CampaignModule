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
                    command = new CreateProduct();
                    break;
                case "create_campaign":
                    command = new CreateCampaign();
                    break;
                case "create_order":
                    command = new CreateOrder();
                    break;
                case "get_product_info":
                    command = new GetProductInfo();
                    break;
                case "get_campaign_info":
                    command = new GetCampaignInfo();
                    break;
                default:
                    break;
            }
            return command;
        }

    }
}
