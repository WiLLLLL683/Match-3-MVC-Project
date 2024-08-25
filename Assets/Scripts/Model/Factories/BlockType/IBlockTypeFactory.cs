using System.Collections.Generic;
using Model.Objects;

namespace Model.Factories
{
    /// <summary>
    /// Сервис генерации рандомных типов блоков
    /// </summary>
    public interface IBlockTypeFactory
    {
        IBlockType Create(int id);
        IBlockType Create(IBlockType origin);
        public void SetLevelConfig(List<BlockType_Weight> typesWeight, IBlockType defaultBlockType);
        
        /// <summary>
        /// Получить рандомный тип блока с заданными вероятностями
        /// </summary>
        public IBlockType CreateRandom();
    }
}