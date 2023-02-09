using Model.Objects;
using UnityEngine;

namespace Model.Systems
{
    public interface IMoveSystem
    {
        public SwapBlocksAction Move(Vector2Int _startPosition, Directions direction);
        public void SetLevel(Level level);
    }
}