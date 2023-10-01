using System;
using UnityEngine;

namespace Model.Objects
{
    /// <summary>
    /// Базовый тип блока, без действия по активации
    /// </summary>
    [Serializable]
    public class BasicBlockType : IBlockType, ICounterTarget
    {
        [SerializeField] private int id;
        [SerializeField] private Sprite icon;
        [SerializeField] private ParticleSystem destroyEffect;

        public int Id => id;
        public Sprite Icon => icon;
        public ParticleSystem DestroyEffect => destroyEffect;

        public BasicBlockType()
        {

        }
        public BasicBlockType(int id = 0)
        {
            this.id = id;
        }

        public bool Activate() => false;
    }
}