using Data;
using Model.Readonly;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    /// <summary>
    /// Объект игрового поля, хранит сетку клеток и все блоки на поле
    /// </summary>
    public class GameBoard : IGameBoard_Readonly
    {
        public Cell[,] cells;
        public int rowsOfInvisibleCells;
        public List<Block> blocks = new List<Block>();

        public event Action<IBlock_Readonly> OnBlockSpawn;

        public ICell_Readonly[,] Cells_Readonly => cells;
        public IEnumerable<IBlock_Readonly> Blocks_Readonly => blocks;
        public int RowsOfInvisibleCells => rowsOfInvisibleCells;

        /// <summary>
        /// Регистрация блока в игровом поле
        /// </summary>
        public void RegisterBlock(Block block)
        {
            if (block != null)
            {
                blocks.Add(block);
                block.OnDestroy += UnRegisterBlock;
                OnBlockSpawn?.Invoke(block);
            }
        }

        private void UnRegisterBlock(Block block) => blocks.Remove(block);
    }
}
