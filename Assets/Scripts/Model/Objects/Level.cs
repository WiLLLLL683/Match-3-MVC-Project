using System;

namespace Model.Objects
{
    /// <summary>
    /// ������ ������ � ������� ������ � ���������
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