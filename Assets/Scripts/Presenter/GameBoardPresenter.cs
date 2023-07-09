using Model;
using Model.Infrastructure;
using NaughtyAttributes;
using System;
using System.Collections;
using UnityEngine;
using View;

namespace Presenter
{
    /// <summary>
    /// Контроллер для игрового поля
    /// </summary>
    public class GameBoardPresenter : MonoBehaviour
    {
        [SerializeField] private BlockSpawner blockSpawner;
        [SerializeField] private CellSpawner cellSpawner;

        private Game game;
        private InputBase input;

        public void Init(Game game, InputBase input)
        {
            this.game = game;
            this.input = input;

            input.OnSwipeMoving += DragBlock;
            input.OnSwipeEnded += MoveBlock;
            input.OnTap += ActivateBlock;
        }
        private void OnDestroy()
        {
            input.OnSwipeMoving -= DragBlock;
            input.OnSwipeEnded -= MoveBlock;
            input.OnTap -= ActivateBlock;
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
            blockSpawner.SpawnGameBoard(game.Level.gameBoard);
        }



        private void DragBlock(IBlockPresenter block, Vector2 deltaPosition)
        {
            //TODO ограничение границами играбельных клеток
            block.Drag(deltaPosition);
            Debug.Log(block + " grabbed");
        }
        private void MoveBlock(IBlockPresenter block, Directions direction)
        {
            block.Move(direction);
            Debug.Log(block + " moved " + direction);
        }
        private void ActivateBlock(IBlockPresenter block)
        {
            block.Activate();
            Debug.Log(block + " activated");
            //TODO активация блока в клетке?
        }
    }
}