using Model.Readonly;
using View;
using Data;
using UnityEngine;
using Utils;

namespace Presenter
{
    public class CellPresenter : ICellPresenter
    {
        /// <summary>
        /// Реализация фабрики использующая класс презентера в котором находится.
        /// </summary>
        public class Factory : AFactory<ICell_Readonly, ACellView, ICellPresenter>
        {
            public Factory(ACellView viewPrefab, Transform parent = null) : base(viewPrefab)
            {
            }

            public override ICellPresenter Connect(ACellView existingView, ICell_Readonly model)
            {
                var presenter = new CellPresenter(model, existingView);
                existingView.Init(model.Position, model.Type);
                allPresenters.Add(presenter);
                presenter.Enable();
                return presenter;
            }
        }
        
        private ICell_Readonly model;
        private ACellView view;

        public CellPresenter(ICell_Readonly model, ACellView view)
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
            GameObject.Destroy(view.gameObject);
        }
        private void ChangeType(ACellType type) => view.ChangeType(type);
        private void Empty(ICell_Readonly cell) => view.PlayEmptyEffect();
    }
}