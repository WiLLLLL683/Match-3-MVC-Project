using Model.Objects;
using System;

namespace Model.Readonly
{
    public interface ICounter_Readonly
    {
        int Count { get; }
        bool IsCompleted { get; }
        ICounterTarget Target { get; }

        event Action<ICounterTarget, int> OnUpdateEvent;
        event Action<ICounterTarget> OnCompleteEvent;
    }
}