using System;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "New LevelSet", menuName = "Config/Level/LevelSet")]
    public class LevelSetSO : ScriptableObject, ILevelConfigProvider
    {
        [SerializeField] private LevelSO[] levels;
        [SerializeField] private LevelSO defaultLevel;

        public int LastLevelIndex => levels.Length - 1;

        public LevelSO GetSO(int index)
        {
            if (!levels.IsInBounds(index))
                return defaultLevel;

            return levels[index];
        }
    }
}