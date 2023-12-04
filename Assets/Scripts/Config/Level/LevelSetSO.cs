using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Utils;

namespace Config
{
    [CreateAssetMenu(fileName = "New LevelSet", menuName = "Config/Level/LevelSet")]
    public class LevelSetSO : ScriptableObject
    {
        public List<LevelSO> levels;
        public LevelSO defaultLevel;

#if UNITY_EDITOR

        private readonly AssetFinder assetFinder = new();

        [Button] public void FindAllLevelsInProject() => assetFinder.FindAllAssetsOfType(ref levels, this);
#endif
    }
}