using Data;
using Model.Infrastructure;
using Model.Objects;
using Model.Readonly;
using Presenter;
using UnityEngine;
using Utils;
using View;

public class CoreGameState : IState
{
    //зависимости
    private readonly Game game;
    private readonly PrefabConfig prefabs;
    private readonly Bootstrap bootstrap;

    //экраны
    private IGameBoardPresenter gameBoardScreen;
    private AInput input;
    private IHudPresenter hudScreen;
    private IBoosterInventoryPresenter boosterScreen;
    private IPausePresenter pauseScreen;
    private IEndGamePresenter endGameScreen;

    //фабрики игровых элементов
    private AFactory<IBlock_Readonly, IBlockView, IBlockPresenter> blockFactory;
    private AFactory<ICell_Readonly, ICellView, ICellPresenter> cellFactory;
    private AFactory<ICounter_Readonly, ICounterView, ICounterPresenter> goalFactory;
    private AFactory<ICounter_Readonly, ICounterView, ICounterPresenter> restrictionFactory;
    private AFactory<IBooster, IBoosterView, IBoosterPresenter> boosterFactory;
    private AFactory<PlayerSettings, APausePopUp, IPopUpPresenter> pausePopUpFactory;
    private AFactory<ILevel_Readonly, AEndGamePopUp, IPopUpPresenter> endGamePopUpFactory;

    public CoreGameState(Game game, PrefabConfig prefabs, Bootstrap bootstrap)
    {
        this.game = game;
        this.prefabs = prefabs;
        this.bootstrap = bootstrap;
    }

    public void OnStart()
    {
        //запуск модели
        game.StartCoreGame(bootstrap.SelectedLevel);

        //создание фабрик игровых элементов
        blockFactory = new BlockPresenter.Factory(prefabs.blockPrefab);
        cellFactory = new CellPresenter.Factory(prefabs.cellPrefab);
        goalFactory = new CounterPresenter.Factory(prefabs.goalCounterPrefab);
        restrictionFactory = new CounterPresenter.Factory(prefabs.restrictionCounterPrefab);
        boosterFactory = new BoosterPresenter.Factory(prefabs.boosterPrefab);
        pausePopUpFactory = new PausePopUpPresenter.Factory(prefabs.pausePopUpPrefab, bootstrap);
        endGamePopUpFactory = new EndGamePopUpPresenter.Factory(prefabs.endGamePopUpPrefab, bootstrap);

        //создание инпута
        input = GameObject.Instantiate(prefabs.inputPrefab);

        //создание фабрик экранов
        var gameboardFactory = new GameBoardPresenter.Factory(prefabs.gameBoardPrefab, blockFactory, cellFactory);
        var boosterInventoryFactory = new BoosterInventoryPresenter.Factory(prefabs.boosterInventoryPrefab, boosterFactory);
        var endGameFactory = new EndGamePresenter.Factory(prefabs.endGamePrefab, input, endGamePopUpFactory);
        var hudFactory = new HudPresenter.Factory(prefabs.hudPrefab, goalFactory, restrictionFactory);
        var pauseFactory = new PausePresenter.Factory(prefabs.pausePrefab, pausePopUpFactory, input);

        //создание экранов и инпута
        gameBoardScreen = gameboardFactory.Create(game.Level.gameBoard).Presenter;
        input.Init(gameBoardScreen).Enable();
        hudScreen = hudFactory.Create(game.Level).Presenter;
        boosterScreen = boosterInventoryFactory.Create(game.BoosterInventory).Presenter;
        pauseScreen = pauseFactory.Create(game.PlayerSettings).Presenter;
        endGameScreen = endGameFactory.Create(game.Level).Presenter;
    }
    public void OnEnd()
    {
        gameBoardScreen.Destroy();
        hudScreen.Destroy();
        boosterScreen.Destroy();
        pauseScreen.Destroy();
        endGameScreen.Destroy();
        GameObject.Destroy(input.gameObject);

        //уничтожение экранов
        //GameObject.Destroy(hudScreen.gameObject);
        //GameObject.Destroy(gameBoardScreen.gameObject);
        //GameObject.Destroy(input.gameObject);
        //GameObject.Destroy(boosterInventoryScreen.gameObject);
        //GameObject.Destroy(pauseScreen.gameObject);
        //GameObject.Destroy(endGameScreen.gameObject);
    }
}
