using System;
using System.Collections;
using UnityEngine;

namespace Data
{
    /// <summary>
    /// Базовый тип блока, без действия по активации
    /// </summary>
    public class BasicBlockType : ABlockType
    {
        public BasicBlockType(int id = 0) : base(id)
        {

        }

        public override bool Activate() => false;
    }
}