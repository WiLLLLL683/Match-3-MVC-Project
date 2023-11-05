using System;
using System.Collections.Generic;
namespace Model.Objects
{
    /// <summary>
    /// Объект игрового поля, хранит сетку клеток и все блоки на поле
    /// </summary>
    [Serializable]
    public class GameBoard
    {
        public Cell[,] Cells;
        public int RowsOfInvisibleCells;
        public List<Block> Blocks;

        public GameBoard(Cell[,] cells, int rowsOfInvisibleCells, List<Block> blocks = null)
        {
            Cells = cells;
            RowsOfInvisibleCells = rowsOfInvisibleCells;
            Blocks = blocks ?? new List<Block>();
        }
    }
}
