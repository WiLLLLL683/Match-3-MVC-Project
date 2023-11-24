using Config;
using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace View.Factories
{
    public class CellViewFactory : ICellViewFactory
    {
        private readonly IInstantiator instantiator;
        private readonly IGameBoardView gameBoardView;
        private readonly ICellTypeConfigProvider configProvider;
        private readonly CellView cellPrefab;
        private readonly CellView invisibleCellPrefab;

        public CellViewFactory(IInstantiator instantiator,
            IGameBoardView gameBoardView,
            ICellTypeConfigProvider configProvider,
            CellView cellPrefab,
            CellView invisibleCellPrefab)
        {
            this.instantiator = instantiator;
            this.gameBoardView = gameBoardView;
            this.configProvider = configProvider;
            this.cellPrefab = cellPrefab;
            this.invisibleCellPrefab = invisibleCellPrefab;
        }

        public ICellView Create(Cell model)
        {
            CellView view = instantiator.InstantiatePrefabForComponent<CellView>(cellPrefab, gameBoardView.CellsParent);
            InitView(view, model);
            return view;
        }

        public ICellView CreateInvisible(Cell model)
        {
            CellView view = instantiator.InstantiatePrefabForComponent<CellView>(invisibleCellPrefab, gameBoardView.CellsParent);
            InitView(view, model);
            return view;
        }

        private void InitView(ICellView view, Cell model)
        {
            CellTypeSO SO = configProvider.GetSO(model.Type.Id);
            view.Init(model.Position, SO.icon, model.Type.IsPlayable, SO.destroyEffect, SO.emptyEffect);
        }
    }
}