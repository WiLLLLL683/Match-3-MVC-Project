using Model.Readonly;
using View;
using UnityEngine;
using Utils;
using Config;

namespace Presenter
{
    public class CellPresenter : ICellPresenter
    {
        /// <summary>
        /// Реализация фабрики использующая класс презентера в котором находится.
        /// </summary>
        public class Factory : AFactory<ICell_Readonly, ACellView, ICellPresenter>
        {
            private readonly AllCellTypeSO allCellTypeSO;

            public Factory(ACellView viewPrefab, AllCellTypeSO allCellTypeSO, Transform parent = null) : base(viewPrefab)
            {
                this.allCellTypeSO = allCellTypeSO;
            }

            public override ICellPresenter Connect(ACellView existingView, ICell_Readonly model)
            {
                CellTypeSO cellTypeSO = allCellTypeSO.GetSO(model.Type_Readonly.Id);
                ICellPresenter presenter = new CellPresenter(model, existingView, cellTypeSO, allCellTypeSO);
                existingView.Init(model.Position, cellTypeSO.icon, model.Type_Readonly.IsPlayable,
                    cellTypeSO.destroyEffect, cellTypeSO.emptyEffect);
                allPresenters.Add(presenter);
                presenter.Enable();
                return presenter;
            }
        }
        
        private readonly ICell_Readonly model;
        private readonly ACellView view;
        private readonly AllCellTypeSO allCellTypeSO;
        private CellTypeSO typeSO;

        public CellPresenter(ICell_Readonly model, ACellView view, CellTypeSO typeSO, AllCellTypeSO allCellTypeSO)
        {
            this.model = model;
            this.view = view;
            this.typeSO = typeSO;
            this.allCellTypeSO = allCellTypeSO;
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
        private void ChangeType(ICellType_Readonly type)
        {
            typeSO = allCellTypeSO.GetSO(type.Id);
            view.ChangeType(typeSO.icon, type.IsPlayable, typeSO.destroyEffect, typeSO.emptyEffect);
        }
        private void Empty(ICell_Readonly cell) => view.PlayEmptyEffect();
    }
}