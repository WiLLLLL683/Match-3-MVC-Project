using Model.Services;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    [Serializable]
    public class ShuffleBlocksBooster : IBooster
    {
        [SerializeField] private int id;
        public int Id => id;

        public void Execute(Vector2Int _, IBlockDestroyService __, IBlockMoveService moveService)
        {
            moveService.ShuffleAllBlocks();
        }

        public IBooster Clone() => (IBooster)MemberwiseClone();
    }
}