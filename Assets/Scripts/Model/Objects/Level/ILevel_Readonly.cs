using Model.Readonly;

namespace Model.Readonly
{
    public interface ILevel_Readonly
    {
        public IGameBoard_Readonly GameBoard_Readonly { get; }
        public ICounter_Readonly[] Goals_Readonly { get; }
        public ICounter_Readonly[] Restrictions_Readonly { get; }
        public bool CheckLose();
        public bool CheckWin();
    }
}