using UnityEngine;
using Model.Objects;

namespace Config
{
    [System.Serializable]
    public abstract class CounterTargetSO : ScriptableObject
    {
        public Sprite icon;
        public abstract ICounterTarget CounterTarget { get; }
    }
}