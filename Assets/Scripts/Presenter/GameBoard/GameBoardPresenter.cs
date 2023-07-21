using NaughtyAttributes;
using UnityEngine;
using Model.Infrastructure;
using Model.Readonly;
using View;
using System.Collections.Generic;

namespace Presenter
{
    public class GameBoardPresenter : MonoBehaviour, IGameBoardPresenter
    {
        [SerializeField] private BlockSpawner blockSpawner;
        [SerializeField] private CellSpawner cellSpawner;

        private Dictionary<ICell_Readonly, ICellView> cells;
        private Dictionary<IBlock_Readonly, IBlockView> blocks;
        private Game game;
        private IGameBoard_Readonly gameBoard;

        public void Init(Game game, IGameBoard_Readonly gameBoard)
        {
            this.game = game;
            this.gameBoard = gameBoard;

            //TODO спавн блоков по событию в модели
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