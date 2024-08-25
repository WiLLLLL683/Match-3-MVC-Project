using DG.Tweening;
using NaughtyAttributes;
using System;
using UnityEngine;

namespace Config
{
    [Serializable]
    public class BlockTypeConfig
    {
        public ParticleSystem destroyEffect;
        public float smoothTime = 0.1f;
        [SortingLayer] public string flyingLayer;
        public float flyingScale = 1.1f;
        public Ease flyingEase;
    }
}
