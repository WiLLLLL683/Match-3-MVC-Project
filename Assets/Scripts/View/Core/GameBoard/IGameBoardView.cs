using UnityEngine;

namespace View
{
    public interface IGameBoardView
    {
        Transform BlocksParent { get; }
        Transform CellsParent { get; }

        void ClearCellsParent();
        void ClearBlocksParent();
    }
}