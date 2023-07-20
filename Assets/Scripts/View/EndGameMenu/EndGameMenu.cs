using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    /// <summary>
    /// Меню окончания игры, победы или поражения.<br/>
    /// Отображает результат игры в виде звезд и текста очков.
    /// Передает инпут перехода на следующий уровень, рестарта и выхода из кор-игры.
    /// </summary>
    public class EndGameMenu : MonoBehaviour, IEndGameMenu, IEndGameMenuInput
    {
        [SerializeField] private List<Image> starVisuals;
        [SerializeField] private TMP_Text scoreText;

        public event Action OnNextLevelInput;
        public event Action OnReplayInput;
        public event Action OnQuitInput;

        public void UpdateScore(int score, int stars = 0)
        {
            ShowScore(score);
            ShowStars(stars);
        }
        public void Input_NextLevel() => OnNextLevelInput?.Invoke();
        public void Input_Replay() => OnReplayInput?.Invoke();
        public void Input_Quit() => OnQuitInput?.Invoke();



        private void ShowScore(int score) => scoreText.text = score.ToString();
        private void ShowStars(int stars)
        {
            for (int i = 0; i < starVisuals.Count; i++)
            {
                starVisuals[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < stars && i < starVisuals.Count; i++)
            {
                starVisuals[i].gameObject.SetActive(true);
            }
        }
    }
}