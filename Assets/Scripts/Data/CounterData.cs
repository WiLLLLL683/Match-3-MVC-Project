using Model.Objects;
using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class CounterData
    {
        [SerializeReference, SubclassSelector]
        public ICounterTarget target;
        public int count;

        public CounterData(ICounterTarget _target, int _count)
        {
            target = _target;
            count = _count;
        }

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
