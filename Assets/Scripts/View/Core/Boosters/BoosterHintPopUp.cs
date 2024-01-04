using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class BoosterHintPopUp : MonoBehaviour, IBoosterHintPopUp
    {
        [SerializeField] private Canvas popUp;
        [SerializeField] private Canvas overlayBehindGameBoard;
        [SerializeField] private Canvas overlayInfrontGameBoard;
        [SerializeField] private Image boosterIcon;
        [SerializeField] private TMP_Text boosterNameText;
        [SerializeField] private TMP_Text boosterHintText;

        public void Show(Sprite icon, string name, string hint, bool showGameBoard = true)
        {
            boosterIcon.sprite = icon;
            boosterNameText.text = name;
            boosterHintText.text = hint;

            popUp.enabled = true;
            overlayBehindGameBoard.enabled = showGameBoard;
            overlayInfrontGameBoard.enabled = !showGameBoard;
        }

        public void Hide()
        {
            popUp.enabled = false;
            overlayBehindGameBoard.enabled = false;
            overlayInfrontGameBoard.enabled = false;
        }
    }
}