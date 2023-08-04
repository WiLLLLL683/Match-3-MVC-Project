using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Model.Readonly;
using View;
using Utils;

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
            public Factory(AGameBoardView viewPrefab,
                AFactory<IBlock_Readonly, ABlockView, IBlockPresenter> blockFactory,
                AFactory<ICell_Readonly, ACellView, ICellPresenter> cellFactory) : base(viewPrefab)
            {
                this.blockFactory = blockFactory;
                this.cellFactory = cellFactory;
            }

            public override IGameBoardPresenter Connect(AGameBoardView existingView, IGameBoard_Readonly model)
            {
                var presenter = new GameBoardPresenter(model, existingView, blockFactory, cellFactory);
                presenter.Enable();
                allPresenters.Add(presenter);
                return presenter;
            }
        }

        private readonly IGameBoard_Readonly model;
        private readonly AGameBoardView view;
        private readonly AFactory<IBlock_Readonly, ABlockView, IBlockPresenter> blockFactory;
        private readonly AFactory<ICell_Readonly, ACellView, ICellPresenter> cellFactory;

        private readonly Dictionary<ICell_Readonly, ACellView> cells = new();
        private readonly Dictionary<IBlock_Readonly, ABlockView> blocks = new();

        public GameBoardPresenter(IGameBoard_Readonly model, AGameBoardView view,
            AFactory<IBlock_Readonly, ABlockView, IBlockPresenter> blockFactory,
            AFactory<ICell_Readonly, ACellView, ICellPresenter> cellFactory)
        {
            this.model = model;
            this.view = view;
            this.blockFactory = blockFactory;
            this.cellFactory = cellFactory;

            this.blockFactory.SetParent(view.BlocksParent);
            this.cellFactory.SetParent(view.CellsParent);
        }

        public void Enable()
        {
            SpawnCells();
            SpawnBlocks();
            //TODO спавн блоков по событию в модели
            Debug.Log($"{this} enabled");
        }
        public void Disable()
        {
            ClearBlocks();
            ClearCells();
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
        private void SpawnCells()
        {
            ClearCells();

            int xLength = model.Cells_Readonly.GetLength(0);
            int yLength = model.Cells_Readonly.GetLength(1);

            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    ICell_Readonly cellModel = model.Cells_Readonly[x, y];
                    cells[cellModel] = cellFactory.Create(cellModel).View;
                }
            }
        }
        [Button]
        private void SpawnBlocks()
        {
            ClearBlocks();

            foreach (var blockModel in model.Blocks_Readonly)
            {
                blocks[blockModel] = blockFactory.Create(blockModel).View;
            }
        }
        private void ClearCells()
        {
            cellFactory.Clear();
            cellFactory.ClearParent();
            cells.Clear();
        }
        private void ClearBlocks()
        {
            blockFactory.Clear();
            blockFactory.ClearParent();
            blocks.Clear();
        }
    }
}