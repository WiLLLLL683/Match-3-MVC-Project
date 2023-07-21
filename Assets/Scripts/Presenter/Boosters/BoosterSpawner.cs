using System.Collections.Generic;
using UnityEngine;
using View;

namespace Presenter
{
    public class BoosterSpawner : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        [SerializeField] private BoosterView boosterPrefab;

        private List<BoosterView> allBoosters = new();

        public BoosterView SpawnCounter(Model.Objects.IBooster _boosterModel)
        {
            BoosterView booster = Instantiate(boosterPrefab, parent);
            booster.Init(_boosterModel.Icon, _boosterModel.Amount);
            allBoosters.Add(booster);
            return booster;
        }

    }
}