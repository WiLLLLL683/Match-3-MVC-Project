using Model.Objects;
using UnityEngine;

namespace Model.Systems
{
    public interface IMoveSystem
    {
        SwapBlocksAction Move(Vector2Int _startPosition, Directions direction);
        void SetLevel(Level level);
    }
}