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

        public void Enable()
        {
            model.OnDestroy += Destroy;
            model.OnEmpty += Empty;
            model.OnTypeChange += ChangeType;
        }
        public void Disable()
        {
            model.OnDestroy -= Destroy;
            model.OnEmpty -= Empty;
            model.OnTypeChange -= ChangeType;
        }
        public void Destroy() => Destroy(model);



        private void Destroy(ICell_Readonly cell)
        {
            Disable();
            view.PlayDestroyEffect();
        }
        private void ChangeType(ACellType type) => view.ChangeType(type);
        private void Empty(ICell_Readonly cell) => view.PlayEmptyEffect();
    }
}