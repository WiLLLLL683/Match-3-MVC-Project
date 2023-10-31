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
        public abstract ABlockView GetBlockView(Vector2Int modelPosition);
        public abstract ACellView GetCellView(Vector2Int modelPosition);
    }
}