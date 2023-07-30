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
    private Game game;
    private PrefabConfig prefabs;
    private Bootstrap bootstrap;

    private IInput input;
    private AHudScreen hudScreen;
    private AGameBoardScreen gameBoardScreen;
    private ABoosterInventoryScreen boosterInventoryScreen;
    private APauseScreen pauseScreen;
    private AEndGameScreen endGameScreen;

    private BlockFactory blockFactory;
    private CellFactory cellFactory;
    private CounterFactory goalFactory;
    private CounterFactory restrictionFactory;
    private BoosterFactory boosterFactory;

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
        blockFactory = new BlockFactory(prefabs.blockPrefab);
        cellFactory = new CellFactory(prefabs.cellPrefab);
        goalFactory = new CounterFactory(prefabs.goalCounterPrefab);
        restrictionFactory = new CounterFactory(prefabs.restrictionCounterPrefab);
        boosterFactory = new BoosterFactory(prefabs.boosterPrefab);

        //создание экранов
        gameBoardScreen = CreateGameBoardScreen(game.Level.gameBoard, blockFactory, cellFactory);
        input = CreateInput(gameBoardScreen);
        hudScreen = CreateHUDScreen(game.Level, goalFactory, restrictionFactory);
        boosterInventoryScreen = CreateBoosterInvScreen(game.BoosterInventory, boosterFactory);
        pauseScreen = CreatePauseScreen(game.PlayerSettings, input, bootstrap);
        endGameScreen = CreateEndGameScreen(game, input);
    }
    public void OnEnd()
    {
        //gameBoard.Disable();
        //boosterInventory.Disable();
        //input.Disable();
        //pause.Disable();
        //hud.Disable();
        //GameObject.Destroy(input.gameObject);
        //GameObject.Destroy(pause.gameObject);

        //уничтожение экранов
        GameObject.Destroy(hudScreen.gameObject);
        GameObject.Destroy(gameBoardScreen.gameObject);
        GameObject.Destroy(input.gameObject);
        GameObject.Destroy(boosterInventoryScreen.gameObject);
        GameObject.Destroy(pauseScreen.gameObject);
        GameObject.Destroy(endGameScreen.gameObject);
    }



    private IInput CreateInput(AGameBoardScreen gameBoardScreen)
    {
        var input = (IInput)GameObject.Instantiate(prefabs.inputPrefab.UnderlyingValue);
        input.Init(gameBoardScreen);
        input.Enable();
        return input;
    }
    private AEndGameScreen CreateEndGameScreen(Game game, IInput input)
    {
        var endGameScreen = GameObject.Instantiate(prefabs.endGamePrefab);
        endGameScreen.Init(game, input);
        endGameScreen.Enable();
        return endGameScreen;
    }
    private APauseScreen CreatePauseScreen(PlayerSettings playerSettings, IInput input, Bootstrap bootstrap)
    {
        var pauseScreen = GameObject.Instantiate(prefabs.pausePrefab);
        pauseScreen.Init(playerSettings, input, bootstrap);
        pauseScreen.Enable();
        return pauseScreen;
    }
    private ABoosterInventoryScreen CreateBoosterInvScreen(BoosterInventory boosterInventory, BoosterFactory boosterFactory)
    {
        var boosterInventoryScreen = GameObject.Instantiate(prefabs.boosterInventoryPrefab);
        boosterInventoryScreen.Init(boosterInventory, boosterFactory);
        boosterInventoryScreen.Enable();
        return boosterInventoryScreen;
    }
    private AGameBoardScreen CreateGameBoardScreen(IGameBoard_Readonly gameboardModel,
                 AFactory<IBlock_Readonly, IBlockView, IBlockPresenter> blockFactory,
                 AFactory<ICell_Readonly, ICellView, ICellPresenter> cellFactory)
    {
        var gameBoardScreen = GameObject.Instantiate(prefabs.gameBoardPrefab);
        gameBoardScreen.Init(gameboardModel, blockFactory, cellFactory);
        gameBoardScreen.Enable();
        return gameBoardScreen;
    }
    private AHudScreen CreateHUDScreen(ILevel_Readonly levelModel,
            AFactory<ICounter_Readonly, ICounterView, ICounterPresenter> goalFactory,
            AFactory<ICounter_Readonly, ICounterView, ICounterPresenter> restrictionFactory)
    {
        var hudScreen = GameObject.Instantiate(prefabs.hudPrefab);
        hudScreen.Init(levelModel, goalFactory, restrictionFactory);
        hudScreen.Enable();
        return hudScreen;
    }
}
