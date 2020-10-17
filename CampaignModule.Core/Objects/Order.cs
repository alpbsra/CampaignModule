using System;
using System.Collections.Generic;
using System.Text;

namespace CampaignModule.Core.Objects
{
    public class Order
    {
        public string ProductCode { get; set; }
        public int Quantitiy { get; set; }
        public decimal TotalCost { get; set; }

    }
}
