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
        public bool IsActivatable { get; }

        /// <summary>
        /// Активация блока в заданной позиции.
        /// Direction - для активации перемещением блока.
        /// </summary>
        public UniTask Activate(Vector2Int position, Directions direction);
    }
}
