using Model.Infrastructure;
using Model.Readonly;
using UnityEngine;
using Data;
using View;
using Utils;

namespace Presenter
{
    /// <summary>
    /// Менеджер игрового поля
    /// Управляет жизненным циклом клеток и блоков, предоставляет к ним доступ
    /// </summary>
    public interface IGameBoardPresenter : IPresenter
    {
        public abstract IBlockView GetBlockView(Vector2Int modelPosition);
        public abstract ICellView GetCellView(Vector2Int modelPosition);
    }
}