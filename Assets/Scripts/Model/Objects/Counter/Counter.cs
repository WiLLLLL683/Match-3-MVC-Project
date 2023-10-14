using Model.Readonly;
using System;
using UnityEngine;

namespace Model.Objects
{
    /// <summary>
    /// Счетчик целей заданного типа
    /// </summary>
    [Serializable]
    public class Counter : ICounter_Readonly
    {
        public ICounterTarget Target { get; private set; }
        public int Count { get; private set; }
        public bool IsCompleted { get; private set; }

        public event Action<ICounterTarget, int> OnUpdateEvent;
        public event Action<ICounterTarget> OnCompleteEvent;

        public Counter(ICounterTarget Target, int Count)
        {
            this.Target = Target;
            this.Count = Count;
        }

        /// <summary>
        /// Проверка на совпадение с целью счетчика, уменьшение счета при совпадении
        /// </summary>
        public void CheckTarget(ICounterTarget target)
        {
            if (!IsCompleted &&
                target.GetType() == Target.GetType() &&
                target.Id == Target.Id)
            {
                Count--;
                CheckCompletion();
                OnUpdateEvent?.Invoke(Target, Count);
            }
        }

        private void CheckCompletion()
        {
            if (Count <= 0)
            {
                OnCompleteEvent?.Invoke(Target);
                Count = 0;
                IsCompleted = true;
            }
        }
    }
}
