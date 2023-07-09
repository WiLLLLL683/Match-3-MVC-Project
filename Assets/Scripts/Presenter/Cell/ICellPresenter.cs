using Model.Objects;
using System;

namespace Presenter
{
    public interface ICellPresenter
    {
        void Init();
        void Destroy(Cell cell);
    }
}