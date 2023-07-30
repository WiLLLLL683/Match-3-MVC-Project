using Data;
using Model.Infrastructure;
using Model.Objects;
using Model.Readonly;
using Presenter;
using UnityEngine;
using Utils;
using View;

public class MetaGameState : IState
{
    //зависимости
    private readonly Game game;
    private readonly PrefabConfig prefabs;
    private readonly Bootstrap bootstrap;

    //экраны
    private ALevelSelectionScreen levelSelectionScreen;
    private ABackgroundScreen backgroundScreen;
    private AHeaderScreen headerScreen;

    //фабрики игровых элементов
    private AFactory<CurrencyInventory, ICounterView, ICurrencyPresenter> scoreFactory;
    private AFactory<ILevelSelection_Readonly, ASelectorView, ISelectorPresenter> selectorFactory;

    public MetaGameState(Game game, PrefabConfig prefabs, Bootstrap bootstrap)
    {
        this.game = game;
        this.prefabs = prefabs;
        this.bootstrap = bootstrap;
    }

    public void OnStart()
    {
        //TODO game.StartMetaGame();

        //создание фабрик игровых элементов
        scoreFactory = new CurrencyPresenter.Factory(prefabs.scorePrefab);
        selectorFactory = new LevelSelectorPresenter.Factory(prefabs.selectorPrefab, bootstrap);

        //создание экранов
        levelSelectionScreen = ALevelSelectionScreen.Create(prefabs.levelSelectionPrefab, game.LevelSelection, selectorFactory);
        headerScreen = AHeaderScreen.Create(prefabs.headerPrefab, game.CurrencyInventory, scoreFactory);
        backgroundScreen = ABackgroundScreen.Create(prefabs.backgroundPrefab);
    }

    public void OnEnd()
    {
        //levelSelectionScreen.Disable();
        //headerScreen.Disable();
        //backgroundScreen.gameObject.SetActive(false);

        GameObject.Destroy(levelSelectionScreen.gameObject);
        GameObject.Destroy(backgroundScreen.gameObject);
        GameObject.Destroy(headerScreen.gameObject);
    }
}
