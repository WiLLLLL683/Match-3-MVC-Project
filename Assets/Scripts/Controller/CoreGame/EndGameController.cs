using Model.Infrastructure;
using System;
using System.Collections;
using UnityEngine;

namespace Controller
{
    public class EndGameController : MonoBehaviour
    {
        [SerializeField] private EndGameMenu completeMenu;
        [SerializeField] private EndGameMenu defeatMenu;

        private Game game;
        private InputBase input;
        private Bootstrap bootstrap;

        public void Init(Game game, InputBase input, Bootstrap bootstrap)
        {
            this.game = game;
            this.input = input;
            this.bootstrap = bootstrap;
        }

        public void ShowCompleteMenu()
        {
            defeatMenu.gameObject.SetActive(false);
            completeMenu.gameObject.SetActive(true);

            input.Disable();
        }
        public void ShowDefeatMenu()
        {
            defeatMenu.gameObject.SetActive(true);
            completeMenu.gameObject.SetActive(false);

            input.Disable();
        }
        public void HideAllMenus()
        {
            defeatMenu.gameObject.SetActive(false);
            completeMenu.gameObject.SetActive(false);

            input.Enable();
        }

        //функционал кнопок
        public void Replay()
        {
            //TODO replay
            Debug.Log("Replay");
        }
        public void NextLevel()
        {
            //TODO nextLevel
            Debug.Log("Next Level");
        }
        public void Quit() => bootstrap.LoadMetaGame();
    }
}