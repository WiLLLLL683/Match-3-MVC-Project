using System;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class PauseView : MonoBehaviour, IPauseView
    {
        [SerializeField] private PausePopUp pausePopUp;
        [Header("Buttons")]
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button closeButton;

        public IPausePopUp PausePopUp => pausePopUp;

        public event Action OnShowInput;
        public event Action OnHideInput;

        private void Awake()
        {
            pauseButton.onClick.AddListener(Input_Pause);
            closeButton.onClick.AddListener(Input_UnPause);
        }

        private void OnDestroy()
        {
            pauseButton.onClick.RemoveListener(Input_Pause);
            closeButton.onClick.RemoveListener(Input_UnPause);
        }

        private void Input_Pause() => OnShowInput?.Invoke();
        private void Input_UnPause() => OnHideInput?.Invoke();
    }
}