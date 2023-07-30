using System;
using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class PausePopUp : APausePopUp
    {
        [SerializeField] private AToggleView soundToggle;
        [SerializeField] private AToggleView vibrationToggle;

        public override event Action<bool> OnSoundIsOn;
        public override event Action<bool> OnVibrationIsOn;

        public override void Init(bool soundOnStart, bool vibrationOnStart)
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