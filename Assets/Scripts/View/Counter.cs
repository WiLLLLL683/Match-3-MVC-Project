using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace ViewElements
{
    public class Counter : MonoBehaviour
    {
        [SerializeField] private TMP_Text countText;
        [SerializeField] private Image icon;

        public void Init(Sprite iconSprite, int initialNumber)
        {
            SetIcon(iconSprite);
            SetCount(initialNumber);
        }

        public void SetCount(int count) => countText.text = count.ToString();
        public void SetIcon(Sprite iconSprite)
        {
            if (icon != null && iconSprite != null)
            {
                icon.sprite = iconSprite;
            }
        }
    }
}