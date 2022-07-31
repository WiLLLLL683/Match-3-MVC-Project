using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Systems
{
    /// <summary>
    /// Смена блоков местами в двух блоков
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
            SwapTwoBlocks();
        }

        public void Undo()
        {
            throw new System.NotImplementedException();
        }
        
        private void SwapTwoBlocks()
        {
            if (cellA != null && cellB != null)
            {
                Block tempBlock = cellA.block;
                cellA.SetBlock(cellB.block);
                cellB.SetBlock(tempBlock);
            }
        }
    }
}
