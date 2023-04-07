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
        [SerializeReference, SubclassSelector]
        public ABlockType blockType = new BlueBlockType();
        public int weight;

        public BlockType_Weight(ABlockType _blockType, int _weight = 0)
        {
            blockType = _blockType;
            weight = _weight;
        }
    }
}