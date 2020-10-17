using CampaignModule.Core.Enum;
using CampaignModule.Core.Interfaces;
using System.Collections.Generic;

namespace CampaignModule.Core.Objects
{
    public class Campaign : IObservable, ITimeObserver
    {
        private List<IObserver> observers;

        public string Name { get; set; }
        public string ProductCode { get; set; }
        public int Duration { get; set; } // hour
        public int PMLimit { get; set; } // percentage
        public int TargetSalesCount { get; set; }

        public bool IsActive { get; set; }
        public DirectionOfChange IncreaseOrDecrease { get; set; }

        private int Time;
        public int TimeFlag
        {
            get { return Time; }
            set
            {
                Time += value;
                if (IsActive && Time < Duration)
                {
                    NotifyObserver(true);
                }
                else
                {
                    IsActive = false;
                    NotifyObserver(false);
                }

            }
        }

        public Campaign()
        {
            observers = new List<IObserver>();
            IsActive = true;
            IncreaseOrDecrease = DirectionOfChange.DECREASE;

        }

        public void AddObserver(IObserver o)
        {
            observers.Add(o);
        }

        public void RemoveObserver(IObserver o)
        {
            observers.Remove(o);
        }

        public void NotifyObserver(bool isCampaignActive)
        {
            // products are triggered and price of products are increased/decreased
            foreach (IObserver o in observers)
            {
                o.Update(isCampaignActive, PMLimit, Duration, TimeFlag, (int)IncreaseOrDecrease);
            }
        }

        public void IncreaseTime(int hour)
        {
            TimeFlag = hour;
        }
    }


}
