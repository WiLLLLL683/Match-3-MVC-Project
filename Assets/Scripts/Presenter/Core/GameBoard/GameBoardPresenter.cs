using Config;
using Model.Objects;
using Model.Services;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;
using View;
using Zenject;

namespace Presenter
{
    /// <summary>
    /// Презентер игрового поля
    /// Создает визуальные элементы клеток и блоков, а также синхронизирует их с моделью
    /// </summary>
    public class GameBoardPresenter : IGameBoardPresenter
    {
        private readonly GameBoard model;
        private readonly IGameBoardView view;
        private readonly IBlockSpawnService blockSpawnService;
        private readonly BlockPresenter.Factory blockPresenterFactory;
        private readonly BlockView.Factory blockViewFactory;
        private readonly CellPresenter.Factory cellPresenterFactory;
        private readonly CellView.Factory cellViewFactory;
        private readonly CellView.Factory notPlayableCellViewFactory;
        private readonly CellTypeSetSO allCellTypes;
        private readonly BlockTypeSetSO blockTypeSet;

        private readonly Dictionary<Cell, ICellView> cells = new();
        private readonly Dictionary<Block, IBlockView> blocks = new();

        public GameBoardPresenter(GameBoard model,
            IGameBoardView view,
            IBlockSpawnService blockSpawnService,
            BlockPresenter.Factory blockPresenterFactory,
            BlockView.Factory blockViewFactory,
            CellPresenter.Factory cellPresenterFactory,
            [Inject(Id = "cellViewFactory")] CellView.Factory cellViewFactory,
            [Inject(Id = "notPlayableCellViewFactory")] CellView.Factory notPlayableCellViewFactory,
            CellTypeSetSO allCellTypes,
            BlockTypeSetSO blockTypeSet)
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

        public ICellView GetCellView(Vector2Int modelPosition)
        {
            Cell cellModel = model.Cells[modelPosition.x, modelPosition.y];
            return cells[cellModel];
        }

        public IBlockView GetBlockView(Vector2Int modelPosition)
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
                        ICellView cellView = cellViewFactory.Create();
                        CreateCellPresenter(cellModel, cellView);
                    }
                    else
                    {
                        ICellView cellView = notPlayableCellViewFactory.Create();
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
            IBlockView blockView = blockViewFactory.Create();
            BlockTypeSO blockTypeSO = blockTypeSet.GetSO(blockModel.Type.Id);
            IBlockPresenter blockPresenter = blockPresenterFactory.Create(blockModel, blockView, blockTypeSO, blockTypeSet);
            blockPresenter.Enable();
            blocks.Add(blockModel, blockView);
        }

        private void CreateCellPresenter(Cell cellModel, ICellView cellView)
        {
            CellTypeSO cellTypeSO = allCellTypes.GetSO(cellModel.Type.Id);
            ICellPresenter cellPresenter = cellPresenterFactory.Create(cellModel, cellView, cellTypeSO, allCellTypes);
            cellPresenter.Enable();
            cells[cellModel] = cellView;
        }
    }
}