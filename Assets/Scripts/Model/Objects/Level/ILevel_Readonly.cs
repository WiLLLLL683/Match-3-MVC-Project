using Model.Readonly;
using System;

namespace Model.Readonly
{
    public interface ILevel_Readonly
    {
        public abstract event Action OnWin;
        public abstract event Action OnLose;

        public IGameBoard_Readonly GameBoard_Readonly { get; }
        public ICounter_Readonly[] Goals_Readonly { get; }
        public ICounter_Readonly[] Restrictions_Readonly { get; }
    }
}