using System;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class PausePopUp : MonoBehaviour, IPausePopUp
    {
        [SerializeField] private Canvas canvas;
        [Header("Buttons")]
        [SerializeField] private ToggleView soundToggle;
        [SerializeField] private ToggleView vibrationToggle;
        [SerializeField] private Button replayButton;
        [SerializeField] private Button quitButton;

        public event Action<bool> OnSoundIsOn;
        public event Action<bool> OnVibrationIsOn;
        public event Action OnReplayInput;
        public event Action OnQuitInput;

        public virtual void Show(bool soundOnStart, bool vibrationOnStart)
        {
            soundToggle.Init(soundOnStart);
            vibrationToggle.Init(vibrationOnStart);
            canvas.enabled = true;

            soundToggle.OnToggle += Input_SwitchSound;
            vibrationToggle.OnToggle += Input_SwitchVibration;
            replayButton.onClick.AddListener(Input_Replay);
            quitButton.onClick.AddListener(Input_Quit);
        }
        public virtual void Hide()
        {
            canvas.enabled = false;

            soundToggle.OnToggle -= Input_SwitchSound;
            vibrationToggle.OnToggle -= Input_SwitchVibration;
            replayButton.onClick.RemoveListener(Input_Replay);
            quitButton.onClick.RemoveListener(Input_Quit);
        }

        private void Input_Replay() => OnReplayInput?.Invoke();
        private void Input_Quit() => OnQuitInput?.Invoke();
        private void Input_SwitchSound(bool isOn) => OnSoundIsOn?.Invoke(isOn);
        private void Input_SwitchVibration(bool isOn) => OnVibrationIsOn?.Invoke(isOn);
    }
}