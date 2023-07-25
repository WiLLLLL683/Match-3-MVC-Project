﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    /// <summary>
    /// Кнопка для запуска бустера.<br/>
    /// Может изменять иконку и текст с количеством бустеров, включать/выключать кнопку.
    /// Передает инпут активации бустера.
    /// </summary>
    public class BoosterView : IBoosterView, IBoosterInput
    {
        [SerializeField] private TMP_Text ammountText;
        [SerializeField] private Image icon;
        [SerializeField] private Button button;

        public override event Action OnActivate;

        public override void Init(Sprite iconSprite, int initialNumber)
        {
            ChangeIcon(iconSprite);
            ChangeAmount(initialNumber);
        }
        public override void ChangeAmount(int boosterAmmount) => ammountText.text = boosterAmmount.ToString();
        public override void ChangeIcon(Sprite iconSprite)
        {
            if (icon != null && iconSprite != null)
            {
                icon.sprite = iconSprite;
            }
        }
        public override void EnableButton() => button.enabled = true;
        public override void DisableButton() => button.enabled = false;
        public void Input_ActivateBooster() => OnActivate?.Invoke();
    }
}