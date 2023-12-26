using UnityEngine;
using Config;
using Model.Objects;

namespace Model.Factories
{
    public class GameBoardFactory : IGameBoardFactory
    {
        private readonly ICellFactory cellFactory;
        private readonly IConfigProvider configProvider;

        private LevelSO config;
        private Cell[,] cells;
        private int xLength;
        private int yLength;
        private int hiddenCellsStartIndex;

        public GameBoardFactory(ICellFactory cellFactory, IConfigProvider configProvider)
        {
            this.cellFactory = cellFactory;
            this.configProvider = configProvider;
        }

        public GameBoard Create(LevelSO config)
        {
            this.config = config;
            xLength = config.gameBoard.GridSize.x;
            yLength = config.gameBoard.GridSize.y + config.rowsOfHiddenCells;
            hiddenCellsStartIndex = yLength - config.rowsOfHiddenCells;
            cells = new Cell[xLength, yLength];
            CreateHiddenCells();
            CreateCells();

            return new GameBoard(cells, hiddenCellsStartIndex);
        }

        private void CreateHiddenCells()
        {
            for (int y = hiddenCellsStartIndex; y < yLength; y++)
            {
                for (int x = 0; x < xLength; x++)
                {
                    cells[x, y] = cellFactory.CreateHidden(new Vector2Int(x, y));
                }
            }
        }

        private void CreateCells()
        {
            for (int y = 0; y < hiddenCellsStartIndex; y++)
            {
                for (int x = 0; x < xLength; x++)
                {
                    int cellTypeId = config.gameBoard.GetCell(x, y);
                    CellType type = configProvider.GetCellTypeSO(cellTypeId).type;
                    cells[x, y] = cellFactory.Create(new(x, y), type);
                }
            }
        }
    }
}