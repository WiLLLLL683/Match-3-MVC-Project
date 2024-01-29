using UnityEngine;

namespace View
{
    public class GameBoardView : MonoBehaviour, IGameBoardView
    {
        [SerializeField] private Transform blocksParent;
        [SerializeField] private Transform cellsParent;

        public Transform BlocksParent => blocksParent;
        public Transform CellsParent => cellsParent;

        public void ClearBlocksParent()
        {
            for (int i = 0; i < BlocksParent.childCount; i++)
            {
                GameObject.Destroy(BlocksParent.GetChild(i).gameObject);
            }
        }

        public void ClearCellsParent()
        {
            for (int i = 0; i < CellsParent.childCount; i++)
            {
                GameObject.Destroy(CellsParent.GetChild(i).gameObject);
            }
        }
    }
}