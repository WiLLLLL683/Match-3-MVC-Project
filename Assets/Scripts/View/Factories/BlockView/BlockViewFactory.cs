using Config;
using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace View.Factories
{
    public class BlockViewFactory : IBlockViewFactory
    {
        private readonly IInstantiator instantiator;
        private readonly IGameBoardView gameBoardView;
        private readonly IConfigProvider configProvider;

        public BlockViewFactory(IInstantiator instantiator, IGameBoardView gameBoardView, IConfigProvider configProvider)
        {
            this.instantiator = instantiator;
            this.gameBoardView = gameBoardView;
            this.configProvider = configProvider;
        }

        public IBlockView Create(Block model)
        {
            BlockTypeSO config = configProvider.GetBlockTypeSO(model.Type.Id);
            BlockView view = instantiator.InstantiatePrefabForComponent<BlockView>(configProvider.Prefabs.blockViewPrefab, gameBoardView.BlocksParent);
            view.Init(config.icon, config.typeConfig, model.Position);
            return view;
        }
    }
}