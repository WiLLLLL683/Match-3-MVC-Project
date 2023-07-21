using Model.Readonly;
using System.Collections.Generic;
using UnityEngine;
using View;

namespace Presenter
{
    public class CounterSpawner : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        [SerializeField] private CounterView counterPrefab;

        private List<CounterView> allCounters = new();

        public CounterView SpawnCounter(ICounter_Readonly _counterModel)
        {
            CounterView Counter = Instantiate(counterPrefab, parent);
            Counter.Init(_counterModel.Target.Icon, _counterModel.Count);
            allCounters.Add(Counter);
            return Counter;
        }
    }
}