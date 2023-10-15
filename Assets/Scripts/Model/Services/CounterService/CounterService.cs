using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Services
{
    public class CounterService : ICounterService
    {
        public event Action<Counter> OnUpdateEvent;
        public event Action<Counter> OnCompleteEvent;

        public void CheckTarget(Counter counter, ICounterTarget target)
        {
            if (!counter.IsCompleted &&
                target.GetType() == counter.Target.GetType() &&
                target.Id == counter.Target.Id)
            {
                counter.Count--;
                CheckCompletion(counter);
                OnUpdateEvent?.Invoke(counter);
            }
        }

        private void CheckCompletion(Counter counter)
        {
            if (counter.Count <= 0)
            {
                OnCompleteEvent?.Invoke(counter);
                counter.Count = 0;
                counter.IsCompleted = true;
            }
        }
    }
}