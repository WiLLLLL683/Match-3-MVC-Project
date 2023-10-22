using Model.Objects;
using System;

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

        public bool CheckCompletion(Counter counter)
        {
            if (counter == null)
                return false;

            if (counter.IsCompleted)
                return true;

            if (counter.Count > 0)
                return false;

            counter.Count = 0;
            counter.IsCompleted = true;
            OnCompleteEvent?.Invoke(counter);
            return true;
        }
    }
}