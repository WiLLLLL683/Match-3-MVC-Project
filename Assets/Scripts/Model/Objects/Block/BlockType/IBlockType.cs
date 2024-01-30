using Model.Services;
using System;
using UnityEngine;
namespace Model.Objects
{
    /// <summary>
    /// Тип блока с возможностью активации. <br/>
    /// </summary>
    public interface IBlockType: ICounterTarget
    {
        /// <summary>
        /// Возвращает успешен ли был ход
        /// </summary>
        public bool Activate(Vector2Int position, IBlockDestroyService destroyService);

        /// <summary>
        /// Memberwise clone
        /// </summary>
        public IBlockType Clone();
    }
}
