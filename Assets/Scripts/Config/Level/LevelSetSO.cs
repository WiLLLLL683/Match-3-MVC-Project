using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Utils;

namespace Config
{
    [CreateAssetMenu(fileName = "New LevelSet", menuName = "Config/Level/LevelSet")]
    public class LevelSetSO : ScriptableObject, ILevelConfigProvider
    {
        [SerializeField] private List<LevelSO> levels;
        [SerializeField] private LevelSO defaultLevel;

        public int LastLevelIndex => levels.Count - 1;

        public LevelSO GetSO(int index)
        {
            if (!levels.IsInBounds(index))
                return defaultLevel;

            return levels[index];
        }

#if UNITY_EDITOR

        private readonly AssetFinder assetFinder = new();

        [Button] public void FindAllLevelsInProject() => assetFinder.FindAllAssetsOfType(ref levels, this);
#endif
    }
}