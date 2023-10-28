using Model.Objects;
using System.Runtime.InteropServices;
using UnityEngine;

namespace TestUtils
{
    public static class TestPatternFactory
    {
        public static MatchPattern DotPattern1x1()
        {
            var grid = new bool[1,1];
            grid[0, 0] = true;
            return CreatePattern(grid);
        }

        public static MatchPattern VertLinePattern1x3()
        {
            var grid = new bool[1, 3];
            grid[0, 0] = true;
            grid[0, 1] = true;
            grid[0, 2] = true;
            return CreatePattern(grid);
        }

        public static MatchPattern CrossPattern3x3()
        {
            var grid = new bool[3, 3];
            grid[0, 1] = true;
            grid[1, 0] = true;
            grid[1, 1] = true;
            grid[1, 2] = true;
            grid[2, 1] = true;
            return CreatePattern(grid);
        }

        public static MatchPattern HorizLinePattern2x1()
        {
            var grid = new bool[2, 1];
            grid[0, 0] = true;
            grid[1, 0] = true;
            return CreatePattern(grid);
        }

        private static MatchPattern CreatePattern(bool[,] grid)
        {
            var hints = new HintPattern[1] { CreateDefaultHintPattern() };
            return new MatchPattern(grid, hints);
        }

        public static HintPattern CreateDefaultHintPattern()
        {
            var grid = new bool[1, 1];
            grid[0, 0] = true;
            var cellToMove = new Vector2Int(0, 0);
            return new(grid, cellToMove, Directions.Up);
        }
    }
}