using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Config
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New CounterTargetSet", menuName = "Config/CounterTargetSet")]
    public class CounterTargetSetSO : ScriptableObject
    {
        public List<ACounterTargetSO> targets = new();
        public ACounterTargetSO defaultTarget;
        public TurnSO turnSO;

#if UNITY_EDITOR
        private readonly AssetFinder assetFinder = new();
        private readonly UniqueIdChecker idChecker = new();

        [Button] public void CheckUniqueId() => idChecker.CheckCounterTarget(targets);
        [Button] public void FindAllCounterTargetsInProject() => assetFinder.FindAllAssetsOfType(ref targets, this);
#endif
    }
}