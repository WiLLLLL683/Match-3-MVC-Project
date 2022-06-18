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

        internal bool ValidCheck()
        {
            if (target == null && count < 0)
            {
                Debug.LogError("Something wrong with CounterData");
                return false;
            }

            return true;
        }
    }
}
