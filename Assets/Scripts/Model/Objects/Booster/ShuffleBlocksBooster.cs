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
        public int Id => id;

        public ShuffleBlocksBooster() { }

        public HashSet<Cell> Execute(Vector2Int _, GameBoard gameboard, IValidationService validationService, IBlockMoveService moveService)
        {
            List<Block> blockInPlayArea = new();

            for (int x = 0; x < gameboard.Cells.GetLength(0); x++)
            {
                for (int y = 0; y < gameboard.HiddenRowsStartIndex; y++)
                {
                    if (validationService.BlockExistsAt(new(x,y)))
                        blockInPlayArea.Add(gameboard.Cells[x, y].Block);
                }
            }

            //Fisher–Yates shuffle
            for (int i = blockInPlayArea.Count - 1; i >= 1; i--)
            {
                int random = UnityEngine.Random.Range(0, i);
                moveService.Move(blockInPlayArea[i].Position, blockInPlayArea[random].Position);
            }

            return new();
        }

        public IBooster Clone() => (IBooster)MemberwiseClone();
    }
}