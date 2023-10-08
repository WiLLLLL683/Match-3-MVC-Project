using Model.Objects;

namespace Model.Services
{
    /// <summary>
    /// Сервис по созданию блоков в заданном игровом поле
    /// </summary>
    public interface IBlockSpawnService
    {
        /// <summary>
        /// Задать данные о текущем уровне
        /// </summary>
        void SetLevel(GameBoard gameBoard, Balance balance);

        /// <summary>
        /// Заполнить пустые клетки случайными блоками только в рядах невидимых клеток
        /// </summary>
        void FillInvisibleRows();

        /// <summary>
        /// Создать или принудительно изменить тип блока в выбранной клетке
        /// </summary>
        void SpawnBlock_WithOverride(BlockType type, Cell cell);

        /// <summary>
        /// Создать или принудительно изменить тип блока на случайный в выбранной клетке
        /// </summary>
        void SpawnRandomBlock_WithOverride(Cell cell);

        /// <summary>
        /// Заполнить пустые клетки случайными блоками во всем игровом поле, включая невидимые клетки
        /// </summary>
        void FillGameBoard();
    }
}