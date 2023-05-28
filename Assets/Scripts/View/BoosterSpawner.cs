using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class BoosterSpawner : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        [SerializeField] private Booster boosterPrefab;

        private List<Booster> allBoosters = new();

        public Booster SpawnCounter(Model.Objects.IBooster _boosterModel)
        {
            Booster booster = Instantiate(boosterPrefab, parent);
            booster.Init(_boosterModel.Icon, _boosterModel.Ammount);
            allBoosters.Add(booster);
            return booster;
        }

    }
}