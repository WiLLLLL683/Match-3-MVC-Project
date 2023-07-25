using NaughtyAttributes;
using UnityEngine;
using Model.Infrastructure;
using Model.Readonly;
using View;
using System.Collections.Generic;
using Data;

namespace Presenter
{
    public class GameBoardPresenter : MonoBehaviour, IGameBoardPresenter
    {
        [SerializeField] private Transform blocksParent;
        [SerializeField] private Transform cellsParent;

        private Dictionary<ICell_Readonly, ICellView> cells = new();
        private Dictionary<IBlock_Readonly, IBlockView> blocks = new();
        private Game game;
        private IGameBoard_Readonly gameBoard;
        private IFactory<IBlock_Readonly, IBlockView> blockSpawner;
        private IFactory<ICell_Readonly, ICellView> cellSpawner;

        public void Init(Game game, IGameBoard_Readonly gameBoard, PrefabConfig prefabConfig)
        {
            this.game = game;
            this.gameBoard = gameBoard;
            this.blockSpawner = new BlockFactory(prefabConfig.blockPrefab, blocksParent);
            this.cellSpawner = new CellFactory(prefabConfig.cellPrefab, cellsParent);
            //TODO спавн блоков по событию в модели
        }
        [Button]
        public void SpawnCells()
        {
            cellSpawner.Clear();
            cells.Clear();

            int xLength = gameBoard.Cells_Readonly.GetLength(0);
            int yLength = gameBoard.Cells_Readonly.GetLength(1);

            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    ICell_Readonly cellModel = gameBoard.Cells_Readonly[x, y];
                    cells[cellModel] = cellSpawner.Create(cellModel);
                }
            }
        }
        [Button]
        public void SpawnBlocks()
        {
            blockSpawner.Clear();
            blocks.Clear();

            foreach (var blockModel in gameBoard.Blocks_Readonly)
            {
                blocks[blockModel] = blockSpawner.Create(blockModel);
            }
        }
        public ICellView GetCellView(Vector2Int modelPosition)
        {
            ICell_Readonly cellModel = gameBoard.Cells_Readonly[modelPosition.x, modelPosition.y];
            return cells[cellModel];
        }
        public IBlockView GetBlockView(Vector2Int modelPosition)
        {
            IBlock_Readonly blockModel = gameBoard.Cells_Readonly[modelPosition.x, modelPosition.y].Block_Readonly;
            if (blockModel != null && blocks.ContainsKey(blockModel))
                return blocks[blockModel];
            else
                return null;
        }



        private Dictionary<IBlock_Readonly, IBlockView> SpawnAllBlocks(IGameBoard_Readonly gameBoard)
        {
            Dictionary<IBlock_Readonly, IBlockView> spawnedBlocks = new();

            foreach (var blockModel in gameBoard.Blocks_Readonly)
            {
                spawnedBlocks[blockModel] = blockSpawner.Create(blockModel);
            }

            return spawnedBlocks;
        }
        private Dictionary<ICell_Readonly, ICellView> SpawnAllCells(IGameBoard_Readonly gameBoard)
        {
            int xLength = gameBoard.Cells_Readonly.GetLength(0);
            int yLength = gameBoard.Cells_Readonly.GetLength(1);
            Dictionary<ICell_Readonly, ICellView> spawnedCells = new();

            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    ICell_Readonly cellModel = gameBoard.Cells_Readonly[x, y];
                    spawnedCells[cellModel] = cellSpawner.Create(cellModel);
                }
            }

            return spawnedCells;
        }
    }
}