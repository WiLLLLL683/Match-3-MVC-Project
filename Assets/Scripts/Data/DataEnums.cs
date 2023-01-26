using System;
using Array2DEditor;
using Model.Objects;
using Model.Systems;

namespace Array2DEditor
{
    public enum CellTypeEnum
    {
        NotPlayable,
        Basic
    }
    public enum BlockTypeEnum
    {
        Blue,
        Red
    }
}

namespace Data
{
    public enum CounterTargetEnum
    {
        Block,
        Cell,
        Turn
    }

    public static class DataFromEnum 
    {
        public static ABlockType GetBlockType(BlockTypeEnum _enum)
        {
            switch (_enum)
            {
                case BlockTypeEnum.Blue:
                    return new BlueBlockType();
                case BlockTypeEnum.Red:
                    return new RedBlockType();
                default:
                    return null;
            }
        }

        public static ACellType GetCellType(CellTypeEnum _enum)
        {
            switch (_enum)
            {
                case CellTypeEnum.NotPlayable:
                    return new NotPlayableCellType();
                case CellTypeEnum.Basic:
                    return new BasicCellType();
                default:
                    return null;
            }
        }

        //public static ICounterTarget GetCounterTarget(CounterDataForUnity counterData)
        //{
        //    switch (counterData.targetType)
        //    {
        //        case CounterTargetEnum.Block:
        //            switch (counterData.blockType)
        //            {
        //                case BlockTypeEnum.Blue:
        //                    return new BlueBlockType();
        //                case BlockTypeEnum.Red:
        //                    return new RedBlockType();
        //                default:
        //                    return null;
        //            }
        //        case CounterTargetEnum.Cell:
        //            switch (counterData.cellType)
        //            {
        //                case CellTypeEnum.Basic:
        //                    return new BasicCellType();
        //                default:
        //                    return null;
        //            };
        //        case CounterTargetEnum.Turn:
        //            return new Turn();
        //        default:
        //            return null;
        //    }
        //}
    }
}