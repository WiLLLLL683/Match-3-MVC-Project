using System;
using UnityEngine;
using Array2DEditor;
using NaughtyAttributes;

namespace Data.ForUnity
{
    [Serializable]
    public class CounterDataForUnity
    {
        public int count;
        public CounterTargetEnum targetType;
        [ShowIf("targetIsCell")] [AllowNesting]
        public CellTypeEnum cellType;
        [ShowIf("targetIsBlock")] [AllowNesting]
        public BlockTypeEnum blockType;

        private bool targetIsCell { get { return targetType == CounterTargetEnum.Cell; } }
        private bool targetIsBlock { get { return targetType == CounterTargetEnum.Block; } }
    }
}
