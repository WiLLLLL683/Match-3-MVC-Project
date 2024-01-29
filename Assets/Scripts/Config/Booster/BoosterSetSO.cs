using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Config
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "NewBoosterSet", menuName = "Config/Boosters/BoosterSet")]
    public class BoosterSetSO : ScriptableObject
    {
        public List<BoosterSO> boosters = new();
        public BoosterSO defaultBooster;

#if UNITY_EDITOR
        private readonly AssetFinder assetFinder = new();
        private readonly UniqueIdChecker idChecker = new();

        [Button] public void CheckUniqueId() => idChecker.CheckBoosters(boosters);
        [Button] public void FindAllBoostersInProject() => assetFinder.FindAllAssetsOfType(ref boosters, this);
#endif
    }
}