using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View
{
    /// <summary>
    /// Кнопка для запуска бустера.<br/>
    /// Может изменять иконку и текст с количеством бустеров, включать/выключать кнопку.
    /// Передает инпут активации бустера.
    /// </summary>
    public class BoosterButtonView : MonoBehaviour, IBoosterButtonView
    {
        public class Factory : PlaceholderFactory<BoosterButtonView> { }

        [SerializeField] private TMP_Text ammountText;
        [SerializeField] private Image icon;
        [SerializeField] private Button button;

        public event Action OnActivate;

        public void Init(Sprite iconSprite, int initialAmmount)
        {
            ChangeIcon(iconSprite);
            ChangeAmount(initialAmmount);
            button.onClick.AddListener(Input_ActivateBooster);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(Input_ActivateBooster);
        }

        public void EnableButton() => button.enabled = true;
        public void DisableButton() => button.enabled = false;
        public void ChangeAmount(int boosterAmmount) => ammountText.text = boosterAmmount.ToString();

        public void ChangeIcon(Sprite iconSprite)
        {
            if (icon != null && iconSprite != null)
            {
                icon.sprite = iconSprite;
            }
        }

        private void Input_ActivateBooster() => OnActivate?.Invoke();
    }
}