using Model.Readonly;

namespace Presenter
{
    public interface ICellPresenter : IPresenter
    {
        void Destroy(ICell_Readonly cell);
    }
}