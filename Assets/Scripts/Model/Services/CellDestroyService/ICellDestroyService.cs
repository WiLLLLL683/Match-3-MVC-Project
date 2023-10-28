using Model.Objects;
using System;

namespace Model.Services
{
    public interface ICellDestroyService
    {
        event Action<Cell> OnDestroy;

        void Destroy(Cell cell);
    }
}