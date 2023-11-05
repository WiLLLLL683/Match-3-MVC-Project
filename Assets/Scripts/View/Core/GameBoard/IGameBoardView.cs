using UnityEngine;

namespace View
{
    public interface IGameBoardView
    {
        public abstract Transform BlocksParent { get; }
        public abstract Transform CellsParent { get; }
    }
}