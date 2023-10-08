using System;
using System.Collections.Generic;
using Model.Readonly;

namespace Model.Objects
{
    /// <summary>
    /// Объект игрового поля, хранит сетку клеток и все блоки на поле
    /// </summary>
    [Serializable]
    public class GameBoard : IGameBoard_Readonly
    {
        public Cell[,] Cells;
        public int RowsOfInvisibleCells;
        public List<Block> Blocks;
        public ICell_Readonly[,] Cells_Readonly => Cells;
        public IEnumerable<IBlock_Readonly> Blocks_Readonly => Blocks;
        int IGameBoard_Readonly.RowsOfInvisibleCells => RowsOfInvisibleCells;

        public event Action<IBlock_Readonly> OnBlockSpawn;

        public GameBoard(Cell[,] cells, int rowsOfInvisibleCells, List<Block> blocks = null)
        {
            Cells = cells;
            RowsOfInvisibleCells = rowsOfInvisibleCells;
            Blocks = new List<Block>();

            if (blocks == null)
                return;

            for (int i = 0; i < blocks.Count; i++)
            {
                RegisterBlock(blocks[i]);
            }
        }

        /// <summary>
        /// Регистрация блока в игровом поле
        /// </summary>
        public void RegisterBlock(Block block)
        {
            if (block != null)
            {
                Blocks.Add(block);
                block.OnDestroy += UnRegisterBlock;
                OnBlockSpawn?.Invoke(block);
            }
        }

        private void UnRegisterBlock(Block block) => Blocks.Remove(block);
    }
}
