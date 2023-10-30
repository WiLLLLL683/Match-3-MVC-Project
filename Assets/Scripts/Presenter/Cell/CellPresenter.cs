using Config;
using Model.Objects;
using Model.Services;
using UnityEngine;
using Utils;
using View;

namespace Presenter
{
    public class CellPresenter : ICellPresenter
    {
        /// <summary>
        /// Реализация фабрики использующая класс презентера в котором находится.
        /// </summary>
        public class Factory : AFactory<Cell, ACellView, ICellPresenter>
        {
            private readonly CellTypeSetSO allCellTypeSO;
            private readonly ICellSetBlockService setBlockService;
            private readonly ICellChangeTypeService changeTypeService;
            private readonly ICellDestroyService cellDestroyService;

            public Factory(ACellView viewPrefab, CellTypeSetSO allCellTypeSO, ICellSetBlockService setBlockService, ICellChangeTypeService changeTypeService, ICellDestroyService cellDestroyService, Transform parent = null) : base(viewPrefab)
            {
                this.allCellTypeSO = allCellTypeSO;
                this.setBlockService = setBlockService;
                this.changeTypeService = changeTypeService;
                this.cellDestroyService = cellDestroyService;
            }

            public override ICellPresenter Connect(ACellView existingView, Cell model)
            {
                CellTypeSO cellTypeSO = allCellTypeSO.GetSO(model.Type.Id);
                ICellPresenter presenter = new CellPresenter(model, existingView, cellTypeSO, allCellTypeSO, setBlockService, changeTypeService, cellDestroyService);
                existingView.Init(model.Position, cellTypeSO.icon, model.Type.IsPlayable,
                    cellTypeSO.destroyEffect, cellTypeSO.emptyEffect);
                allPresenters.Add(presenter);
                presenter.Enable();
                return presenter;
            }
        }

        private readonly Cell model;
        private readonly ACellView view;
        private readonly CellTypeSetSO allCellTypeSO;
        private readonly ICellSetBlockService setBlockService;
        private readonly ICellChangeTypeService changeTypeService;
        private readonly ICellDestroyService cellDestroyService;
        private CellTypeSO typeSO;

        public CellPresenter(Cell model, ACellView view, CellTypeSO typeSO, CellTypeSetSO allCellTypeSO, ICellSetBlockService setBlockService, ICellChangeTypeService changeTypeService, ICellDestroyService cellDestroyService)
        {
            this.model = model;
            this.view = view;
            this.typeSO = typeSO;
            this.allCellTypeSO = allCellTypeSO;
            this.setBlockService = setBlockService;
            this.changeTypeService = changeTypeService;
            this.cellDestroyService = cellDestroyService;
        }

        public void Enable()
        {
            cellDestroyService.OnDestroy += Destroy;
            setBlockService.OnEmpty += Empty;
            changeTypeService.OnTypeChange += ChangeType;
        }
        public void Disable()
        {
            cellDestroyService.OnDestroy -= Destroy;
            setBlockService.OnEmpty -= Empty;
            changeTypeService.OnTypeChange -= ChangeType;
        }
        public void Destroy() => Destroy(model);

        private void Destroy(Cell cell)
        {
            Disable();
            view.PlayDestroyEffect();
            GameObject.Destroy(view.gameObject);
        }
        private void ChangeType(Cell model)
        {
            if (this.model != model)
                return;

            typeSO = allCellTypeSO.GetSO(model.Type.Id);
            view.ChangeType(typeSO.icon, model.Type.IsPlayable, typeSO.destroyEffect, typeSO.emptyEffect);
        }
        private void Empty(Cell model)
        {
            if (this.model != model)
                return;

            view.PlayEmptyEffect();
        }
    }
}