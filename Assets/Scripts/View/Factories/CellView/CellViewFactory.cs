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

        public CellViewFactory(IInstantiator instantiator,
            IGameBoardView gameBoardView,
            ICellTypeConfigProvider configProvider)
        {
            this.instantiator = instantiator;
            this.gameBoardView = gameBoardView;
            this.configProvider = configProvider;
        }

        public ICellView Create(Cell model)
        {
            CellTypeSO config = configProvider.GetSO(model.Type.Id);
            CellView view = instantiator.InstantiatePrefabForComponent<CellView>(config.prefab, gameBoardView.CellsParent);
            view.Init(model.Position, config.icon, model.Type.IsPlayable, config.destroyEffect, config.emptyEffect);
            return view;
        }
    }
}