using Model.Readonly;
using View;
using Data;

namespace Presenter
{
    public class CellPresenter : ICellPresenter
    {
        private ICell_Readonly model;
        private ICellView view;

        public CellPresenter(ICell_Readonly model, ICellView view)
        {
            this.model = model;
            this.view = view;
        }

        public void Init()
        {
            model.OnDestroy += Destroy;
            model.OnEmpty += Empty;
            model.OnTypeChange += ChangeType;
        }
        public void Destroy(ICell_Readonly cell)
        {
            view.PlayDestroyEffect();

            model.OnDestroy -= Destroy;
            model.OnEmpty -= Empty;
            model.OnTypeChange -= ChangeType;
        }



        private void ChangeType(ACellType type) => view.ChangeType(type);
        private void Empty(ICell_Readonly cell) => view.PlayEmptyEffect();
    }
}