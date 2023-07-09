using Presenter;
using Data;
using System;
using UnityEngine;

namespace View
{
    public interface IBlockView
    {
        public void Init(ABlockType type, Vector2 modelPosition, IBlockPresenter controller);
        public void SetModelPosition(Vector2 modelPosition);
        public void PlayClickAnimation();
        public void Drag(Vector2 deltaPosition);
        public void ReturnToModelPosition();
        public void ChangeType(ABlockType blockType);
        public void PlayDestroyEffect();

        public GameObject gameObject { get; }
        public IBlockPresenter Controller { get; }
    }
}