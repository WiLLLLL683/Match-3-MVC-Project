﻿using Model.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data
{
    [Serializable]
    public abstract class ACellType : ScriptableObject, ICounterTarget
    {
        public Sprite Sprite;
        public ParticleSystem DestroyEffect;
        public ParticleSystem EmptyEffect;

        public virtual bool CanContainBlock => true;
        public abstract void DestroyCellMaterial();
    }
}
