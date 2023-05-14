﻿using Model.Infrastructure;
using System;
using System.Collections;
using UnityEngine;

namespace Controller
{
    /// <summary>
    /// Контроллер для HUD
    /// </summary>
    public class HudController : MonoBehaviour
    {
        [SerializeField] private PauseMenuController pauseMenuController;

        private Game game;

        public void Init(Game _game)
        {
            game = _game;
        }

        public void PauseGame()
        {
            pauseMenuController.ShowPauseMenu();
        }
    }
}