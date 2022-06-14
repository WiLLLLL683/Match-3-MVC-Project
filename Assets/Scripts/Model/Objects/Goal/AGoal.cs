using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Model.Objects
{
    [System.Serializable]
    public abstract class AGoal
    {
        public int Count { get { return count; } }
        [SerializeField]
        protected int count;

        public virtual event GoalDelegate goalIsUpdatedEvent;
        public virtual event GoalDelegate goalIsCompletedEvent;

        public abstract void UpdateGoal(IGoalTarget goalTarget);

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
