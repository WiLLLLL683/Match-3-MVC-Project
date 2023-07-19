using Model.Infrastructure;
using Model.Objects;
using System;
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
        IBlockView GetBlockView(Vector2Int modelPosition);
        ICellView GetCellView(Vector2Int modelPosition);

        public GameObject gameObject { get; }

        void Init(Game game, GameBoard gameBoard);
        void SpawnBlocks();
        void SpawnCells();
    }
}