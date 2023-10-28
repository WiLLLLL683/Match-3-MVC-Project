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
        public void FillInvisibleRows();

        /// <summary>
        /// Создать или принудительно изменить тип блока в выбранной клетке
        /// </summary>
        public void SpawnBlock_WithOverride(Cell cell, BlockType type);

        /// <summary>
        /// Создать или принудительно изменить тип блока на случайный в выбранной клетке
        /// </summary>
        public void SpawnRandomBlock_WithOverride(Cell cell);

        /// <summary>
        /// Заполнить пустые клетки случайными блоками во всем игровом поле, включая невидимые клетки
        /// </summary>
        public void FillGameBoard();
    }
}