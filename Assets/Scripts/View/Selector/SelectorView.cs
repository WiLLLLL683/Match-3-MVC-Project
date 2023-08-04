using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class SelectorView : ASelectorView, ISelector_Input
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text nameText;

        public override event Action OnStartSelected;
        public override event Action OnSelectNext;
        public override event Action OnSelectPrevious;

        public override void Init(Sprite iconSprite, string name)
        {
            SetIcon(iconSprite);
            SetName(name);
        }
        public override void SetIcon(Sprite iconSprite) => icon.sprite = iconSprite;
        public override void SetName(string name) => nameText.text = name;
        public void Input_StartSelected() => OnStartSelected?.Invoke();
        public void Input_SelectNext() => OnSelectNext?.Invoke();
        public void Input_SelectPrevious() => OnSelectPrevious?.Invoke();
    }
}