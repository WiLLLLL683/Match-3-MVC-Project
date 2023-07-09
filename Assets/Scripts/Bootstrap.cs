using Presenter;
using Data;
using Model.Infrastructure;
using System;
using System.Collections;
using UnityEngine;
using AYellowpaper;

public class Bootstrap : MonoBehaviour
{
    [Header("Meta Game Prefabs")]
    [SerializeField] private InterfaceReference<ILevelSelectionController, MonoBehaviour> levelSelectionPrefab;
    [SerializeField] private Canvas backgroundPrefab;
    [SerializeField] private Canvas headerPrefab;
    [Header("Core Game Prefabs")]
    [SerializeField] private InterfaceReference<IHudPresenter, MonoBehaviour> hudPrefab;
    [SerializeField] private InterfaceReference<IGameBoardPresenter, MonoBehaviour> gameBoardPrefab;
    [SerializeField] private InterfaceReference<IInput, MonoBehaviour> inputPrefab;
    [SerializeField] private InterfaceReference<IBoosterInventoryPresenter, MonoBehaviour> boosterInventoryPrefab;
    [SerializeField] private InterfaceReference<IPausePresenter, MonoBehaviour> pausePrefab;
    [SerializeField] private InterfaceReference<IEndGamePresenter, MonoBehaviour> endGamePrefab;
    [Header("Settings")]
    [SerializeField] private LevelData selectedLevel;

    private Game game;
    //meta game
    private ILevelSelectionController levelSelection;
    private Canvas background;
    private Canvas header;
    //core game
    private IHudPresenter hud;
    private IGameBoardPresenter gameBoard;
    private IInput input;
    private IBoosterInventoryPresenter boosters;
    private IPausePresenter pause;
    private IEndGamePresenter endGame;

    private void Awake()
    {
        game = new();

        LoadMetaGame();
    }
    public void LoadMetaGame()
    {
        if (gameBoard != null)
            UnloadCoreGame();

        levelSelection = (ILevelSelectionController)Instantiate(levelSelectionPrefab.UnderlyingValue);
        background = Instantiate(backgroundPrefab);
        header = Instantiate(headerPrefab);

        levelSelection.Init(game, this);

        //game.StartMetaGame();
    }
    public void LoadCoreGame()
    {
        if (levelSelection != null)
            UnloadMetaGame();

        hud = (IHudPresenter) Instantiate(hudPrefab.UnderlyingValue);
        gameBoard = (IGameBoardPresenter) Instantiate(gameBoardPrefab.UnderlyingValue);
        input = (IInput) Instantiate(inputPrefab.UnderlyingValue);
        boosters = (IBoosterInventoryPresenter) Instantiate(boosterInventoryPrefab.UnderlyingValue);
        pause = (IPausePresenter) Instantiate(pausePrefab.UnderlyingValue);
        endGame = (IEndGamePresenter) Instantiate(endGamePrefab.UnderlyingValue);

        hud.Init(game);
        gameBoard.Init(game, input);
        boosters.Init(game);
        pause.Init(game, input, this);
        endGame.Init(game, input, this);

        game.StartCoreGame(selectedLevel);
        gameBoard.SpawnCells();
        gameBoard.SpawnBlocks();
    }



    private void UnloadCoreGame()
    {
        Destroy(hud.gameObject);
        Destroy(gameBoard.gameObject);
        Destroy(boosters.gameObject);
        Destroy(pause.gameObject);
        Destroy(endGame.gameObject);
    }
    private void UnloadMetaGame()
    {
        Destroy(levelSelection.gameObject);
        Destroy(background.gameObject);
        Destroy(header.gameObject);
    }
}
