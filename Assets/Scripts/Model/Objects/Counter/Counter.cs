using Model.Readonly;
using System;
using UnityEngine;

namespace Model.Objects
{
    /// <summary>
    /// Счетчик целей заданного типа
    /// </summary>
    [Serializable]
    public class Counter : ICounter_Readonly, ICloneable
    {
        public ICounterTarget Target => target;
        [SerializeReference, SubclassSelector] private ICounterTarget target;
        public int Count => count;
        [SerializeField] private int count;
        public bool isCompleted { get; private set; }

        public event GoalDelegate OnUpdateEvent;
        public event GoalDelegate OnCompleteEvent;

        public Counter(ICounterTarget _target, int _count)
        {
            target = _target;
            count = _count;
        }

        /// <summary>
        /// Проверка на совпадение с целью счетчика, уменьшение счета при совпадении
        /// </summary>
        /// <param name="goalTarget"></param>
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

        public object Clone() => this.MemberwiseClone();
    }
}
