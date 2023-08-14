using Model.Objects;
using System;
using UnityEngine;

namespace Data
{
    /// <summary>
    /// Класс для отображения в инспекторе пары: тип блока - вероятность его выпадения
    /// </summary>
    [Serializable]
    public class BlockType_Weight
    {
        public BlockTypeSO blockTypeSO;
        public IBlockType blockType;
        public int weight;

        public BlockType_Weight(IBlockType blockType, int weight = 0)
        {
            this.blockType = blockType;
            this.weight = weight;
        }

        public void LinkBlockTypeToSO() => blockType = blockTypeSO.blockType;
    }
}