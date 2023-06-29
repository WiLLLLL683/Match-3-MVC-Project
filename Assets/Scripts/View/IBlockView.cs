using Controller;
using Data;
using System;
using UnityEngine;

namespace ViewElements
{
    public interface IBlockView
    {
        public void Init(ABlockType type, Vector2 modelPosition, IBlockController controller);
        public void PlayClickAnimation();
        public void DragPosition(Vector2 deltaPosition);
        public void ReturnToModelPosition();
        public void ChangeType(ABlockType blockType);
        public void SetModelPosition(Vector2 modelPosition);
        public void PlayDestroyEffect();

        public GameObject gameObject { get; }
        public IBlockController Controller { get; }
    }
}