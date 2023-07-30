using NaughtyAttributes;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class ToggleView : AToggleView
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private RectTransform handle;

        public override event Action<bool> OnToggle;

        private Vector2 handlePosition;

        [Button]
        public void Init() => Init(false);
        public override void Init(bool isOn)
        {
            handlePosition = handle.anchoredPosition;
            toggle.isOn = isOn;
            handle.anchoredPosition = isOn ? handlePosition * -1 : handlePosition;

            toggle.onValueChanged.AddListener(OnSwitch);
        }
        private void OnSwitch(bool isOn)
        {
            handle.anchoredPosition = isOn ? handlePosition * -1 : handlePosition;
            OnToggle?.Invoke(isOn);
        }
    }
}