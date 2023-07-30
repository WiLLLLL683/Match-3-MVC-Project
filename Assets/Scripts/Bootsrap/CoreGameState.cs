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
    private Game game;
    private PrefabConfig prefabs;
    private Bootstrap bootstrap;

    //экраны
    private AGameBoardScreen gameBoardScreen;
    private AInput input;
    private AHudScreen hudScreen;
    private ABoosterInventoryScreen boosterInventoryScreen;
    private APauseScreen pauseScreen;
    private AEndGameScreen endGameScreen;

    //фабрики игровых элементов
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

        //создание экранов и инпута
        gameBoardScreen = AGameBoardScreen.Create(prefabs.gameBoardPrefab, game.Level.gameBoard, blockFactory, cellFactory);
        input = AInput.Create(prefabs.inputPrefab, gameBoardScreen);
        hudScreen = AHudScreen.Create(prefabs.hudPrefab, game.Level, goalFactory, restrictionFactory);
        boosterInventoryScreen = ABoosterInventoryScreen.Create(prefabs.boosterInventoryPrefab, game.BoosterInventory, boosterFactory);
        pauseScreen = APauseScreen.Create(prefabs.pausePrefab, game.PlayerSettings, input, bootstrap);
        endGameScreen = AEndGameScreen.Create(prefabs.endGamePrefab, game, input);
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
