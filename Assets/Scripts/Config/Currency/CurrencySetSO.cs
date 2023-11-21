using Model.Objects;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Utils;

namespace Config
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "NewCurrencySet", menuName = "Config/Currency/CurrencySet")]
    public class CurrencySetSO : ScriptableObject, ICurrencyConfigProvider
    {
        public List<CurrencySO> currencies = new();

        public CurrencySO GetSO(CurrencyType type)
        {
            for (int i = 0; i < currencies.Count; i++)
            {
                if (currencies[i].type == type)
                    return currencies[i];
            }

            return null;
        }

        public List<CurrencySO> GetAllSO() => currencies;

#if UNITY_EDITOR

        private readonly AssetFinder assetFinder = new();

        [Button] public void FindAllCurrenciesInProject() => assetFinder.FindAllAssetsOfType(ref currencies, this);
#endif
    }
}