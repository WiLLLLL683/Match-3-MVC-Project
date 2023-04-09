using Model.Infrastructure;
using NaughtyAttributes;
using System;
using System.Collections;
using UnityEngine;
using View;

namespace Controller
{
    /// <summary>
    /// Контроллер для игрового поля
    /// </summary>
    public class GameBoardController : MonoBehaviour
    {
        [SerializeField] private BlockSpawner blockSpawner;
        [SerializeField] private CellSpawner cellSpawner;

        private Game game;

        public void Init(Game _game)
        {
            game = _game;
        }

        [Button]
        public void SpawnCells()
        {
            cellSpawner.Clear();
            cellSpawner.SpawnGameboard(game.Level.gameBoard);
        }
        [Button]
        public void SpawnBlocks()
        {
            blockSpawner.Clear();
            blockSpawner.SpawnGameboard(game.Level.gameBoard);
        }
    }
}