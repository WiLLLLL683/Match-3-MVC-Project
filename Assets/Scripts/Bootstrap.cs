using Presenter;
using Data;
using Model.Infrastructure;
using System;
using System.Collections;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [Header("Meta Game Prefabs")]
    [SerializeField] private LevelSelectionController levelSelectionPrefab;
    [SerializeField] private Canvas backgroundPrefab;
    [SerializeField] private Canvas headerPrefab;
    [Header("Core Game Prefabs")]
    [SerializeField] private HudPresenter hudPrefab;
    [SerializeField] private GameBoardPresenter gameBoardPrefab;
    [SerializeReference] private InputBase inputPrefab;
    [SerializeField] private BoosterInventoryPresenter boostersPrefab;
    [SerializeField] private PausePresenter pausePrefab;
    [SerializeField] private EndGamePresenter endGamePrefab;

    [Header("Settings")]
    [SerializeField] private LevelData selectedLevel;

    private Game game;

    private LevelSelectionController levelSelection;
    private Canvas background;
    private Canvas header;

    private HudPresenter hud;
    private GameBoardPresenter gameBoard;
    private InputBase input;
    private BoosterInventoryPresenter boosters;
    private PausePresenter pause;
    private EndGamePresenter endGame;

    private void Awake()
    {
        game = new();

        LoadMetaGame();
        //LoadCoreGame();
    }

    public void LoadMetaGame()
    {
        if (gameBoard != null)
            UnloadCoreGame();

        levelSelection = Instantiate(levelSelectionPrefab);
        background = Instantiate(backgroundPrefab);
        header = Instantiate(headerPrefab);

        levelSelection.Init(game, this);

        //game.StartMetaGame();
    }
    public void LoadCoreGame()
    {
        if (levelSelection != null)
            UnloadMetaGame();

        hud = Instantiate(hudPrefab);
        gameBoard = Instantiate(gameBoardPrefab);
        input = Instantiate(inputPrefab);
        boosters = Instantiate(boostersPrefab);
        pause = Instantiate(pausePrefab);
        endGame = Instantiate(endGamePrefab);

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
