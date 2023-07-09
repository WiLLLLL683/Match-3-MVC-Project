using Data;
using System;
using UnityEngine;

namespace View
{
    public interface ICellView
    {
        void Init(Vector2 modelPosition, ACellType type);
        void ChangeType(ACellType type);
        void PlayDestroyEffect();
        void PlayEmptyEffect();
    }
}