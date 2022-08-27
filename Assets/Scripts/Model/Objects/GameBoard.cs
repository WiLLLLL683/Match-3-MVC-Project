using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    public class GameBoard
    {
        public Cell[,] cells { get; private set; }
        public List<Block> blocks { get; private set; }

        public GameBoard(GameBoardData data)
        {
            cells = new Cell[data.cellTypes.GetLength(0), data.cellTypes.GetLength(1)];

            for (int i = 0; i < data.cellTypes.GetLength(0); i++)
            {
                for (int j = 0; j < data.cellTypes.GetLength(1); j++)
                {
                    cells[i, j] = new Cell(data.cellTypes[i,j]);
                }
            }
        }

        public GameBoard(int xLength, int yLength)
        {
            cells = new Cell[xLength, yLength];

            for (int i = 0; i < xLength; i++)
            {
                for (int j = 0; j < yLength; j++)
                {
                    cells[i, j] = new Cell(new BasicCellType());
                }
            }
        }

        public void RegisterBlock(Block _block)
        {
            blocks.Add(_block);
            _block.OnDestroy += UnRegisterBlock;
        }

        private void UnRegisterBlock(Block _block, EventArgs eventArgs)
        {
            blocks.Remove(_block);
        }
    }
}
