using UnityEngine;
using Config;
using Model.Objects;

namespace Model.Factories
{
    public class GameBoardFactory : IGameBoardFactory
    {
        private readonly ICellFactory cellFactory;
        private readonly CellTypeSetSO allCellTypes;

        private LevelSO config;
        private Cell[,] cells;
        private int xLength;
        private int yLength;

        public GameBoardFactory(ICellFactory cellFactory, CellTypeSetSO allCellTypes)
        {
            this.cellFactory = cellFactory;
            this.allCellTypes = allCellTypes;
        }

        public GameBoard Create(LevelSO config)
        {
            this.config = config;

            xLength = config.gameBoard.GridSize.x;
            yLength = config.gameBoard.GridSize.y + config.rowsOfInvisibleCells;
            cells = new Cell[xLength, yLength];
            CreateInvisibleCells();
            CreateCells();

            return new GameBoard(cells, config.rowsOfInvisibleCells);
        }

        private void CreateInvisibleCells()
        {
            //спавн невидимых клеток
            for (int y = 0; y < config.rowsOfInvisibleCells; y++)
            {
                for (int x = 0; x < xLength; x++)
                {
                    cells[x, y] = cellFactory.CreateInvisible(new Vector2Int(x, y));
                }
            }
        }

        private void CreateCells()
        {
            //спавн обычных клеток после рядов невидимых клеток
            for (int y = config.rowsOfInvisibleCells; y < yLength; y++)
            {
                for (int x = 0; x < xLength; x++)
                {
                    var type = GetCellTypeFromConfig(x, y);
                    cells[x, y] = cellFactory.Create(new(x, y), type);
                }
            }
        }

        private CellType GetCellTypeFromConfig(int x, int y)
        {
            //сдвиг на количество невидимых рядов, в конфиге они заданы отдельно, а не в 2д массиве
            y -= config.rowsOfInvisibleCells;

            int cellTypeId = config.gameBoard.GetCell(x, y);

            return allCellTypes.GetSO(cellTypeId).type;
        }
    }
}