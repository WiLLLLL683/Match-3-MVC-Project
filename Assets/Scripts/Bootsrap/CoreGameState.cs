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
    private LevelData selectedLevel;

    private IHudPresenter hud;
    private IGameBoardPresenter gameBoard;
    private IInput input;
    private IBoosterInventoryPresenter boosters;
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
        selectedLevel = bootstrap.SelectedLevel;
        //создание окон вью
        hud = (IHudPresenter)GameObject.Instantiate(prefabs.hudPrefab.UnderlyingValue);
        gameBoard = (IGameBoardPresenter)GameObject.Instantiate(prefabs.gameBoardPrefab.UnderlyingValue);
        input = (IInput)GameObject.Instantiate(prefabs.inputPrefab.UnderlyingValue);
        boosters = (IBoosterInventoryPresenter)GameObject.Instantiate(prefabs.boosterInventoryPrefab.UnderlyingValue);
        pause = (IPausePresenter)GameObject.Instantiate(prefabs.pausePrefab.UnderlyingValue);
        endGame = (IEndGamePresenter)GameObject.Instantiate(prefabs.endGamePrefab.UnderlyingValue);
        //запуск модели
        game.StartCoreGame(selectedLevel);
        //инициализация
        hud.Init(game);
        gameBoard.Init(game, game.Level.gameBoard, prefabs);
        input.Init(gameBoard);
        boosters.Init(game, prefabs);
        pause.Init(game, input, bootstrap);
        endGame.Init(game, input, bootstrap);
        //создание игровых элементов
        gameBoard.SpawnCells();
        gameBoard.SpawnBlocks();
    }
    public void OnEnd()
    {
        GameObject.Destroy(hud.gameObject);
        GameObject.Destroy(gameBoard.gameObject);
        GameObject.Destroy(boosters.gameObject);
        GameObject.Destroy(pause.gameObject);
        GameObject.Destroy(endGame.gameObject);
    }
}
