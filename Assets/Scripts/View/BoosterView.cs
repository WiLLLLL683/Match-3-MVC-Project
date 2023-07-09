using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class BoosterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text ammountText;
        [SerializeField] private Image icon;

        public void Init(Sprite iconSprite, int initialNumber)
        {
            SetIcon(iconSprite);
            SetAmount(initialNumber);
        }
        public void SetAmount(int boosterAmmount) => ammountText.text = boosterAmmount.ToString();
        public void SetIcon(Sprite iconSprite)
        {
            if (icon != null && iconSprite != null)
            {
                icon.sprite = iconSprite;
            }
        }
    }
}