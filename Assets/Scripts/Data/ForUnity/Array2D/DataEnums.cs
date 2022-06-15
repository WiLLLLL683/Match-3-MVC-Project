using System;
using Array2DEditor;
using Model.Objects;


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

namespace Data.ForUnity
{
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
    }
}