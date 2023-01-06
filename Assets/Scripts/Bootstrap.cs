using Controller;
using Model.GameLogic;
using System;
using System.Collections;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private Game game;
    [SerializeField] private GameBoardController gameBoardController;
    [SerializeField] private BoosterController boosterController;
    [SerializeField] private HudAdapter hudAdapter;
    [SerializeField] private LevelSelectionController levelSelectionController;

    private void Start()
    {
        game = new();
        game.StartMetaGame();

        gameBoardController.Init(game);
        boosterController.Init(game);
        hudAdapter.Init(game);
        levelSelectionController.Init(game);
    }
}
