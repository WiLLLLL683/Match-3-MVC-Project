using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Services
{
    public class CellChangeTypeService : ICellChangeTypeService
    {
        public event Action<Cell> OnTypeChange;

        public void ChangeType(Cell cell, CellType type)
        {
            if (cell == null || type == null)
                return;

            cell.Type = type;
            OnTypeChange?.Invoke(cell);
        }
    }
}