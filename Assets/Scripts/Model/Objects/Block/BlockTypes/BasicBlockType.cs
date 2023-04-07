using System;
using System.Collections;
using UnityEngine;

namespace Model.Objects
{
    /// <summary>
    /// Базовый тип блока, без действия по активации
    /// </summary>
    [Serializable]
    public class BasicBlockType : ABlockType
    {
        public override bool Activate()
        {
            return base.Activate();
        }
    }
}