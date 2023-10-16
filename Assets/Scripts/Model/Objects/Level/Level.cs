using System;
using Model.Readonly;

namespace Model.Objects
{
    /// <summary>
    /// Объект уровня с игровой доской и правилами
    /// </summary>
    [Serializable]
    public class Level : ILevel_Readonly
    {
        public GameBoard gameBoard;
        public Counter[] goals;
        public Counter[] restrictions;
        public MatchPattern[] matchPatterns;

        public event Action OnWin;
        public event Action OnLose;

        public Counter[] Goals_Readonly => goals;
        public Counter[] Restrictions_Readonly => goals;

        public void SetWin() => OnWin?.Invoke();
        public void SetLose() => OnLose?.Invoke();
    }
}