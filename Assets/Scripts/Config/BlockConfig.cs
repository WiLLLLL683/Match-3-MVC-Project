using System;
using UnityEngine;

namespace Config
{
    [Serializable]
    public class BlockConfig //TODO ��������� � ��� �����?
    {
        [Tooltip("Delay In Seconds"), Min(0.01f)] public float blockFlyDuration = 1f;
        public int bonusBlock_explosionRadius = 2;
    }
}