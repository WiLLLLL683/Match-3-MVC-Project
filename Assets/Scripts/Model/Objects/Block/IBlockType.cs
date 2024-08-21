using Cysharp.Threading.Tasks;
using Model.Services;
using System;
using UnityEngine;
using Utils;

namespace Model.Objects
{
    /// <summary>
    /// Тип блока с возможностью активации. <br/>
    /// </summary>
    public interface IBlockType: ICounterTarget
    {
        new public int Id { get; set; }

        /// <summary>
        /// Активация блока в заданной позиции. Возвращает успех активации (зависит от типа блока).
        /// Direction - для активации перемещением блока.
        /// </summary>
        public UniTask<bool> Activate(Vector2Int position, Directions direction, BlockTypeContext dependencies);

        /// <summary>
        /// Memberwise clone
        /// </summary>
        IBlockType Clone();
    }
}
