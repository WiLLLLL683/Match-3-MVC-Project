using Data;
using Model.Infrastructure;
using System;
using System.Collections;
using UnityEngine;

namespace Controller
{
    /// <summary>
    /// Контроллер для окна выбора уровня
    /// </summary>
    public class LevelSelectionController : MonoBehaviour
    {
        private Game game;
        private Bootstrap bootstrap;
        private LevelData[] allLevels;
        private LevelData selectedLevel;

        public void Init(Game game, Bootstrap bootstrap)
        {
            this.game = game;
            this.bootstrap = bootstrap;
        }

        public void SelectPreviousLevel()
        {
            //TODO
        }
        public void SelectNextLevel()
        {
            //TODO
        }
        public void StartSelectedLevel() => bootstrap.LoadCoreGame();
    }
}