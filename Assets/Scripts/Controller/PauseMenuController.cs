using Controller;
using System;
using System.Collections;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameBoardInput gameBoardInput;
    [SerializeField] private GameObject pauseMenu;

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
        gameBoardInput.enabled = false;
    }

    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
        gameBoardInput.enabled = true;
    }
}
