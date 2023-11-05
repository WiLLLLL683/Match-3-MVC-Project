using System;
using UnityEngine;

namespace View
{
    public class PausePopUp : PopUpView, IPausePopUp
    {
        [SerializeField] private AToggleView soundToggle;
        [SerializeField] private AToggleView vibrationToggle;

        public event Action<bool> OnSoundIsOn;
        public event Action<bool> OnVibrationIsOn;

        public void Init(bool soundOnStart, bool vibrationOnStart)
        {
            soundToggle.Init(soundOnStart);
            vibrationToggle.Init(vibrationOnStart);

            soundToggle.OnToggle += SwitchSound;
            vibrationToggle.OnToggle += SwitchVibration;
        }

        private void OnDestroy()
        {
            soundToggle.OnToggle -= SwitchSound;
            vibrationToggle.OnToggle -= SwitchVibration;
        }

        private void SwitchSound(bool isOn)
        {
            OnSoundIsOn?.Invoke(isOn);
        }

        private void SwitchVibration(bool isOn)
        {
            OnVibrationIsOn?.Invoke(isOn);
        }
    }
}