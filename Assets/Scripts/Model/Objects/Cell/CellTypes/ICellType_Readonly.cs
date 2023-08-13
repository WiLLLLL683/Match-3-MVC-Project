using System;
using UnityEngine;

namespace Model.Readonly
{
    public interface ICellType_Readonly
    {
        public Sprite Icon { get; }
        public ParticleSystem DestroyEffect { get; }
        public ParticleSystem EmptyEffect { get; }
        public bool CanContainBlock { get; }
        public bool IsPlayable { get; }
    }
}