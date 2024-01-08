using System;
using UnityEngine;
using Utils;

namespace View
{
    /// <summary>
    /// Визуальный элемент блока, вызывается из IBlockPresenter
    /// </summary>
    public interface IBlockView
    {
        GameObject gameObject { get; }
        Vector2Int ModelPosition { get; }

        void Init(Sprite iconSprite, ParticleSystem destroyEffectPrefab, Vector2Int modelPosition);
        void SetModelPosition(Vector2Int modelPosition);
        void SetType(Sprite iconSprite, ParticleSystem destroyEffectPrefab);
        void PlayClickAnimation();
        void PlayDestroyEffect();
        void Release();
        void Drag(Vector2 deltaPosition);
    }
}