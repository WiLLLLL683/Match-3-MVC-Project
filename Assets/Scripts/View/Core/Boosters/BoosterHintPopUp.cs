using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class BoosterHintPopUp : MonoBehaviour, IBoosterHintPopUp
    {
        [SerializeField] private Canvas popUp;
        [SerializeField] private Canvas overlayWithGameBoard;
        [SerializeField] private Canvas overlayWithButton;
        [SerializeField] private Image boosterIcon;
        [SerializeField] private TMP_Text boosterNameText;
        [SerializeField] private TMP_Text boosterHintText;
        [Header("Buttons")]
        [SerializeField] private Button activateButton;
        [SerializeField] private List<Button> hideButtons;

        public event Action<Vector2Int> OnInputActivate;
        public event Action OnInputHide;

        public void Show(Sprite icon, string name, string hint)
        {
            boosterIcon.sprite = icon;
            boosterNameText.text = name;
            boosterHintText.text = hint;
            popUp.enabled = true;

            activateButton.onClick.AddListener(Input_Activate);
            //TODO подписка на нажатие на игровое поле
            foreach (Button button in hideButtons)
            {
                button.onClick.AddListener(Input_Hide);
            }
        }

        public void Hide()
        {
            popUp.enabled = false;
            overlayWithGameBoard.enabled = false;
            overlayWithButton.enabled = false;

            activateButton.onClick.RemoveListener(Input_Activate);
            foreach (Button button in hideButtons)
            {
                button.onClick.RemoveListener(Input_Hide);
            }
        }

        public void ShowOverlayWithGameBoard()
        {
            overlayWithGameBoard.enabled = true;
            overlayWithButton.enabled = false;
        }

        public void ShowOverlayWithButton()
        {
            overlayWithGameBoard.enabled = false;
            overlayWithButton.enabled = true;
        }

        private void Input_Activate() => OnInputActivate?.Invoke(new());
        private void Input_Hide() => OnInputHide?.Invoke();
    }
}