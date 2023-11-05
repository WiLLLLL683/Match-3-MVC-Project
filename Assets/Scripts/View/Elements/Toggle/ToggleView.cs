using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class ToggleView : MonoBehaviour, IToggleView
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private RectTransform handle;

        public event Action<bool> OnToggle;

        private Vector2 handlePosition;

        public void Init(bool isOn)
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