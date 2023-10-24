using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Services
{
    public class CellDestroyService : ICellDestroyService
    {
        public event Action<Cell> OnDestroy;

        /// <summary>
        /// Уничтожить материал клетки, результат зависит от типа клетки
        /// </summary>
        public void Destroy(Cell cell)
        {
            if (cell == null)
                return;

            if (cell.Type == null)
                return;

            cell.Type.DestroyCellMaterial();
            OnDestroy?.Invoke(cell);
        }
    }
}