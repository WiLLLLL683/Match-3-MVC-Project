using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    /// <summary>
    /// Всплываюшее окно для окончания игры, победы или поражения.<br/>
    /// Отображает результат игры в виде звезд и текста очков.
    /// </summary>
    public class EndGamePopUp : MonoBehaviour, IEndGamePopUp
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private List<Image> starVisuals;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private Button nextLevelButton;
        [SerializeField] private Button replayButton;
        [SerializeField] private Button quitButton;

        public event Action OnNextLevelInput;
        public event Action OnReplayInput;
        public event Action OnQuitInput;

        public void Show(int score, int stars = 0)
        {
            ShowScore(score);
            ShowStars(stars);
            nextLevelButton.onClick.AddListener(Input_NextLevel);
            replayButton.onClick.AddListener(Input_Replay);
            quitButton.onClick.AddListener(Input_Quit);

            canvas.enabled = true;
        }

        public void Hide()
        {
            nextLevelButton.onClick.RemoveListener(Input_NextLevel);
            replayButton.onClick.RemoveListener(Input_Replay);
            quitButton.onClick.RemoveListener(Input_Quit);

            canvas.enabled = false;
        }

        private void Input_NextLevel() => OnNextLevelInput?.Invoke();
        private void Input_Replay() => OnReplayInput?.Invoke();
        private void Input_Quit() => OnQuitInput?.Invoke();
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