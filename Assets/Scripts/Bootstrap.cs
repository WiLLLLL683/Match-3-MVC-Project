using Controller;
using Data;
using Model.Infrastructure;
using System;
using System.Collections;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameBoardController gameBoardController;
    [SerializeField] private BoosterController boosterController;
    [SerializeField] private HudAdapter hudAdapter;
    [SerializeField] private LevelSelectionController levelSelectionController;
    [SerializeField] private LevelData selectedLevel;

    private Game game;

    private void Awake()
    {
        game = new();
        //game.StartMetaGame();

        gameBoardController.Init(game);
        boosterController.Init(game);
        hudAdapter.Init(game);
        levelSelectionController.Init(game);

        game.StartCoreGame(selectedLevel);

        gameBoardController.SpawnCells();
        gameBoardController.SpawnBlocks();
    }
}
