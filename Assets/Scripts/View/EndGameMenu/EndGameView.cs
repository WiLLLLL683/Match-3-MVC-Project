using System;
using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class EndGameView : AEndGameView
    {
        [SerializeField] private EndGameMenu completeMenu;
        [SerializeField] private EndGameMenu defeatMenu;

        public override event Action OnNextLevelInput;
        public override event Action OnReplayInput;
        public override event Action OnQuitInput;

        public override void Init()
        {
            completeMenu.OnNextLevelInput += Input_NextLevel;
            completeMenu.OnReplayInput += Input_Replay;
            completeMenu.OnQuitInput += Input_Quit;
            defeatMenu.OnNextLevelInput += Input_NextLevel;
            defeatMenu.OnReplayInput += Input_Replay;
            defeatMenu.OnQuitInput += Input_Quit;
        }
        private void OnDestroy()
        {
            completeMenu.OnNextLevelInput -= Input_NextLevel;
            completeMenu.OnReplayInput -= Input_Replay;
            completeMenu.OnQuitInput -= Input_Quit;
            defeatMenu.OnNextLevelInput -= Input_NextLevel;
            defeatMenu.OnReplayInput -= Input_Replay;
            defeatMenu.OnQuitInput -= Input_Quit;
        }
        public override void ShowCompleteMenu(int score, int stars)
        {
            defeatMenu.gameObject.SetActive(false);

            completeMenu.UpdateScore(score, stars);
            completeMenu.gameObject.SetActive(true);
        }
        public override void ShowDefeatMenu(int score)
        {
            completeMenu.gameObject.SetActive(false);

            defeatMenu.UpdateScore(score);
            defeatMenu.gameObject.SetActive(true);
        }
        public override void HideAllMenus()
        {
            defeatMenu.gameObject.SetActive(false);
            completeMenu.gameObject.SetActive(false);
        }

        private void Input_NextLevel() => OnNextLevelInput?.Invoke();
        private void Input_Replay() => OnReplayInput?.Invoke();
        private void Input_Quit() => OnQuitInput?.Invoke();
    }
}