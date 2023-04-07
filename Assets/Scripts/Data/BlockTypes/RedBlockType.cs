using System;
using System.Collections;
using UnityEngine;

namespace Data
{
    /// <summary>
    /// Красный тип блока, без действия по активации
    /// </summary>
    [Serializable]
    public class RedBlockType : ABlockType
    {
        public override bool Activate()
        {
            return base.Activate();
        }
    }
}