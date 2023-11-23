using System.Collections.Generic;
using Model.Objects;

namespace Model.Services
{
    /// <summary>
    /// Сервис генерации рандомных типов блоков
    /// </summary>
    public interface IBlockRandomTypeService
    {
        public void SetLevelConfig(List<BlockType_Weight> typesWeight, BlockType defaultBlockType);
        
        /// <summary>
        /// Получить рандомный тип блока с заданными вероятностями
        /// </summary>
        public BlockType GetRandomBlockType();
    }
}