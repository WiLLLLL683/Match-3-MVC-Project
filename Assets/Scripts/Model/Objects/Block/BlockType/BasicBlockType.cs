using System;
using UnityEngine;

namespace Model.Objects
{
    /// <summary>
    /// Базовый тип блока, без действия по активации
    /// </summary>
    [Serializable]
    public class BasicBlockType : IBlockType, ICounterTarget
    {
        [SerializeField] private int id;
        public int Id => id;

        public BasicBlockType() { }

        public BasicBlockType(int id)
        {
            this.id = id;
        }

        public bool Activate() => false;
        public IBlockType Clone() => (IBlockType)MemberwiseClone();
    }
}