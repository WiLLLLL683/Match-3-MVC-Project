using Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViewElements;

namespace Controller
{
    public class CounterSpawner : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        [SerializeField] private Counter counterPrefab;

        private List<Counter> allCounters = new();

        public Counter SpawnCounter(Model.Objects.Counter _counterModel)
        {
            Counter Counter = Instantiate(counterPrefab, parent);
            Counter.Init(_counterModel.Target.Icon, _counterModel.Count);
            allCounters.Add(Counter);
            return Counter;
        }

    }
}