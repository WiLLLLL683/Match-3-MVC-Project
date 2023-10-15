using System;
using Model.Objects;

namespace Model.Readonly
{
    public interface ICounter_Readonly
    {
        public int Count { get; }
        public bool IsCompleted { get; }
        public ICounterTarget Target { get; }

        public event Action<ICounterTarget, int> OnUpdateEvent;
        public event Action<ICounterTarget> OnCompleteEvent;
    }
}