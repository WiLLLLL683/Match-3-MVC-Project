using Model.Services;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    [Serializable]
    public class DestroyHorizontalLineBooster : IBooster
    {
        [SerializeField] private int id;
        public int Id => id;

        public void Execute(Vector2Int startPosition, IBlockDestroyService destroyService, IBlockMoveService _)
        {
            destroyService.MarkToDestroyHorizontalLine(startPosition.y);
        }

        public IBooster Clone() => (IBooster)MemberwiseClone();
    }
}