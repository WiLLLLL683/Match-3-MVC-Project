using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace View
{
    public class Counter : MonoBehaviour
    {
        [SerializeField] private TMP_Text numberText;
        [SerializeField] private Image icon;

        public void Init(Sprite iconSprite, int initialNumber)
        {
            SetIcon(iconSprite);
            SetNumber(initialNumber);
        }

        public void SetNumber(int numer) => numberText.text = numer.ToString();
        public void SetIcon(Sprite iconSprite)
        {
            if (icon != null && iconSprite != null)
            {
                icon.sprite = iconSprite;
            }
        }
    }
}