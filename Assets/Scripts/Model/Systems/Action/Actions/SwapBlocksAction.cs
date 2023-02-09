using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Systems
{
    /// <summary>
    /// Смена блоков местами в двух клетках
    /// </summary>
    public class SwapBlocksAction : IAction
    {
        private Cell cellA;
        private Cell cellB;

        public SwapBlocksAction(Cell _cellA, Cell _cellB)
        {
            cellA = _cellA;
            cellB = _cellB;
        }

        public void Execute()
        {
            SwapTwoBlocks(cellA, cellB);
        }

        public void Undo()
        {
            SwapTwoBlocks(cellB, cellA);
        }

        private void SwapTwoBlocks(Cell _A, Cell _B)
        {
            if (_A != null && _B != null)
            {
                Block tempBlock = _A.Block;
                _A.SetBlock(_B.Block);
                _B.SetBlock(tempBlock);
            }
        }
    }
}
