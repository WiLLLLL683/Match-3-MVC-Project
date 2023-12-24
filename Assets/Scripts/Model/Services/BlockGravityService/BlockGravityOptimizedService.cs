using Infrastructure.Commands;
using Model.Objects;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Model.Services
{
    public class BlockGravityOptimizedService : IBlockGravityService
    {
        private readonly Game game;
        private readonly IValidationService validationService;
        private readonly IBlockMoveService moveService;

        private GameBoard GameBoard => game.CurrentLevel.gameBoard;

        public BlockGravityOptimizedService(Game game, IValidationService validationService, IBlockMoveService moveService)
        {
            this.game = game;
            this.validationService = validationService;
            this.moveService = moveService;
        }

        public void Execute()
        {
            List<Cell> emptyCells = validationService.FindEmptyCells();

            for (int i = 0; i < emptyCells.Count; i++)
            {
                ShiftBlocksColumnDown(emptyCells[i]);
            }
        }

        public void Execute(Cell emptyCell)
        {


            //if (!validationService.CellIsEmptyAt(emptyCell.Position))
            //    return;

            //Cell cellAbove = FindBlockAbove(emptyCell.Position);

            //if (cellAbove == null)
            //    return;

            //var action = new BlockMoveCommand(emptyCell.Position, cellAbove.Position, moveService); //TODO ���������� ��������?
            //action.Execute();
        }

        private Cell FindBlockAbove(Vector2Int position)
        {
            for (int y = position.y; y >= 0; y--) //�������� ����� �����
            {
                if (validationService.CellExistsAt(new(position.x, y)) && GameBoard.Cells[position.x, y].Block != null)
                {
                    return GameBoard.Cells[position.x, y];
                }
            }

            return null;
        }

        private void ShiftBlocksColumnDown(Cell emptyCell)
        {
            int x = emptyCell.Position.x;

            for (int y = emptyCell.Position.y - 1; y >= 0; y--) //����� �����
            {
                moveService.Move(new(x,y), Directions.Down);
            }
        }
    }
}