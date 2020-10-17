using CampaignModule.Core.Interfaces;

namespace CampaignModule.Core.Objects
{
    public class Product : IObserver
    {
        public string Code { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public decimal RawPrice { get; set; }

        public void Update(bool isActive, int percentage, int duration, int time, int direction)
        {
            if (isActive)
                this.Price = decimal.Round(this.RawPrice + direction * time * ((this.RawPrice * percentage / 100) / duration), 2);
            else
                this.Price = this.RawPrice;
        }
    }

}
