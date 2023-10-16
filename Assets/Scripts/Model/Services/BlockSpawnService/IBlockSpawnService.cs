using Model.Objects;
using System;
using System.Collections.Generic;

namespace Model.Services
{
    /// <summary>
    /// Сервис по созданию блоков в заданном игровом поле
    /// </summary>
    public interface IBlockSpawnService
    {
        event Action<Block> OnBlockSpawn;

        /// <summary>
        /// Задать данные о текущем уровне
        /// </summary>
        public void SetLevel(GameBoard gameBoard);

        /// <summary>
        /// Заполнить пустые клетки случайными блоками только в рядах невидимых клеток
        /// </summary>
        public List<IAction> FillInvisibleRows();

        /// <summary>
        /// Создать или принудительно изменить тип блока в выбранной клетке
        /// </summary>
        public IAction SpawnBlock_WithOverride(BlockType type, Cell cell);

        /// <summary>
        /// Создать или принудительно изменить тип блока на случайный в выбранной клетке
        /// </summary>
        public IAction SpawnRandomBlock_WithOverride(Cell cell);

        /// <summary>
        /// Заполнить пустые клетки случайными блоками во всем игровом поле, включая невидимые клетки
        /// </summary>
        public List<IAction> FillGameBoard();
    }
}