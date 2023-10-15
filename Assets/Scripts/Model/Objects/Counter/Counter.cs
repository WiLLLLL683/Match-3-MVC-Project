using System;

namespace Model.Objects
{
    /// <summary>
    /// Счетчик целей заданного типа
    /// </summary>
    [Serializable]
    public class Counter
    {
        public ICounterTarget Target;
        public int Count;
        public bool IsCompleted;

        public Counter(ICounterTarget Target, int Count)
        {
            this.Target = Target;
            this.Count = Count;
        }
    }
}
