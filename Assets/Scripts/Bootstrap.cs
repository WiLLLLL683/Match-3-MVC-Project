using Model.GameLogic;
using System;
using System.Collections;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private Game game;

    private void Start()
    {
        game = new();
        game.StartMetaGame();
    }
}
