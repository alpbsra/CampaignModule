using System;
using System.Collections.Generic;
using System.Text;

namespace CampaignModule.Core.Interfaces
{
    public interface ITimeObserver
    {
        void IncreaseTime(int hour);
    }
}
