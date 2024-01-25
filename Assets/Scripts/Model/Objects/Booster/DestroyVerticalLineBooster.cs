using Model.Services;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    [Serializable]
    public class DestroyVerticalLineBooster : IBooster
    {
        [SerializeField] private int id;
        public int Id => id;

        public HashSet<Cell> Execute(Vector2Int startPosition, GameBoard gameboard, IValidationService validationService, IBlockMoveService moveService)
        {
            HashSet<Cell> blocksToDestroy = new();

            for (int y = 0; y < gameboard.Cells.GetLength(1); y++)
            {
                Vector2Int position = new(startPosition.x, y);
                if (!validationService.BlockExistsAt(position))
                    continue;

                blocksToDestroy.Add(gameboard.Cells[position.x, position.y]);
            }

            return blocksToDestroy;
        }

        public IBooster Clone() => (IBooster)MemberwiseClone();
    }
}