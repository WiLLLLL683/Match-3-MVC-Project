using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Model.Objects
{
    [System.Serializable]
    public class Goal
    {
        public IGoalTarget Target { get { return target; } }
        [SerializeField]
        protected IGoalTarget target;
        public int Count { get { return count; } }
        [SerializeField]
        protected int count;

        public event GoalDelegate goalIsUpdatedEvent;
        public event GoalDelegate goalIsCompletedEvent;

        public Goal(IGoalTarget _target,int _count)
        {
            target = _target;
            count = _count;
        }

        public void UpdateGoal(IGoalTarget goalTarget)
        {
            if (goalTarget.GetType() == target.GetType())
            {
                count -= 1;
                CheckCount();
                goalIsUpdatedEvent?.Invoke(this, new EventArgs());
            }
        }

        protected bool CheckCount()
        {
            if (count <= 0)
            {
                goalIsCompletedEvent?.Invoke(this, new EventArgs());
                return true;
            }
            return false;
        }
    }
}
