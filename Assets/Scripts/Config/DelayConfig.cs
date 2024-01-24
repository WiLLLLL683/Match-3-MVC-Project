using System;
using UnityEngine;

namespace Config
{
    [Serializable]
    public class DelayConfig
    {
        [Tooltip("Delay In Seconds"), Min(0.01f)] public float afterBlockDestroy = 0.3f;
        [Tooltip("Delay In Seconds"), Min(0.01f)] public float beforeBlockDestroy = 0.1f;
        [Tooltip("Delay In Seconds"), Min(0.01f)] public float betweenBlockGravitation = 0.01f;
        [Tooltip("Delay In Seconds"), Min(0.01f)] public float beforeMatchCheck = 0.5f;
        [Tooltip("Delay In Seconds"), Min(0.01f)] public float beforeWin = 1f;
        [Tooltip("Delay In Seconds"), Min(0.01f)] public float beforeLose = 1f;
        [Tooltip("Delay In Frames"), Min(0)] public int beforeBoosterHintHide = 3;
    }
}