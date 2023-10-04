using Model.Objects;
using UnityEngine;

namespace Model.Services
{
    public interface IBlockDestroyService
    {
        IAction Destroy(GameBoard gameBoard, Cell cell);
        IAction Destroy(GameBoard gameBoard, Vector2Int position);
    }
}