using Model.Infrastructure;
using Model.Readonly;
using UnityEngine;
using Data;
using View;

namespace Presenter
{
    /// <summary>
    /// Менеджер игрового поля
    /// Управляет жизненным циклом клеток и блоков, предоставляет к ним доступ
    /// </summary>
    public interface IGameBoardPresenter
    {
        public GameObject gameObject { get; }

        public void Init(IGameBoard_Readonly gameBoard,
            FactoryBase<IBlock_Readonly, IBlockView> blockFactory,
            FactoryBase<ICell_Readonly, ICellView> cellFactory);
        public void SpawnBlocks();
        public void SpawnCells();
        public IBlockView GetBlockView(Vector2Int modelPosition);
        public ICellView GetCellView(Vector2Int modelPosition);
    }
}