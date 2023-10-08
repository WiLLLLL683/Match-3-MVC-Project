using Config;
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
    private readonly CellTypeSetSO allCellTypes;

    //инпут
    private AInput input;

    //экраны
    private IGameBoardPresenter gameBoardScreen;
    private IHudPresenter hudScreen;
    private IBoosterInventoryPresenter boosterScreen;
    private IPausePresenter pauseScreen;
    private IEndGamePresenter endGameScreen;

    //фабрики игровых элементов
    private AFactory<IBlock_Readonly, ABlockView, IBlockPresenter> blockFactory;
    private AFactory<ICell_Readonly, ACellView, ICellPresenter> cellFactory;
    private AFactory<ICell_Readonly, ACellView, ICellPresenter> invisibleCellFactory;
    private AFactory<ICounter_Readonly, ACounterView, ICounterPresenter> goalFactory;
    private AFactory<ICounter_Readonly, ACounterView, ICounterPresenter> restrictionFactory;
    private AFactory<IBooster_Readonly, ABoosterView, IBoosterPresenter> boosterFactory;
    private AFactory<PlayerSettings, APausePopUp, IPopUpPresenter> pausePopUpFactory;
    private AFactory<ILevel_Readonly, AEndGamePopUp, IPopUpPresenter> endGamePopUpFactory;

    public CoreGameState(Game game, PrefabConfig prefabs, CellTypeSetSO allCellTypes, Bootstrap bootstrap)
    {
        this.game = game;
        this.prefabs = prefabs;
        this.bootstrap = bootstrap;
        this.allCellTypes = allCellTypes;
    }

    public void OnStart()
    {
        //запуск модели
        game.StartLevel(bootstrap.SelectedLevel);

        //создание фабрик игровых элементов
        blockFactory = new BlockPresenter.Factory(prefabs.blockPrefab, game, bootstrap.SelectedLevel.blockTypeSet);
        cellFactory = new CellPresenter.Factory(prefabs.cellPrefab, allCellTypes);
        invisibleCellFactory = new CellPresenter.Factory(prefabs.invisibleCellPrefab, allCellTypes);
        goalFactory = new CounterPresenter.Factory(prefabs.goalCounterPrefab);
        restrictionFactory = new CounterPresenter.Factory(prefabs.restrictionCounterPrefab);
        boosterFactory = new BoosterPresenter.Factory(prefabs.boosterPrefab, game);
        pausePopUpFactory = new PausePopUpPresenter.Factory(prefabs.pausePopUpPrefab, bootstrap);
        endGamePopUpFactory = new EndGamePopUpPresenter.Factory(prefabs.endGamePopUpPrefab, bootstrap);

        //создание инпута
        input = GameObject.Instantiate(prefabs.inputPrefab);

        //создание фабрик экранов
        var gameboardFactory = new GameBoardPresenter.Factory(prefabs.gameBoardPrefab, blockFactory, cellFactory, invisibleCellFactory);
        var boosterInventoryFactory = new BoosterInventoryPresenter.Factory(prefabs.boosterInventoryPrefab, boosterFactory);
        var endGameFactory = new EndGamePresenter.Factory(prefabs.endGamePrefab, input, endGamePopUpFactory);
        var hudFactory = new HudPresenter.Factory(prefabs.hudPrefab, goalFactory, restrictionFactory);
        var pauseFactory = new PausePresenter.Factory(prefabs.pausePrefab, pausePopUpFactory, input);

        //создание экранов и инпута
        gameBoardScreen = gameboardFactory.Create(game.CurrentLevel.gameBoard).Presenter;
        input.Init(gameBoardScreen).Enable();
        hudScreen = hudFactory.Create(game.CurrentLevel).Presenter;
        boosterScreen = boosterInventoryFactory.Create(game.BoosterInventory).Presenter;
        pauseScreen = pauseFactory.Create(game.PlayerSettings).Presenter;
        endGameScreen = endGameFactory.Create(game.CurrentLevel).Presenter;
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
