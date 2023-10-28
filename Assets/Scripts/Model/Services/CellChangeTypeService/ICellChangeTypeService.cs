using Model.Objects;
using System;

namespace Model.Services
{
    public interface ICellChangeTypeService
    {
        event Action<Cell> OnTypeChange;

        /// <summary>
        /// Изменить тип клетки
        /// </summary>
        void ChangeType(Cell cell, CellType type);
    }
}