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
    private Game game;
    private PrefabConfig prefabs;
    private Bootstrap bootstrap;

    private ALevelSelectionScreen levelSelectionScreen;
    private Canvas backgroundScreen;
    private AHeaderScreen headerScreen;

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
        //создание фабрик игровых элементов
        scoreFactory = new CurrencyFactory(prefabs.scorePrefab);
        selectorFactory = new LevelSelectorFactory(prefabs.selectorPrefab, bootstrap);

        //создание окон вью
        levelSelectionScreen = CreateLevelSelectionScreen(game.LevelSelection, selectorFactory);
        headerScreen = CreateHeaderScreen(game.CurrencyInventory, scoreFactory);
        backgroundScreen = CreateBackgroundScreen();

        //TODO game.StartMetaGame();
    }

    private Canvas CreateBackgroundScreen()
    {
        var backgroundScreen = GameObject.Instantiate(prefabs.backgroundPrefab);
        backgroundScreen.gameObject.SetActive(true);
        return backgroundScreen;
    }

    private AHeaderScreen CreateHeaderScreen(CurrencyInventory currencyInventory, AFactory<CurrencyInventory, ICounterView, ICurrencyPresenter> scoreFactory)
    {
        var headerScreen = GameObject.Instantiate(prefabs.headerPrefab);
        headerScreen.Init(currencyInventory, scoreFactory);
        headerScreen.Enable();
        return headerScreen;
    }

    private ALevelSelectionScreen CreateLevelSelectionScreen(LevelSelection levelSelection, AFactory<ILevelSelection_Readonly, ASelectorView, ISelectorPresenter> selectorFactory)
    {
        var levelSelectionScreen = GameObject.Instantiate(prefabs.levelSelectionPrefab);
        levelSelectionScreen.Init(levelSelection, selectorFactory);
        levelSelectionScreen.Enable();
        return levelSelectionScreen;
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
