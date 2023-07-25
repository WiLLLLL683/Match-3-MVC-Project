using Data;
using Model.Infrastructure;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Presenter
{
    public class MetaGameFactory
    {
        private Game game;
        private PrefabConfig prefabs;
        private Bootstrap bootstrap;

        private ILevelSelectionController levelSelection;
        private Canvas background;
        private Canvas header;

        public MetaGameFactory(Game game, PrefabConfig prefabs, Bootstrap bootstrap)
        {
            this.game = game;
            this.prefabs = prefabs;
            this.bootstrap = bootstrap;
        }

        public void Create()
        {
            //создание окон вью
            levelSelection = (ILevelSelectionController)GameObject.Instantiate(prefabs.levelSelectionPrefab.UnderlyingValue);
            background = GameObject.Instantiate(prefabs.backgroundPrefab);
            header = GameObject.Instantiate(prefabs.headerPrefab);
            //инициализация
            levelSelection.Init(game, bootstrap);

            //game.StartMetaGame();
        }
        public void Clear()
        {
            GameObject.Destroy(levelSelection.gameObject);
            GameObject.Destroy(background.gameObject);
            GameObject.Destroy(header.gameObject);
        }
    }
}