using System;

namespace View
{
    public interface IPausePopUp
    {
        event Action OnShow;
        event Action OnHide;
        event Action<bool> OnSoundIsOn;
        event Action<bool> OnVibrationIsOn;
        event Action OnQuitInput;
        event Action OnReplayInput;

        void Show(bool soundOnStart, bool vibrationOnStart);
        void Hide();
        void Input_Quit();
        void Input_Replay();
    }
}