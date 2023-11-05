using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Model.Services;
using Model.Objects;
using View;
using Utils;
using Zenject;
using Config;
using System;

namespace Presenter
{
    public class GameBoardPresenter : IGameBoardPresenter
    {
        ///// <summary>
        ///// Реализация фабрики использующая класс презентера в котором находится.
        ///// </summary>
        //public class Factory : AFactory<GameBoard, AGameBoardView, IGameBoardPresenter>
        //{
        //    private readonly IBlockSpawnService blockSpawnService;
        //    private readonly AFactory<Block, ABlockView, IBlockPresenter> blockFactory;
        //    private readonly AFactory<Cell, ACellView, ICellPresenter> cellFactory;
        //    private readonly AFactory<Cell, ACellView, ICellPresenter> invisibleCellFactory;
        //    public Factory(AGameBoardView viewPrefab,
        //        IBlockSpawnService blockSpawnService,
        //        AFactory<Block, ABlockView, IBlockPresenter> blockFactory,
        //        AFactory<Cell, ACellView, ICellPresenter> cellFactory,
        //        AFactory<Cell, ACellView, ICellPresenter> invisibleCellFactory) : base(viewPrefab)
        //    {
        //        this.blockSpawnService = blockSpawnService;
        //        this.invisibleCellFactory = invisibleCellFactory;
        //        this.blockFactory = blockFactory;
        //        this.cellFactory = cellFactory;
        //    }

        //    public override IGameBoardPresenter Connect(AGameBoardView existingView, GameBoard model)
        //    {
        //        var presenter = new GameBoardPresenter(model, existingView, blockSpawnService, blockFactory, cellFactory, invisibleCellFactory);
        //        presenter.Enable();
        //        allPresenters.Add(presenter);
        //        return presenter;
        //    }
        //}

        private readonly GameBoard model;
        private readonly AGameBoardView view;
        private readonly IBlockSpawnService blockSpawnService;
        private readonly BlockPresenter.Factory blockPresenterFactory;
        private readonly BlockView.Factory blockViewFactory;
        private readonly CellPresenter.Factory cellPresenterFactory;
        private readonly CellView.Factory cellViewFactory;
        private readonly CellView.Factory notPlayableCellViewFactory;
        private readonly CellTypeSetSO allCellTypes;
        private readonly BlockTypeSetSO blockTypeSet;
        //private readonly AFactory<Block, ABlockView, IBlockPresenter> blockFactory;
        //private readonly AFactory<Cell, ACellView, ICellPresenter> cellFactory;
        //private readonly AFactory<Cell, ACellView, ICellPresenter> invisibleCellFactory;

        private readonly Dictionary<Cell, ACellView> cells = new();
        private readonly Dictionary<Block, ABlockView> blocks = new();

        public GameBoardPresenter(GameBoard model,
            AGameBoardView view,
            IBlockSpawnService blockSpawnService,
            BlockPresenter.Factory blockPresenterFactory,
            BlockView.Factory blockViewFactory,
            CellPresenter.Factory cellPresenterFactory,
            [Inject(Id = "cellViewFactory")] CellView.Factory cellViewFactory,
            [Inject(Id = "notPlayableCellViewFactory")] CellView.Factory notPlayableCellViewFactory,
            CellTypeSetSO allCellTypes,
            BlockTypeSetSO blockTypeSet)
            //AFactory<Block, ABlockView, IBlockPresenter> blockFactory,
            //AFactory<Cell, ACellView, ICellPresenter> cellFactory,
            //AFactory<Cell, ACellView, ICellPresenter> invisibleCellFactory)
        {
            this.model = model;
            this.view = view;
            this.blockSpawnService = blockSpawnService;
            this.blockPresenterFactory = blockPresenterFactory;
            this.blockViewFactory = blockViewFactory;
            this.cellPresenterFactory = cellPresenterFactory;
            this.cellViewFactory = cellViewFactory;
            this.notPlayableCellViewFactory = notPlayableCellViewFactory;
            this.allCellTypes = allCellTypes;
            this.blockTypeSet = blockTypeSet;
            //this.blockFactory = blockFactory;
            //this.cellFactory = cellFactory;
            //this.invisibleCellFactory = invisibleCellFactory;

            //this.blockFactory.SetParent(view.BlocksParent);
            //this.cellFactory.SetParent(view.CellsParent);
            //this.invisibleCellFactory.SetParent(view.CellsParent);
        }

        public void Enable()
        {
            SpawnAllCells();
            SpawnAllBlocks();
            blockSpawnService.OnBlockSpawn += SpawnBlock;
            Debug.Log($"{this} enabled");
        }

        public void Disable()
        {
            blockSpawnService.OnBlockSpawn -= SpawnBlock;
            Debug.Log($"{this} disabled");
        }

        public ACellView GetCellView(Vector2Int modelPosition)
        {
            Cell cellModel = model.Cells[modelPosition.x, modelPosition.y];
            return cells[cellModel];
        }

        public ABlockView GetBlockView(Vector2Int modelPosition)
        {
            Block blockModel = model.Cells[modelPosition.x, modelPosition.y].Block;
            if (blockModel == null || !blocks.ContainsKey(blockModel))
                return null;

            return blocks[blockModel];
        }

        [Button]
        private void SpawnAllCells()
        {
            ClearAllCells();

            int xLength = model.Cells.GetLength(0);
            int yLength = model.Cells.GetLength(1);

            for (int y = 0; y < yLength; y++)
            {
                for (int x = 0; x < xLength; x++)
                {
                    Cell cellModel = model.Cells[x, y];

                    if (cellModel.Type.IsPlayable)
                    {
                        ACellView cellView = cellViewFactory.Create();
                        CreateCellPresenter(cellModel, cellView);
                    }
                    else
                    {
                        ACellView cellView = notPlayableCellViewFactory.Create();
                        CreateCellPresenter(cellModel, cellView);
                    }
                }
            }
        }

        [Button]
        private void SpawnAllBlocks()
        {
            ClearAllBlocks();

            foreach (var blockModel in model.Blocks)
            {
                SpawnBlock(blockModel);
            }
        }

        private void ClearAllCells()
        {
            foreach(Transform cell in view.CellsParent)
            {
                GameObject.Destroy(cell.gameObject);
            }

            cells.Clear();
        }

        private void ClearAllBlocks()
        {
            foreach (Transform block in view.BlocksParent)
            {
                GameObject.Destroy(block.gameObject);
            }

            blocks.Clear();
        }

        private void SpawnBlock(Block blockModel)
        {
            ABlockView blockView = blockViewFactory.Create();
            BlockTypeSO blockTypeSO = blockTypeSet.GetSO(blockModel.Type.Id);
            IBlockPresenter blockPresenter = blockPresenterFactory.Create(blockModel, blockView, blockTypeSO, blockTypeSet);
            blockPresenter.Enable();
            blocks.Add(blockModel, blockView);
        }

        private void CreateCellPresenter(Cell cellModel, ACellView cellView)
        {
            CellTypeSO cellTypeSO = allCellTypes.GetSO(cellModel.Type.Id);
            ICellPresenter cellPresenter = cellPresenterFactory.Create(cellModel, cellView, cellTypeSO, allCellTypes);
            cellPresenter.Enable();
            cells[cellModel] = cellView;
        }
    }
}