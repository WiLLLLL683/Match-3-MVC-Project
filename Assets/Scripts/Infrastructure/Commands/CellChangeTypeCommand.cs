using Model.Objects;
using Model.Services;

namespace Infrastructure.Commands
{
    /// <summary>
    /// смена типа клетки
    /// </summary>
    public class CellChangeTypeCommand : CommandBase
    {
        private readonly Cell cell;
        private readonly CellType targetType;
        private readonly CellType previousType;
        private readonly ICellChangeTypeService cellChangeTypeService;

        public CellChangeTypeCommand(Cell cell, CellType targetType, ICellChangeTypeService cellChangeTypeService)
        {
            if (cell == null ||
                targetType == null ||
                cellChangeTypeService == null ||
                cell.Type == null)
            {
                return;
            }

            this.cell = cell;
            this.targetType = targetType;
            this.cellChangeTypeService = cellChangeTypeService;
            previousType = cell.Type;
            isValid = true;
        }

        protected override void OnExecute()
        {
            cellChangeTypeService.ChangeType(cell, targetType);
            isExecuted = true;
        }
        protected override void OnUndo() => cellChangeTypeService.ChangeType(cell, previousType);
    }
}