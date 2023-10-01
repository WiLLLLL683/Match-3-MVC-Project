using UnityEngine;

namespace Model.Objects
{
    public interface ICounterTarget
    {
        public int Id { get; }
        public Sprite Icon { get; }
    }
}
