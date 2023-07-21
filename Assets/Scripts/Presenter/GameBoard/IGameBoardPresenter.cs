using Model.Infrastructure;
using Model.Readonly;
using UnityEngine;
using View;

namespace Presenter
{
    /// <summary>
    /// Менеджер игрового поля
    /// Управляет жизненным циклом клеток и блоков, предоставляет к ним доступ
    /// </summary>
    public interface IGameBoardPresenter
    {
        public IBlockView GetBlockView(Vector2Int modelPosition);
        public ICellView GetCellView(Vector2Int modelPosition);

        public GameObject gameObject { get; }

        public void Init(Game game, IGameBoard_Readonly gameBoard);
        public void SpawnBlocks();
        public void SpawnCells();
    }
}