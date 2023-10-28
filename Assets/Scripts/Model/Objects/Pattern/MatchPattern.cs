using System;

namespace Model.Objects
{
    [Serializable]
    public class MatchPattern : Pattern
    {
        public HintPattern[] hintPatterns;

        public MatchPattern(bool[,] grid, HintPattern[] hintPatterns) : base(grid)
        {
            this.hintPatterns = hintPatterns;
        }
    }
}