﻿using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    /// <summary>
    /// Всплываюшее окно для окончания игры, победы или поражения.<br/>
    /// Отображает результат игры в виде звезд и текста очков.
    /// </summary>
    public class EndGamePopUp : PopUpView, IEndGamePopUp
    {
        [SerializeField] private List<Image> starVisuals;
        [SerializeField] private TMP_Text scoreText;

        public void UpdateScore(int score, int stars = 0)
        {
            ShowScore(score);
            ShowStars(stars);
        }

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