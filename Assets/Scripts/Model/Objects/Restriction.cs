using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Model.Objects
{
    public class Restriction
    {
        public int Count { get { return count; } }
        [SerializeField]
        protected int count;
        private bool isCompleted;

        public event RestrictionDelegate onUpdateEvent;
        public event RestrictionDelegate OnCompleteEvent;

        public Restriction(int _count)
        {
            count = _count;
        }

        public void SubtractCount()
        {
            if (!isCompleted)
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
