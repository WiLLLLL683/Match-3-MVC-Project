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
    private ABackgroundScreen backgroundScreen;
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
        //TODO game.StartMetaGame();

        //создание фабрик игровых элементов
        scoreFactory = new CurrencyFactory(prefabs.scorePrefab);
        selectorFactory = new LevelSelectorFactory(prefabs.selectorPrefab, bootstrap);

        //создание окон вью
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
