using Model.Services;
using System;
using UnityEngine;
namespace Model.Objects
{
    /// <summary>
    /// Тип блока с возможностью активации. <br/>
    /// (Абстрактный класс, вместо интерфейса IBlockType, чтобы использовать Id для BlockType и для ICounterTarget без конфликтов.)
    /// </summary>
    [Serializable]
    public abstract class BlockType : ICounterTarget
    {
        public int Id;
        int ICounterTarget.Id => Id;

        /// <summary>
        /// Возвращает успешен ли был ход
        /// </summary>
        public abstract bool Activate(Vector2Int position, IBlockDestroyService destroyService);

        /// <summary>
        /// Memberwise clone
        /// </summary>
        public BlockType Clone() => (BlockType)MemberwiseClone();
    }
}
