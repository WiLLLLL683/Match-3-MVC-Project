﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Objects
{
    public abstract class ACellType : ICounterTarget
    {
        public virtual bool canContainBlock { get { return true; } }
        public abstract void DestroyCellMaterial();
    }
}