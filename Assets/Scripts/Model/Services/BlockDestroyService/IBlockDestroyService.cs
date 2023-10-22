using Model.Objects;
using System;
using UnityEngine;

namespace Model.Services
{
    public interface IBlockDestroyService
    {
        event Action<Block> OnDestroy;

        void SetLevel(GameBoard gameBoard);
        void DestroyAt(Vector2Int position);
        void DestroyAt(Cell cell);
    }
}