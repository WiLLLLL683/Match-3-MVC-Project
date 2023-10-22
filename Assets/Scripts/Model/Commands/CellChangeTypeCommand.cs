using Model.Objects;
using Model.Services;

namespace Model.Commands
{
    /// <summary>
    /// смена типа клетки
    /// </summary>
    public class CellChangeTypeCommand : ICommand
    {
        private readonly Cell cell;
        private readonly CellType targetType;
        private readonly CellType previousType;
        private readonly ICellChangeTypeService cellChangeTypeService;

        public CellChangeTypeCommand(Cell cell, CellType targetType, ICellChangeTypeService cellChangeTypeService)
        {
            this.cell = cell;
            this.targetType = targetType;
            this.cellChangeTypeService = cellChangeTypeService;
            previousType = cell.Type;
        }

        public void Execute() => cellChangeTypeService.ChangeType(cell, targetType);
        public void Undo() => cellChangeTypeService.ChangeType(cell, previousType);
    }
}