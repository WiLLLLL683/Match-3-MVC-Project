using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Services
{
    public class CellSetBlockService : ICellSetBlockService
    {
        public event Action<Cell> OnEmpty;

        public void SetBlock(Cell cell, Block block)
        {
            if (!cell.Type.CanContainBlock)
                return;

            if (block != null)
            {
                cell.Block = block;
                cell.Block.Position = cell.Position;
            }
            else
            {
                SetEmpty(cell);
            }
        }

        public void SetEmpty(Cell cell)
        {
            cell.Block = null;
            OnEmpty?.Invoke(cell);
        }
    }
}