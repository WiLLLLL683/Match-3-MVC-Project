using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Config
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New CounterTargetSet", menuName = "Config/CounterTargetSet")]
    public class CounterTargetSetSO : ScriptableObject, ICounterTargetConfigProvider
    {
        public TurnSO turnSO;
        [SerializeField] private List<ACounterTargetSO> targets = new();
        [SerializeField] private ACounterTargetSO defaultTarget;

        public ACounterTargetSO GetSO(int id)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                if (id == targets[i].CounterTarget.Id)
                {
                    return targets[i];
                }
            }

            return defaultTarget;
        }

#if UNITY_EDITOR
        private readonly AssetFinder assetFinder = new();
        private readonly UniqueIdChecker idChecker = new();

        [Button] public void CheckUniqueId() => idChecker.CheckCounterTarget(targets);
        [Button] public void FindAllCounterTargetsInProject() => assetFinder.FindAllAssetsOfType(ref targets, this);
#endif
    }
}