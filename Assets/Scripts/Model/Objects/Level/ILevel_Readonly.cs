using Model.Objects;
using System;

namespace Model.Readonly
{
    public interface ILevel_Readonly
    {
        public abstract event Action OnWin;
        public abstract event Action OnLose;

        public IGameBoard_Readonly GameBoard_Readonly { get; }
        public Counter[] Goals_Readonly { get; }
        public Counter[] Restrictions_Readonly { get; }
    }
}