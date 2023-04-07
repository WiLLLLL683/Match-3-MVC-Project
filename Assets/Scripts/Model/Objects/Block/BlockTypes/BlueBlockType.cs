using System;
using System.Collections;
using UnityEngine;

namespace Model.Objects
{
    /// <summary>
    /// Синий тип блока, без действия по активации
    /// </summary>
    [Serializable]
    public class BlueBlockType : ABlockType
    {
        public override bool Activate()
        {
            return base.Activate();
        }
    }
}