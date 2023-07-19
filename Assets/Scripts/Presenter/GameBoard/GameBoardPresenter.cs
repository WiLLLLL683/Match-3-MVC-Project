using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using Model.Infrastructure;
using Model.Objects;
using View;
using System.Collections.Generic;

namespace Presenter
{
    public class GameBoardPresenter : MonoBehaviour, IGameBoardPresenter
    {
        [SerializeField] private BlockSpawner blockSpawner;
        [SerializeField] private CellSpawner cellSpawner;

        private Dictionary<Cell, ICellView> cells;
        private Dictionary<Block, IBlockView> blocks;
        private Game game;
        private GameBoard gameBoard;

        public void Init(Game game, GameBoard gameBoard)
        {
            this.game = game;
            this.gameBoard = gameBoard;

            blockSpawner.Init(this);
            //TODO спавн блоков по событию в модели
        }
        public ICellView GetCellView(Vector2Int modelPosition)
        {
            Cell cellModel = gameBoard.Cells[modelPosition.x, modelPosition.y];
            return cells[cellModel];
        }
        public IBlockView GetBlockView(Vector2Int modelPosition)
        {
            Block blockModel = gameBoard.Cells[modelPosition.x, modelPosition.y].Block;
            return blocks[blockModel];
        }

        [Button]
        public void SpawnCells()
        {
            cellSpawner.Clear();
            cells = cellSpawner.SpawnGameBoard(gameBoard);
        }
        [Button]
        public void SpawnBlocks()
        {
            blockSpawner.Clear();
            blocks = blockSpawner.SpawnGameBoard(gameBoard);
        }
    }
}