using Model.Objects;

namespace Model.Readonly
{
    public interface ICounter_Readonly
    {
        int Count { get; }
        bool isCompleted { get; }
        ICounterTarget Target { get; }

        event GoalDelegate OnCompleteEvent;
        event GoalDelegate OnUpdateEvent;
    }
}