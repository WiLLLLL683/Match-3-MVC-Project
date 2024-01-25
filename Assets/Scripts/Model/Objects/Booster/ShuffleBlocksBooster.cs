using Model.Services;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    [Serializable]
    public class ShuffleBlocksBooster : IBooster
    {
        [SerializeField] private int id;
        [SerializeField] [Min(0)] private int shuffleCount;
        [SerializeField] [Min(0)] private int randomIterationCount;
        public int Id => id;

        private GameBoard gameboard;
        private IValidationService validationService;

        public HashSet<Cell> Execute(Vector2Int _, GameBoard gameboard, IValidationService validationService, IBlockMoveService moveService)
        {
            this.gameboard = gameboard;
            this.validationService = validationService;

            for (int i = 0; i < shuffleCount; i++)
            {
                Cell startCell = GetRandomCellWithBlock();
                Cell targetCell = GetRandomCellWithBlock();

                if (startCell == null || targetCell == null)
                    continue;

                moveService.Move(startCell.Position, targetCell.Position);
            }

            return new();
        }

        public IBooster Clone() => (IBooster)MemberwiseClone();

        private Cell GetRandomCellWithBlock()
        {
            for (int i = 0; i < randomIterationCount; i++)
            {
                int x = UnityEngine.Random.Range(0, gameboard.Cells.GetLength(0));
                int y = UnityEngine.Random.Range(0, gameboard.HiddenRowsStartIndex);

                if (validationService.BlockExistsAt(new(x, y)))
                    return gameboard.Cells[x, y];
            }

            return null;
        }
    }
}