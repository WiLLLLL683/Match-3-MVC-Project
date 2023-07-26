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
    public interface IGameBoardPresenter : IPresenter
    {
        public void Destroy();
        public void SpawnBlocks();
        public void SpawnCells();
        public IBlockView GetBlockView(Vector2Int modelPosition);
        public ICellView GetCellView(Vector2Int modelPosition);
    }
}