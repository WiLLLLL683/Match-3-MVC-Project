using Config;
using Model.Objects;
using Model.Services;
using UnityEngine;
using View;
using Zenject;

namespace Presenter
{
    public class CellPresenter : ICellPresenter
    {
        public class Factory : PlaceholderFactory<Cell, ACellView, CellTypeSO, CellTypeSetSO, CellPresenter> { }

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

            view.Init(model.Position, typeSO.icon, model.Type.IsPlayable, typeSO.destroyEffect, typeSO.emptyEffect);
            Debug.Log($"{this} enabled");
        }

        public void Disable()
        {
            cellDestroyService.OnDestroy -= Destroy;
            setBlockService.OnEmpty -= Empty;
            changeTypeService.OnTypeChange -= ChangeType;
            Debug.Log($"{this} disabled");
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