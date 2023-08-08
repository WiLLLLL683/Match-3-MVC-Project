using System;
using Array2DEditor;
using Model.Objects;
using Model.Systems;

namespace Array2DEditor
{
    public enum CellTypeEnum
    {
        ____NotPlayable,
        O___Basic
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
        public static ACellType GetCellType(CellTypeEnum _enum)
        {
            switch (_enum)
            {
                case CellTypeEnum.____NotPlayable:
                    return new NotPlayableCellType();
                case CellTypeEnum.O___Basic:
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
        //                    return new BasicBlockType();
        //                case BlockTypeEnum.Red:
        //                    return new BasicBlockType();
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