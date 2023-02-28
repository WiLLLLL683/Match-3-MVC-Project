using Model.Objects;
using UnityEngine;

namespace Model.Systems
{
    public interface IMoveSystem : ISystem
    {
        public SwapBlocksAction Move(Vector2Int _startPosition, Directions direction);
    }
}