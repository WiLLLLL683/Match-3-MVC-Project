using Model.Services;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    public interface IBooster
    {
        int Id { get; }

        /// <summary>
        /// Использовать бустер.
        /// </summary>
        void Execute(Vector2Int startPosition, IBlockDestroyService destroyService, IBlockMoveService moveService);

        /// <summary>
        /// Memberwise clone.
        /// </summary>
        IBooster Clone();
    }
}