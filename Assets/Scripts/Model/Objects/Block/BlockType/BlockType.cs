using System;
using UnityEngine;
using Model.Readonly;

namespace Model.Objects
{
    /// <summary>
    /// Тип блока с возможностью активации
    /// </summary>
    [Serializable]
    public abstract class BlockType : IBlockType_Readonly, ICounterTarget
    {
        [SerializeField] protected int id;
        public int Id => id;

        /// <summary>
        /// Возвращает успешен ли был ход
        /// </summary>
        public abstract bool Activate();

        /// <summary>
        /// Memberwise clone
        /// </summary>
        public BlockType Clone() => (BlockType)MemberwiseClone();
    }
}
