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
        /// <summary>
        /// 2d-массив клеток, с координатами [x,y]. [0,0] - нижняя левая клетка.
        /// </summary>
        public Cell[,] Cells;

        /// <summary>
        /// Индекс[y] первого ряда скрытых клеток, нужных для незаметного спавна блоков
        /// </summary>
        public int HiddenRowsStartIndex;

        /// <summary>
        /// Список всех блоков
        /// </summary>
        public List<Block> Blocks;

        public GameBoard(Cell[,] cells, int hiddenCellsStartIndex, List<Block> blocks = null)
        {
            Cells = cells;
            HiddenRowsStartIndex = hiddenCellsStartIndex;
            Blocks = blocks ?? new List<Block>();
        }
    }
}
