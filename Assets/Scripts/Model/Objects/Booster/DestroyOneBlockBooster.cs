using Model.Services;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    [Serializable]
    public class DestroyOneBlockBooster : IBooster
    {
        [SerializeField] private int id;
        public int Id => id;

        public HashSet<Cell> Execute(Vector2Int startPosition, GameBoard gameboard, IValidationService validationService)
        {
            if (!validationService.BlockExistsAt(startPosition))
                return new();

            return new() { gameboard.Cells[startPosition.x, startPosition.y] };
        }

        public IBooster Clone() => (IBooster)MemberwiseClone();
    }
}