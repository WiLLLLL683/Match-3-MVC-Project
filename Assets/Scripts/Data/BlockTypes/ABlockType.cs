using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    /// <summary>
    /// Тип блока с возможностью активации
    /// </summary>
    [Serializable]
    public abstract class ABlockType : ICounterTarget
    {
        public int id;
        public Sprite Sprite;
        public ParticleSystem DestroyEffect;
        public Sprite Icon => Sprite;

        protected ABlockType(int id)
        {
            this.id = id;
        }

        /// <summary>
        /// Возвращает успешен ли был ход
        /// </summary>
        public abstract bool Activate();
    }
}
