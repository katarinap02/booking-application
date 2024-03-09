using System;
using System.Collections.Generic;

namespace BookingApp.Observer
{
    public class Subject
    {
        private List<IObserver> observers;

        public Subject()
        {
            observers = new List<IObserver>();
        }

        public void Subscribe(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update();
            }
        }
    }
}
