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
        [SerializeField] private TMP_Text ammountText;
        [SerializeField] private Image icon;
        [SerializeField] private Button button;

        public int Id { get; private set; }

        public event Action<IBoosterButtonView> OnActivate;

        public void Init(int id, Sprite iconSprite, int initialAmmount, bool isEnabled)
        {
            this.Id = id;
            ChangeIcon(iconSprite);
            ChangeAmount(initialAmmount);
            EnableButton(isEnabled);
            button.onClick.AddListener(Input_ActivateBooster);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(Input_ActivateBooster);
        }

        public void EnableButton(bool isEnabled) => button.interactable = isEnabled;
        public void ChangeAmount(int boosterAmmount) => ammountText.text = boosterAmmount.ToString();

        public void ChangeIcon(Sprite iconSprite)
        {
            if (icon != null && iconSprite != null)
            {
                icon.sprite = iconSprite;
            }
        }

        private void Input_ActivateBooster() => OnActivate?.Invoke(this);
    }
}