using System;
using System.Collections.Generic;
using System.Text;

namespace CampaignModule.Core.Interfaces
{
    public interface IObserver
    {
        void Update(bool isActive, int discountPercentage, int duration, int time, int direction);
    }
}
