using UnityEngine;
using View;

namespace Presenter
{
    /// <summary>
    /// Менеджер игрового поля
    /// Управляет жизненным циклом клеток и блоков, предоставляет к ним доступ
    /// </summary>
    public interface IBlocksPresenter : IPresenter
    {
        public abstract IBlockView GetBlockView(Vector2Int modelPosition);
    }
}