using Model.Objects;
using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public struct CounterData
    {
        [SerializeReference]
        public ICounterTarget target;
        public int count;
    }
}
