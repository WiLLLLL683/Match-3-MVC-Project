using System;
using UnityEngine;

namespace View
{
    public class PauseView : MonoBehaviour, IPauseView
    {
        [SerializeField] private PausePopUp pausePopUp;

        public IPausePopUp PausePopUp => pausePopUp;

        public event Action OnShowInput;
        public event Action OnHideInput;

        public void Input_Pause() => OnShowInput?.Invoke();
        public void Input_UnPause() => OnHideInput?.Invoke();
    }
}