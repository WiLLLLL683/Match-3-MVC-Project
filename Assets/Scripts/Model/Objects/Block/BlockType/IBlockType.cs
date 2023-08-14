using Model.Readonly;
using UnityEngine;

namespace Model.Objects
{
    /// <summary>
    /// Тип блока с возможностью активации
    /// </summary>
    public interface IBlockType : IBlockType_Readonly, ICounterTarget
    {
        /// <summary>
        /// Возвращает успешен ли был ход
        /// </summary>
        public bool Activate();
    }
}
