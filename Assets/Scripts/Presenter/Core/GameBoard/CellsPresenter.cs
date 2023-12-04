using Config;
using Model.Objects;
using Model.Services;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;
using View;
using View.Factories;

namespace Presenter
{
    /// <summary>
    /// Презентер клеток игрового поля
    /// Создает визуальные элементы клеток, а также синхронизирует их с моделью
    /// </summary>
    public class CellsPresenter : ICellsPresenter
    {
        private readonly Game model;
        private readonly IGameBoardView view;
        private readonly ICellViewFactory cellViewFactory;
        private readonly ICellTypeConfigProvider configProvider;
        private readonly ICellSetBlockService setBlockService;
        private readonly ICellChangeTypeService changeTypeService;
        private readonly ICellDestroyService cellDestroyService;
        private readonly IValidationService validationService;

        private readonly Dictionary<Cell, ICellView> cells = new();

        private GameBoard gameBoard;

        public CellsPresenter(Game model,
            IGameBoardView view,
            ICellViewFactory cellViewFactory,
            ICellTypeConfigProvider configProvider,
            ICellSetBlockService setBlockService,
            ICellChangeTypeService changeTypeService,
            ICellDestroyService cellDestroyService,
            IValidationService validationService)
        {
            this.model = model;
            this.view = view;
            this.cellViewFactory = cellViewFactory;
            this.configProvider = configProvider;
            this.setBlockService = setBlockService;
            this.changeTypeService = changeTypeService;
            this.cellDestroyService = cellDestroyService;
            this.validationService = validationService;
        }

        public void Enable()
        {
            gameBoard = model.CurrentLevel.gameBoard;
            SpawnAll();

            cellDestroyService.OnDestroy += Destroy;
            setBlockService.OnEmpty += Empty;
            changeTypeService.OnTypeChange += ChangeType;

            Debug.Log($"{this} enabled");
        }

        public void Disable()
        {
            ClearAll();

            cellDestroyService.OnDestroy -= Destroy;
            setBlockService.OnEmpty -= Empty;
            changeTypeService.OnTypeChange -= ChangeType;

            Debug.Log($"{this} disabled");
        }

        public ICellView GetCellView(Vector2Int modelPosition)
        {
            if (!validationService.CellExistsAt(modelPosition))
                return null;

            Cell model = gameBoard.Cells[modelPosition.x, modelPosition.y];
            if (!cells.ContainsKey(model))
                return null;

            return cells[model];
        }

        private void SpawnAll()
        {
            ClearAll();

            for (int y = 0; y < gameBoard.Cells.GetLength(1); y++)
            {
                for (int x = 0; x < gameBoard.Cells.GetLength(0); x++)
                {
                    Spawn(y, x);
                }
            }
        }

        private void ClearAll()
        {
            foreach (Transform cell in view.CellsParent)
            {
                GameObject.Destroy(cell.gameObject);
            }

            cells.Clear();
        }

        private void Spawn(int y, int x)
        {
            Cell model = gameBoard.Cells[x, y];
            ICellView view = cellViewFactory.Create(model);
            cells.Add(model, view);
        }

        private void Destroy(Cell model)
        {
            if (!cells.ContainsKey(model))
                return;

            ICellView view = cells[model];
            view.PlayDestroyEffect();
        }

        private void ChangeType(Cell model)
        {
            if (!cells.ContainsKey(model))
                return;

            ICellView view = cells[model];
            CellTypeSO config = configProvider.GetSO(model.Type.Id);
            view.ChangeType(config.icon, model.Type.IsPlayable, config.destroyEffect, config.emptyEffect);
        }

        private void Empty(Cell model)
        {
            if (!cells.ContainsKey(model))
                return;

            ICellView view = cells[model];
            view.PlayEmptyEffect();
        }
    }
}