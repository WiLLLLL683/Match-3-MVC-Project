using Data;
using Model.Infrastructure;
using System;
using System.Collections;
using UnityEngine;

namespace Controller
{
    public class LevelSelectionController : MonoBehaviour
    {
        private Game game;
        private LevelDataScriptable[] allLevels;
        private LevelDataScriptable selectedLevel;

        public void Init(Game _game)
        {
            game = _game;
        }

        public void SelectPreviousLevel()
        {
            //TODO
        }
        public void SelectNextLevel()
        {
            //TODO
        }
        public void StartSelectedLevel() => game.StartCoreGame(selectedLevel);
    }
}