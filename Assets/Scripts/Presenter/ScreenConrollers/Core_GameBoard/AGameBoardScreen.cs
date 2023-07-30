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
    public abstract class AGameBoardScreen : AScreenController
    {
        public abstract void Init(IGameBoard_Readonly model,
                 AFactory<IBlock_Readonly, IBlockView, IBlockPresenter> blockFactory,
                 AFactory<ICell_Readonly, ICellView, ICellPresenter> cellFactory);
        public abstract IBlockView GetBlockView(Vector2Int modelPosition);
        public abstract ICellView GetCellView(Vector2Int modelPosition);
    }
}