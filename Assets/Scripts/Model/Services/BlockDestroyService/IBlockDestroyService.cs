using Model.Objects;
using System;
using UnityEngine;

namespace Model.Services
{
    public interface IBlockDestroyService
    {
        event Action<Block> OnDestroy;

        void DestroyAt(Vector2Int position);
        void DestroyAt(Cell cell);
    }
}