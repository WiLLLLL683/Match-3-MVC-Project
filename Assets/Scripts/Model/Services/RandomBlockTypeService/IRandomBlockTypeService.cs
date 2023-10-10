using System.Collections.Generic;
using Config;
using Model.Objects;

namespace Model.Services
{
    /// <summary>
    /// Сервис генерации рандомных типов блоков
    /// </summary>
    public interface IRandomBlockTypeService
    {
        public BlockType GetRandomBlockType();
        public void SetLevel(List<BlockType_Weight> typesWeight, BlockType defaultBlockType);
    }
}