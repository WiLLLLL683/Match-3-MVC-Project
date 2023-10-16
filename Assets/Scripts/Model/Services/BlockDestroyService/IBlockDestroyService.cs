using Model.Objects;
using System;
using UnityEngine;

namespace Model.Services
{
    public interface IBlockDestroyService
    {
        event Action<Block> OnDestroy;

        public void SetLevel(GameBoard gameBoard);
        public IAction Destroy(Cell cell);
        public IAction Destroy(Vector2Int position);
    }
}