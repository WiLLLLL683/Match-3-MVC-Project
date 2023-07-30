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
    public class EndGamePopUp : AEndGamePopUp
    {
        [SerializeField] private List<Image> starVisuals;
        [SerializeField] private TMP_Text scoreText;

        public override void UpdateScore(int score, int stars = 0)
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