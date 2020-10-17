using CampaignModule.Core.Interfaces;
using System.Collections.Generic;

namespace CampaignModule.Core.Objects
{
    public class TimeHandler : ITimeObservable
    {
        private List<ITimeObserver> TimeObservers;
        public int Time;

        public void IncreaseTime(int hour)
        {
            Time += hour;
            NotifyObserver(hour);
        }


        public TimeHandler()
        {
            TimeObservers = new List<ITimeObserver>();

        }

        public void AddObserver(ITimeObserver o)
        {
            TimeObservers.Add(o);
        }

        public void RemoveObserver(ITimeObserver o)
        {
            TimeObservers.Remove(o);
        }

        public void NotifyObserver(int increaseAmount)
        {
            // campaigns are triggered
            foreach (ITimeObserver o in TimeObservers)
            {
                o.IncreaseTime(increaseAmount);
            }
        }

    }

}
