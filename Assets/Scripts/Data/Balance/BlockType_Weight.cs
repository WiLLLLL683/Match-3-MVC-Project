using Model.Objects;
using System;
using UnityEngine;

namespace Data
{
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