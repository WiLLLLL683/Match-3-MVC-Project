using Data;
using Model.Infrastructure;
using Presenter;
using UnityEngine;
using Utils;
using View;

public class CoreGameState : IState
{
    private Game game;
    private PrefabConfig prefabs;
    private Bootstrap bootstrap;

    private IHudPresenter hud;
    private IGameBoardPresenter gameBoard;
    private IInput input;
    private IBoosterInventoryPresenter boosterInventory;
    private IPausePresenter pause;
    private IEndGamePresenter endGame;

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
        
        //создание фабрик
        var blockFactory = new BlockFactory(prefabs.blockPrefab);
        var cellFactory = new CellFactory(prefabs.cellPrefab);
        var boosterFactory = new BoosterFactory(prefabs.boosterPrefab);
        var goalFactory = new CounterFactory(prefabs.goalCounterPrefab);
        var restrictionFactory = new CounterFactory(prefabs.restrictionCounterPrefab);
        var gameBoardFactory = new GameBoardFactory(prefabs.gameBoardPrefab, blockFactory, cellFactory);

        gameBoardFactory.Create(game.Level.gameBoard, out gameBoard);


        //создание окон вью
        hud = (IHudPresenter)GameObject.Instantiate(prefabs.hudPrefab.UnderlyingValue);
        input = (IInput)GameObject.Instantiate(prefabs.inputPrefab.UnderlyingValue);
        boosterInventory = (IBoosterInventoryPresenter)GameObject.Instantiate(prefabs.boosterInventoryPrefab.UnderlyingValue);
        pause = (IPausePresenter)GameObject.Instantiate(prefabs.pausePrefab.UnderlyingValue);
        endGame = (IEndGamePresenter)GameObject.Instantiate(prefabs.endGamePrefab.UnderlyingValue);
        //инициализация
        hud.Init(game, goalFactory, restrictionFactory);
        input.Init(gameBoard);
        boosterInventory.Init(game, boosterFactory);
        pause.Init(game, input, bootstrap);
        endGame.Init(game, input, bootstrap);
        //запуск
        hud.Enable();
        gameBoard.Enable();
        input.Enable();
        boosterInventory.Enable();
        pause.Enable();
        endGame.Enable();
        //создание игровых элементов
        gameBoard.SpawnCells();
        gameBoard.SpawnBlocks();
    }
    public void OnEnd()
    {
        gameBoard.Destroy();

        hud.Disable();
        input.Disable();
        boosterInventory.Disable();
        pause.Disable();
        endGame.Disable();
        GameObject.Destroy(hud.gameObject);
        GameObject.Destroy(input.gameObject);
        GameObject.Destroy(boosterInventory.gameObject);
        GameObject.Destroy(pause.gameObject);
        GameObject.Destroy(endGame.gameObject);
    }
}
