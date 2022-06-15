﻿using System;
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
        private int count;
        private bool isCompleted;

        public event GoalDelegate onUpdateEvent;
        public event GoalDelegate OnCompleteEvent;

        public Goal(IGoalTarget _target,int _count)
        {
            target = _target;
            count = _count;
        }

        public void UpdateGoal(IGoalTarget goalTarget)
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
