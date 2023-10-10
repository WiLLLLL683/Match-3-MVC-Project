using Model.Objects;
using UnityEngine;

namespace Model.Services
{
    public interface IBlockDestroyService
    {
        public void SetLevel(GameBoard gameBoard);
        public IAction Destroy(Cell cell);
        public IAction Destroy(Vector2Int position);
    }
}