using System;
using System.Collections.Generic;
using System.Text;

namespace CampaignModule.Core.Interfaces
{
    public interface ITimeObservable
    {
        void AddObserver(ITimeObserver observer);

        void RemoveObserver(ITimeObserver observer);

        void NotifyObserver(int increaseAmount);

    }
}
