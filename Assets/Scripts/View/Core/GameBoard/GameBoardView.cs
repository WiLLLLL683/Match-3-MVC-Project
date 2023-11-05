using UnityEngine;

namespace View
{
    public class GameBoardView : MonoBehaviour, IGameBoardView
    {
        [SerializeField] private Transform blocksParent;
        [SerializeField] private Transform cellsParent;

        public Transform BlocksParent => blocksParent;
        public Transform CellsParent => cellsParent;
    }
}