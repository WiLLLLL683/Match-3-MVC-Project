using Controller;
using Model.Infrastructure;
using System;
using System.Collections;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private Game game;
    private InputBase input;
    private Bootstrap bootstrap;

    public void Init(Game game, InputBase input, Bootstrap bootstrap)
    {
        this.game = game;
        this.input = input;
        this.bootstrap = bootstrap;
    }

    //функционал кнопок
    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
        input.Disable();
    }
    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
        input.Enable();
    }
    public void Quit() => bootstrap.LoadMetaGame();
    public void Replay()
    {
        //TODO replay
        Debug.Log("Replay");
    }
}
