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
        private readonly ICellTypeConfigProvider allCellTypes;
        private readonly ICellSetBlockService setBlockService;
        private readonly ICellChangeTypeService changeTypeService;
        private readonly ICellDestroyService cellDestroyService;

        private readonly Dictionary<Cell, ICellView> cells = new();

        private GameBoard gameBoard;

        public CellsPresenter(Game model,
            IGameBoardView view,
            ICellViewFactory cellViewFactory,
            ICellTypeConfigProvider allCellTypes,
            ICellSetBlockService setBlockService,
            ICellChangeTypeService changeTypeService,
            ICellDestroyService cellDestroyService)
        {
            this.model = model;
            this.view = view;
            this.cellViewFactory = cellViewFactory;
            this.allCellTypes = allCellTypes;
            this.setBlockService = setBlockService;
            this.changeTypeService = changeTypeService;
            this.cellDestroyService = cellDestroyService;
        }

        public void Enable()
        {
            gameBoard = model.CurrentLevel.gameBoard;
            SpawnAllCells();

            cellDestroyService.OnDestroy += Destroy;
            setBlockService.OnEmpty += Empty;
            changeTypeService.OnTypeChange += ChangeType;

            Debug.Log($"{this} enabled");
        }

        public void Disable()
        {
            cellDestroyService.OnDestroy -= Destroy;
            setBlockService.OnEmpty -= Empty;
            changeTypeService.OnTypeChange -= ChangeType;

            Debug.Log($"{this} disabled");
        }

        public ICellView GetCellView(Vector2Int modelPosition)
        {
            Cell cellModel = gameBoard.Cells[modelPosition.x, modelPosition.y];
            return cells[cellModel];
        }

        [Button]
        private void SpawnAllCells()
        {
            ClearAllCells();

            for (int y = 0; y < gameBoard.Cells.GetLength(1); y++)
            {
                for (int x = 0; x < gameBoard.Cells.GetLength(0); x++)
                {
                    SpawnCell(y, x);
                }
            }
        }

        private void ClearAllCells()
        {
            foreach (Transform cell in view.CellsParent)
            {
                GameObject.Destroy(cell.gameObject);
            }

            cells.Clear();
        }

        private void SpawnCell(int y, int x)
        {
            Cell cellModel = gameBoard.Cells[x, y];
            cells[cellModel] = cellViewFactory.Create(cellModel);
        }

        private void Destroy(Cell model)
        {
            if (!cells.ContainsKey(model))
                return;

            var cellView = cells[model];
            cellView.PlayDestroyEffect();
        }

        private void ChangeType(Cell model)
        {
            if (!cells.ContainsKey(model))
                return;

            var cellView = cells[model];
            CellTypeSO typeSO = allCellTypes.GetSO(model.Type.Id);
            cellView.ChangeType(typeSO.icon, model.Type.IsPlayable, typeSO.destroyEffect, typeSO.emptyEffect);
        }

        private void Empty(Cell model)
        {
            if (!cells.ContainsKey(model))
                return;

            var cellView = cells[model];
            cellView.PlayEmptyEffect();
        }
    }
}