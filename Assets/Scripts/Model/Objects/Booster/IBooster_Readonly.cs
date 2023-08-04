using UnityEngine;

namespace Model.Readonly
{
    public interface IBooster_Readonly
    {
        public Sprite Icon { get; }
        public int Amount { get; }
    }
}