using Model.Services;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    [Serializable]
    public class DestroyHorizontalLineBooster : IBooster
    {
        [SerializeField] private int id;
        public int Id => id;

        public HashSet<Cell> Execute(Vector2Int startPosition, GameBoard gameboard, IValidationService validationService, IBlockMoveService moveService)
        {
            HashSet<Cell> blocksToDestroy = new();

            for (int x = 0; x < gameboard.Cells.GetLength(0); x++)
            {
                Vector2Int position = new(x, startPosition.y);
                if (!validationService.BlockExistsAt(position))
                    continue;

                blocksToDestroy.Add(gameboard.Cells[position.x, position.y]);
            }

            return blocksToDestroy;
        }

        public IBooster Clone() => (IBooster)MemberwiseClone();
    }
}