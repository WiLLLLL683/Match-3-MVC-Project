using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Model.Objects
{
    public class Counter
    {
        public ICounterTarget Target { get; private set; }
        public int Count { get { return count; } }
        private int count;
        public bool isCompleted { get; private set; }

        public event GoalDelegate OnUpdateEvent;
        public event GoalDelegate OnCompleteEvent;

        public Counter(ICounterTarget _target,int _count)
        {
            Target = _target;
            count = _count;
        }

        public Counter(CounterData data)
        {
            Target = data.target;
            count = data.count;
        }

        public void UpdateGoal(ICounterTarget goalTarget)
        {
            if (goalTarget.GetType() == Target.GetType() && !isCompleted)
            {
                count -= 1;
                CheckCompletion();
                OnUpdateEvent?.Invoke(this, new EventArgs());
            }
        }

        private void CheckCompletion()
        {
            if (count <= 0)
            {
                OnCompleteEvent?.Invoke(this, new EventArgs());
                count = 0;
                isCompleted = true;
            }
        }
    }
}
