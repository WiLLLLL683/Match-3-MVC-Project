using Model.Readonly;
using System;

namespace Model.Objects
{
    /// <summary>
    /// ������ ������ � ������� ������ � ���������
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

        public IGameBoard_Readonly GameBoard_Readonly => gameBoard;
        public ICounter_Readonly[] Goals_Readonly => goals;
        public ICounter_Readonly[] Restrictions_Readonly => goals;

        public void SetWin() => OnWin?.Invoke();
        public void SetLose() => OnLose?.Invoke();
    }
}