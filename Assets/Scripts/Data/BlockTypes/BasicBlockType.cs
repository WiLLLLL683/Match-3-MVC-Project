using System;
using System.Collections;
using UnityEngine;

namespace Data
{
    /// <summary>
    /// Базовый тип блока, без действия по активации
    /// </summary>
    [Serializable]
    public class BasicBlockType : IBlockType
    {
        public int Id => id;
        [SerializeField] private int id;

        public Sprite Icon => icon;
        [SerializeField] private Sprite icon;

        public ParticleSystem DestroyEffect => destroyEffect;
        [SerializeField] private ParticleSystem destroyEffect;

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