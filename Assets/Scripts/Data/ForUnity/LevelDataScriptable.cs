using System;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName ="NewLevelData",menuName ="Data/LevelData")]
    public class LevelDataScriptable : ScriptableObject
    {
        public LevelData level;
    }
}
