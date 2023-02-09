﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Objects
{
    /// <summary>
    /// Тип клетки которая не может содержать блок
    /// </summary>
    [Serializable]
    public class NotPlayableCellType : ACellType
    {
        public override bool CanContainBlock { get { return false; } }
        public override void DestroyCellMaterial()
        {
            
        }
    }
}
