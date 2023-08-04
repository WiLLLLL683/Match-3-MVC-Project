using Model.Objects;
using Model.Readonly;
using UnityEngine;

namespace Model.Infrastructure
{
    public interface IGame
    {
        void ActivateBlock(Vector2Int blockPosition);
        void MoveBlock(Vector2Int blockPosition, Directions direction);
        void ActivateBooster(IBooster_Readonly booster);
    }
}