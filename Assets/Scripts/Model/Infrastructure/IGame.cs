using Model.Objects;
using UnityEngine;

namespace Model.Infrastructure
{
    public interface IGame
    {
        void ActivateBlock(Vector2Int blockPosition);
        void MoveBlock(Vector2Int blockPosition, Directions direction);
        void ActivateBooster(IBooster booster);
    }
}