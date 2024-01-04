using System;
using UnityEngine;

namespace View
{
    public class PausePopUp : MonoBehaviour, IPausePopUp
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private ToggleView soundToggle;
        [SerializeField] private ToggleView vibrationToggle;

        public event Action OnShow;
        public event Action OnHide;
        public event Action<bool> OnSoundIsOn;
        public event Action<bool> OnVibrationIsOn;
        public event Action OnReplayInput;
        public event Action OnQuitInput;

        public virtual void Show(bool soundOnStart, bool vibrationOnStart)
        {
            soundToggle.Init(soundOnStart);
            vibrationToggle.Init(vibrationOnStart);
            canvas.enabled = true;

            soundToggle.OnToggle += SwitchSound;
            vibrationToggle.OnToggle += SwitchVibration;
            OnShow?.Invoke();
        }
        public virtual void Hide()
        {
            canvas.enabled = false;

            soundToggle.OnToggle -= SwitchSound;
            vibrationToggle.OnToggle -= SwitchVibration;
            OnHide?.Invoke();
        }

        public void Input_Replay() => OnReplayInput?.Invoke();
        public void Input_Quit() => OnQuitInput?.Invoke();

        private void SwitchSound(bool isOn) => OnSoundIsOn?.Invoke(isOn);
        private void SwitchVibration(bool isOn) => OnVibrationIsOn?.Invoke(isOn);
    }
}