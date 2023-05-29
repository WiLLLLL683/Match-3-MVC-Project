using Model;
using Model.Infrastructure;
using NaughtyAttributes;
using System;
using System.Collections;
using UnityEngine;

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
        private InputBase input;

        public void Init(Game game, InputBase input)
        {
            this.game = game;
            this.input = input;

            input.OnSwipeMoving += GrabBlock;
            input.OnSwipeEnded += MoveBlock;
            input.OnTap += ActivateBlock;
        }
        private void OnDestroy()
        {
            input.OnSwipeMoving -= GrabBlock;
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
            blockSpawner.SpawnGameboard(game.Level.gameBoard);
        }



        private void GrabBlock(Block block, Vector2 deltaPosition)
        {
            block.GrabBlock(deltaPosition);
            Debug.Log(block + " grabbed", block);
        }
        private void MoveBlock(Block block, Directions direction)
        {
            block.ReturnBlock();
            Debug.Log(block + " moved " + direction);
            //TODO вызов модели
        }
        private void ActivateBlock(Block block)
        {
            block.TapBlock();
            Debug.Log(block + " activated");
            //TODO активация блока в клетке?
        }
    }
}