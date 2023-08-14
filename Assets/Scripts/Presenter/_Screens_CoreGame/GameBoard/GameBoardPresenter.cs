using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Model.Readonly;
using View;
using Utils;
using System;

namespace Presenter
{
    public class GameBoardPresenter : IGameBoardPresenter
    {
        /// <summary>
        /// Реализация фабрики использующая класс презентера в котором находится.
        /// </summary>
        public class Factory : AFactory<IGameBoard_Readonly, AGameBoardView, IGameBoardPresenter>
        {
            private readonly AFactory<IBlock_Readonly, ABlockView, IBlockPresenter> blockFactory;
            private readonly AFactory<ICell_Readonly, ACellView, ICellPresenter> cellFactory;
            private readonly AFactory<ICell_Readonly, ACellView, ICellPresenter> invisibleCellFactory;
            public Factory(AGameBoardView viewPrefab,
                AFactory<IBlock_Readonly, ABlockView, IBlockPresenter> blockFactory,
                AFactory<ICell_Readonly, ACellView, ICellPresenter> cellFactory,
                AFactory<ICell_Readonly, ACellView, ICellPresenter> invisibleCellFactory) : base(viewPrefab)
            {
                this.invisibleCellFactory = invisibleCellFactory;
                this.blockFactory = blockFactory;
                this.cellFactory = cellFactory;
            }

            public override IGameBoardPresenter Connect(AGameBoardView existingView, IGameBoard_Readonly model)
            {
                var presenter = new GameBoardPresenter(model, existingView, blockFactory, cellFactory, invisibleCellFactory);
                presenter.Enable();
                allPresenters.Add(presenter);
                return presenter;
            }
        }

        private readonly IGameBoard_Readonly model;
        private readonly AGameBoardView view;
        private readonly AFactory<IBlock_Readonly, ABlockView, IBlockPresenter> blockFactory;
        private readonly AFactory<ICell_Readonly, ACellView, ICellPresenter> cellFactory;
        private readonly AFactory<ICell_Readonly, ACellView, ICellPresenter> invisibleCellFactory;

        private readonly Dictionary<ICell_Readonly, ACellView> cells = new();
        private readonly Dictionary<IBlock_Readonly, ABlockView> blocks = new();

        public GameBoardPresenter(IGameBoard_Readonly model, AGameBoardView view,
            AFactory<IBlock_Readonly, ABlockView, IBlockPresenter> blockFactory,
            AFactory<ICell_Readonly, ACellView, ICellPresenter> cellFactory,
            AFactory<ICell_Readonly, ACellView, ICellPresenter> invisibleCellFactory)
        {
            this.model = model;
            this.view = view;
            this.blockFactory = blockFactory;
            this.cellFactory = cellFactory;
            this.invisibleCellFactory = invisibleCellFactory;

            this.blockFactory.SetParent(view.BlocksParent);
            this.cellFactory.SetParent(view.CellsParent);
            this.invisibleCellFactory.SetParent(view.CellsParent);
        }

        public void Enable()
        {
            SpawnAllCells();
            SpawnAllBlocks();
            model.OnBlockSpawn += SpawnBlock;
            Debug.Log($"{this} enabled");
        }


        public void Disable()
        {
            ClearAllBlocks();
            ClearAllCells();
            model.OnBlockSpawn -= SpawnBlock;
            Debug.Log($"{this} disabled");
        }
        public void Destroy()
        {
            Disable();
            GameObject.Destroy(view.gameObject);
        }
        public ACellView GetCellView(Vector2Int modelPosition)
        {
            ICell_Readonly cellModel = model.Cells_Readonly[modelPosition.x, modelPosition.y];
            return cells[cellModel];
        }
        public ABlockView GetBlockView(Vector2Int modelPosition)
        {
            IBlock_Readonly blockModel = model.Cells_Readonly[modelPosition.x, modelPosition.y].Block_Readonly;
            if (blockModel != null && blocks.ContainsKey(blockModel))
                return blocks[blockModel];
            else
                return null;
        }



        [Button]
        private void SpawnAllCells()
        {
            ClearAllCells();

            int xLength = model.Cells_Readonly.GetLength(0);
            int yLength = model.Cells_Readonly.GetLength(1);

            for (int y = 0; y < yLength; y++)
            {
                for (int x = 0; x < xLength; x++)
                {
                    ICell_Readonly cellModel = model.Cells_Readonly[x, y];

                    if (cellModel.IsPlayable)
                    {
                        cells[cellModel] = cellFactory.Create(cellModel).View; //спавн обычных клеток
                    }
                    else
                    {
                        cells[cellModel] = invisibleCellFactory.Create(cellModel).View; //спавн невидимых клеток если клетка не играбельна
                    }
                }
            }
        }
        [Button]
        private void SpawnAllBlocks()
        {
            ClearAllBlocks();

            foreach (var blockModel in model.Blocks_Readonly)
            {
                blocks[blockModel] = blockFactory.Create(blockModel).View;
            }
        }
        private void ClearAllCells()
        {
            cellFactory.Clear();
            cellFactory.ClearParent();
            cells.Clear();
        }
        private void ClearAllBlocks()
        {
            blockFactory.Clear();
            blockFactory.ClearParent();
            blocks.Clear();
        }
        private void SpawnBlock(IBlock_Readonly blockModel)
        {
            blocks.Add(blockModel, blockFactory.Create(blockModel).View);
        }
    }
}