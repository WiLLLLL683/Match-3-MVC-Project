using NaughtyAttributes;
using UnityEngine;
using Model.Infrastructure;
using Model.Readonly;
using View;
using System.Collections.Generic;
using Data;

namespace Presenter
{
    public class GameBoardScreen : AGameBoardScreen
    {
        [SerializeField] private Transform blocksParent;
        [SerializeField] private Transform cellsParent;

        private IGameBoard_Readonly model;
        private AFactory<IBlock_Readonly, IBlockView, IBlockPresenter> blockFactory;
        private AFactory<ICell_Readonly, ICellView, ICellPresenter> cellFactory;

        private Dictionary<ICell_Readonly, ICellView> cells = new();
        private Dictionary<IBlock_Readonly, IBlockView> blocks = new();

        public override void Init(IGameBoard_Readonly model,
                                  AFactory<IBlock_Readonly, IBlockView, IBlockPresenter> blockFactory,
                                  AFactory<ICell_Readonly, ICellView, ICellPresenter> cellFactory)
        {
            this.model = model;
            this.blockFactory = blockFactory;
            this.cellFactory = cellFactory;

            this.blockFactory.SetParent(blocksParent);
            this.cellFactory.SetParent(cellsParent);
        }
        public override void Enable()
        {
            SpawnCells();
            SpawnBlocks();
            //TODO спавн блоков по событию в модели
            Debug.Log($"{this} enabled", this);
        }
        public override void Disable()
        {
            ClearBlocks();
            ClearCells();
            Debug.Log($"{this} disabled", this);
        }
        public override ICellView GetCellView(Vector2Int modelPosition)
        {
            ICell_Readonly cellModel = model.Cells_Readonly[modelPosition.x, modelPosition.y];
            return cells[cellModel];
        }
        public override IBlockView GetBlockView(Vector2Int modelPosition)
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
                    cells[cellModel] = cellFactory.CreateView(cellModel);
                }
            }
        }
        [Button]
        private void SpawnBlocks()
        {
            ClearBlocks();

            foreach (var blockModel in model.Blocks_Readonly)
            {
                blocks[blockModel] = blockFactory.CreateView(blockModel);
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