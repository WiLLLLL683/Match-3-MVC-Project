using Config;
using Model.Infrastructure;
using Model.Objects;
using Model.Services;
using Presenter;
using UnityEngine;
using Utils;
using View;
using Zenject;

public class MetaGameState : IState
{
    ////зависимости
    //private readonly Game game;
    //private readonly PrefabConfig prefabs;
    //private readonly Bootstrap bootstrap;
    //private readonly LevelSO[] allLevels;

    ////экраны
    //private ILevelSelectionPresenter levelSelectionScreen;
    //private Canvas backgroundScreen;
    //private IHeaderPresenter headerScreen;

    ////фабрики игровых элементов
    //private AFactory<ICurrencyService, ACounterView, ICurrencyPresenter> scoreFactory;
    //private AFactory<LevelProgress, ALevelSelectionView, ILevelSelectionPresenter> selectorFactory;

    //public MetaGameState(Game game, PrefabConfig prefabs, LevelSO[] allLevels, Bootstrap bootstrap)
    //{
    //    this.game = game;
    //    this.prefabs = prefabs;
    //    this.bootstrap = bootstrap;
    //    this.allLevels = allLevels;
    //}

    public void OnStart()
    {
        ////TODO game.StartMetaGame();

        ////создание фабрик игровых элементов
        //scoreFactory = new CurrencyPresenter.Factory(prefabs.scorePrefab);
        //selectorFactory = new LevelSelectorPresenter.Factory(prefabs.selectorPrefab, bootstrap, allLevels);

        ////создание фабрик экранов
        //var headerFactory = new HeaderPresenter.Factory(prefabs.headerPrefab, scoreFactory);
        //var levelSelectionFactory = new LevelSelectionPresenter.Factory(prefabs.levelSelectionPrefab, selectorFactory);

        ////создание экранов
        //levelSelectionScreen = levelSelectionFactory.Create(game.LevelProgress).Presenter;
        //headerScreen = headerFactory.Create(game.currencyService).Presenter;
        //backgroundScreen = GameObject.Instantiate(prefabs.backgroundPrefab);
    }

    public void OnEnd()
    {
        //levelSelectionScreen.Destroy();
        //headerScreen.Destroy();
        //GameObject.Destroy(backgroundScreen.gameObject);

        ////GameObject.Destroy(levelSelectionScreen.gameObject);
        ////GameObject.Destroy(backgroundScreen.gameObject);
        ////GameObject.Destroy(headerScreen.gameObject);
    }
}
