using Model.Objects;
using System.Numerics;
using UnityEngine;

namespace TestUtils
{
    public static class TestCellFactory
    {
        public static CellType BasicCellType = CreateCellType(true, true);
        public static CellType InvisibleCellType = CreateCellType(false, true);
        public static CellType NotPlayableCellType = CreateCellType(false, false);

        public static Cell CreateCell(CellType type, Vector2Int position = default) => new Cell(type, position);

        private static CellType CreateCellType(bool isPlayable, bool canContainBlock = true) => new BasicCellType(isPlayable, canContainBlock);
    }
}