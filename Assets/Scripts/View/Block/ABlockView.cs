﻿using System;
using UnityEngine;
using Data;

namespace View
{
    /// <summary>
    /// Визуальный элемент блока, вызывается из IBlockPresenter
    /// </summary>
    public abstract class ABlockView : MonoBehaviour
    {
        //public GameObject gameObject { get; }

        public abstract event Action<Directions> OnMove;
        public abstract event Action OnActivate;

        //инициализация
        public abstract void Init(Sprite iconSprite, ParticleSystem destroyEffectPrefab, Vector2Int modelPosition);
        public abstract void ChangeModelPosition(Vector2Int modelPosition);
        public abstract void ChangeType(Sprite iconSprite, ParticleSystem destroyEffectPrefab);
        //визуал
        public abstract void PlayClickAnimation();
        public abstract void PlayDestroyEffect();
    }
}