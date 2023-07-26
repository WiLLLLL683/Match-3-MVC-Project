using NaughtyAttributes;
using UnityEngine;
using Model.Infrastructure;
using Model.Readonly;
using View;
using System.Collections.Generic;
using Data;

namespace Presenter
{
    public class GameBoardPresenter : IGameBoardPresenter
    {
        private IGameBoardView view;
        private IGameBoard_Readonly model;
        private AFactory<IBlock_Readonly, IBlockView, IBlockPresenter> blockFactory;
        private AFactory<ICell_Readonly, ICellView, ICellPresenter> cellFactory;

        private Dictionary<ICell_Readonly, ICellView> cells = new();
        private Dictionary<IBlock_Readonly, IBlockView> blocks = new();

        public GameBoardPresenter(IGameBoard_Readonly model,
                         IGameBoardView view,
                         AFactory<IBlock_Readonly, IBlockView, IBlockPresenter> blockFactory,
                         AFactory<ICell_Readonly, ICellView, ICellPresenter> cellFactory)
        {
            this.view = view;
            this.model = model;
            this.blockFactory = blockFactory;
            this.cellFactory = cellFactory;

            this.blockFactory.SetParent(this.view.BlocksParent);
            this.cellFactory.SetParent(this.view.CellsParent);
        }
        public void Enable()
        {
            //TODO спавн блоков по событию в модели
        }
        public void Disable()
        {

        }
        public void Destroy()
        {
            Disable();
            GameObject.Destroy(view.gameObject);
        }
        [Button]
        public void SpawnCells()
        {
            cellFactory.Clear();
            cellFactory.ClearParent();
            cells.Clear();

            int xLength = model.Cells_Readonly.GetLength(0);
            int yLength = model.Cells_Readonly.GetLength(1);

            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    ICell_Readonly cellModel = model.Cells_Readonly[x, y];
                    cells[cellModel] = cellFactory.Create(cellModel);
                }
            }
        }
        [Button]
        public void SpawnBlocks()
        {
            blockFactory.Clear();
            blockFactory.ClearParent();
            blocks.Clear();

            foreach (var blockModel in model.Blocks_Readonly)
            {
                blocks[blockModel] = blockFactory.Create(blockModel);
            }
        }
        public ICellView GetCellView(Vector2Int modelPosition)
        {
            ICell_Readonly cellModel = model.Cells_Readonly[modelPosition.x, modelPosition.y];
            return cells[cellModel];
        }
        public IBlockView GetBlockView(Vector2Int modelPosition)
        {
            IBlock_Readonly blockModel = model.Cells_Readonly[modelPosition.x, modelPosition.y].Block_Readonly;
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
                spawnedBlocks[blockModel] = blockFactory.Create(blockModel);
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
                    spawnedCells[cellModel] = cellFactory.Create(cellModel);
                }
            }

            return spawnedCells;
        }
    }
}