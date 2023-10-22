using System;
using UnityEngine;

namespace Model.Objects
{
    /// <summary>
    /// Базовый тип блока, без действия по активации
    /// </summary>
    [Serializable]
    public class BasicBlockType : BlockType
    {
        public BasicBlockType() { }
        public BasicBlockType(int id) => this.Id = id;

        public override bool Activate() => false;
    }
}