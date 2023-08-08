using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    /// <summary>
    /// Тип блока с возможностью активации
    /// </summary>
    public interface IBlockType : ICounterTarget
    {
        int Id { get; }
        Sprite Icon { get; }
        ParticleSystem DestroyEffect { get; }

        /// <summary>
        /// Возвращает успешен ли был ход
        /// </summary>
        public bool Activate();
    }
}
