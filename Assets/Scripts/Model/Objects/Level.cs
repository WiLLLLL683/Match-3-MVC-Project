using System;
using Model.Readonly;

namespace Model.Objects
{
    /// <summary>
    /// Объект уровня с игровой доской и правилами
    /// </summary>
    [Serializable]
    public class Level
    {
        public GameBoard gameBoard;
        public Counter[] goals;
        public Counter[] restrictions;
        public MatchPattern[] matchPatterns;
    }
}