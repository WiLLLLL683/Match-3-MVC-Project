﻿using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    /// <summary>
    /// Тип блока с возможностью активации
    /// </summary>
    [Serializable]
    public abstract class ABlockType : ScriptableObject, ICounterTarget
    {
        public Sprite Sprite;
        public ParticleSystem DestroyEffect;

        /// <summary>
        /// Возвращает успешен ли был ход
        /// </summary>
        public virtual bool Activate()
        {
            return false;
        }
    }
}
