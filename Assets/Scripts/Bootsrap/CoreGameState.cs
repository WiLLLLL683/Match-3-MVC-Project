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

    private BlockFactory blockFactory;
    private CellFactory cellFactory;
    private CounterFactory goalFactory;
    private CounterFactory restrictionFactory;
    private BoosterFactory boosterFactory;
    private BoosterInventoryFactory boosterInventoryFactory;
    private GameBoardFactory gameBoardFactory;
    private EndGameFactory endGameFactory;

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

        //создание фабрик экранов
        boosterInventoryFactory = new BoosterInventoryFactory(prefabs.boosterInventoryPrefab, boosterFactory);
        gameBoardFactory = new GameBoardFactory(prefabs.gameBoardPrefab, blockFactory, cellFactory);
        endGameFactory = new EndGameFactory(prefabs.endGamePrefab, input, bootstrap);

        //создание экранов
        boosterInventoryFactory.Create(game.BoosterInventory, out boosterInventory);
        gameBoardFactory.Create(game.Level.gameBoard, out gameBoard);
        endGameFactory.Create(game, out endGame);

        hud = (IHudPresenter)GameObject.Instantiate(prefabs.hudPrefab.UnderlyingValue);
        input = (IInput)GameObject.Instantiate(prefabs.inputPrefab.UnderlyingValue);
        pause = (IPausePresenter)GameObject.Instantiate(prefabs.pausePrefab.UnderlyingValue);
        //инициализация
        hud.Init(game, goalFactory, restrictionFactory);
        input.Init(gameBoard);
        pause.Init(game, input, bootstrap);
        //запуск
        hud.Enable();
        input.Enable();
        pause.Enable();
        //создание игровых элементов
        gameBoard.SpawnCells();
        gameBoard.SpawnBlocks();
    }
    public void OnEnd()
    {
        gameBoardFactory.Clear();
        boosterInventoryFactory.Clear();
        endGameFactory.Clear();

        hud.Disable();
        input.Disable();
        pause.Disable();
        GameObject.Destroy(hud.gameObject);
        GameObject.Destroy(input.gameObject);
        GameObject.Destroy(pause.gameObject);
    }
}
