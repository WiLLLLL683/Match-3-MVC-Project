using Config;
using ModestTree;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
    public class UniqueIdChecker
    {
        public void CheckCounterTarget<T>(List<T> targets)
            where T : ACounterTargetSO
        {
            var duplicates = targets.GroupBy(x => new { x.CounterTarget.Id })
                   .Where(x => x.Skip(1).Any());

            foreach (var dupl in duplicates)
            {
                foreach (var item in dupl)
                {
                    Debug.LogError($"Duplicate {dupl.Key} found in {item.name}", item);
                }
            }

            if (duplicates.IsEmpty())
            {
                Debug.Log("All Ids are unique");
            }
        }

        public void CheckBlockTypeWeight(List<BlockTypeSO_Weight> typeWeights)
        {
            var duplicates = typeWeights.GroupBy(x => new { x.blockTypeSO.CounterTarget.Id })
                   .Where(x => x.Skip(1).Any());

            foreach (var dupl in duplicates)
            {
                foreach (var item in dupl)
                {
                    Debug.LogError($"Duplicate {dupl.Key} found in {item.blockTypeSO.name}", item.blockTypeSO);
                }
            }

            if (duplicates.IsEmpty())
            {
                Debug.Log("All Ids are unique");
            }
        }
    }
}