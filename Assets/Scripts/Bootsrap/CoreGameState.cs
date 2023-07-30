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
    private AGameBoardScreen gameBoardScreen;
    private AInput input;
    private AHudScreen hudScreen;
    private ABoosterInventoryScreen boosterInventoryScreen;
    private APauseScreen pauseScreen;
    private AEndGameScreen endGameScreen;

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

        //создание экранов и инпута
        gameBoardScreen = AGameBoardScreen.Create(prefabs.gameBoardPrefab, game.Level.gameBoard, blockFactory, cellFactory);
        input = AInput.Create(prefabs.inputPrefab, gameBoardScreen);
        hudScreen = AHudScreen.Create(prefabs.hudPrefab, game.Level, goalFactory, restrictionFactory);
        boosterInventoryScreen = ABoosterInventoryScreen.Create(prefabs.boosterInventoryPrefab, game.BoosterInventory, boosterFactory);
        pauseScreen = APauseScreen.Create(prefabs.pausePrefab, game.PlayerSettings, game, pausePopUpFactory, input);
        endGameScreen = AEndGameScreen.Create(prefabs.endGamePrefab, game.Level, input, endGamePopUpFactory);
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
}
