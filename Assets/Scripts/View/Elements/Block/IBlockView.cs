using Config;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
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

        public void Init(Sprite iconSprite, BlockTypeConfig config, Vector2Int modelPosition);
        UniTask FlyTo(Vector2 worldPosition, float duration, CancellationToken token = default);
        void SetModelPosition(Vector2Int modelPosition);
        void SetType(Sprite iconSprite, BlockTypeConfig config);
        void PlayClickAnimation();
        void Destroy();
        void Release();
        void Drag(Vector2 deltaPosition);
    }
}