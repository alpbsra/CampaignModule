using System;
using System.Collections.Generic;
using System.Text;

namespace CampaignModule.Core.Interfaces
{
    public interface IObservable
    {
        void AddObserver(IObserver observer);

        void RemoveObserver(IObserver observer);

        void NotifyObserver(bool isCampaignActive);

    }
}
