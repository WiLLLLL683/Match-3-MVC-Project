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
        public ICounterTarget Target { get { return target; } }
        private ICounterTarget target;
        public int Count { get { return count; } }
        private int count;
        private bool isCompleted;

        public event GoalDelegate onUpdateEvent;
        public event GoalDelegate OnCompleteEvent;

        public Counter(ICounterTarget _target,int _count)
        {
            target = _target;
            count = _count;
        }

        public void UpdateGoal(ICounterTarget goalTarget)
        {
            if (goalTarget.GetType() == target.GetType() && !isCompleted)
            {
                count -= 1;
                CheckCompletion();
                onUpdateEvent?.Invoke(this, new EventArgs());
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
