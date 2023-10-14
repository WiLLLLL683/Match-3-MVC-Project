using Model.Objects;

namespace TestUtils
{
    public static class TestCellFactory
    {
        public static CellType BasicCellType = CreateCellType(true, true);
        public static CellType InvisibleCellType = CreateCellType(false, true);
        public static CellType NotPlayableCellType = CreateCellType(false, false);

        private static CellType CreateCellType(bool isPlayable, bool canContainBlock = true) => new BasicCellType(isPlayable, canContainBlock);
    }
}