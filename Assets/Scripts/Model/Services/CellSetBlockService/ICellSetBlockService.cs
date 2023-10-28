using Model.Objects;
using System;

namespace Model.Services
{
    public interface ICellSetBlockService
    {
        event Action<Cell> OnEmpty;

        /// <summary>
        /// Поместить блок в клетку при возможности, null для опустошения клетки
        /// </summary>
        void SetBlock(Cell cell, Block block);
        void SetEmpty(Cell cell);
    }
}