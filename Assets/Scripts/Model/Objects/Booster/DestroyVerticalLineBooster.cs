using Model.Services;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    [Serializable]
    public class DestroyVerticalLineBooster : IBooster
    {
        [SerializeField] private int id;
        public int Id => id;

        public void Execute(Vector2Int startPosition, IBlockDestroyService destroyService, IBlockMoveService _)
        {
            destroyService.MarkToDestroyVerticalLine(startPosition.y);
        }

        public IBooster Clone() => (IBooster)MemberwiseClone();
    }
}