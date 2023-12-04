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
        private readonly IConfigProvider configProvider;

        public CellViewFactory(IInstantiator instantiator,
            IGameBoardView gameBoardView,
            IConfigProvider configProvider)
        {
            this.instantiator = instantiator;
            this.gameBoardView = gameBoardView;
            this.configProvider = configProvider;
        }

        public ICellView Create(Cell model)
        {
            CellTypeSO config = configProvider.GetCellTypeSO(model.Type.Id);
            CellView view = instantiator.InstantiatePrefabForComponent<CellView>(config.prefab, gameBoardView.CellsParent);
            view.Init(model.Position, config.icon, model.Type.IsPlayable, config.destroyEffect, config.emptyEffect);
            return view;
        }
    }
}