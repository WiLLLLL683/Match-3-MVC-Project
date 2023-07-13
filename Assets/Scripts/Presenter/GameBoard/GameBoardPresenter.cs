using System;
using System.Collections;
using Model.Infrastructure;
using NaughtyAttributes;
using UnityEngine;

namespace Presenter
{
    /// <summary>
    /// Контроллер для игрового поля
    /// </summary>
    public class GameBoardPresenter : MonoBehaviour, IGameBoardPresenter
    {
        [SerializeField] private BlockSpawner blockSpawner;
        [SerializeField] private CellSpawner cellSpawner;

        private Game game;

        public void Init(Game game)
        {
            this.game = game;
        }

        [Button]
        public void SpawnCells()
        {
            cellSpawner.Clear();
            cellSpawner.SpawnGameBoard(game.Level.gameBoard);
        }
        [Button]
        public void SpawnBlocks()
        {
            blockSpawner.Clear();
            blockSpawner.SpawnGameBoard(game.Level.gameBoard);
        }
    }
}