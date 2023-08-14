using Data;
using Model.Objects;

namespace Model.Systems
{
    /// <summary>
    /// Система для спавна новых блоков
    /// </summary>
    public interface ISpawnSystem : ISystem
    {
        /// <summary>
        /// спавн бонусных блоков
        /// </summary>
        public void SpawnBonusBlock(IBlockType _type, Cell _cell);

        /// <summary>
        /// спавн блоков по всем возможным клеткам в игровом поле с заменой существующих блоков
        /// </summary>
        public void SpawnGameBoard();

        /// <summary>
        /// спавн новых блоков вверху уровня при нехватке блоков ниже
        /// </summary>
        public void SpawnTopLine();

        /// <summary>
        /// спавн нового блока случайного типа в заданной клетке
        /// </summary>
        public void SpawnRandomBlock(Cell cell);
    }
}