using Model.Readonly;

namespace Presenter
{
    public interface ICellPresenter
    {
        void Init();
        void Destroy(ICell_Readonly cell);
    }
}