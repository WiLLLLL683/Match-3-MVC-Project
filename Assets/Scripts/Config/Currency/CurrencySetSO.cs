﻿using Model.Objects;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Utils;

namespace Config
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "NewCurrencySet", menuName = "Config/Currency/CurrencySet")]
    public class CurrencySetSO : ScriptableObject
    {
        public List<CurrencySO> currencies = new();

#if UNITY_EDITOR
        private readonly AssetFinder assetFinder = new();

        [Button] public void FindAllCurrenciesInProject() => assetFinder.FindAllAssetsOfType(ref currencies, this);
#endif
    }
}