using Model.Objects;

namespace View.Factories
{
    public interface ICellViewFactory
    {
        ICellView Create(Cell model);
    }
}