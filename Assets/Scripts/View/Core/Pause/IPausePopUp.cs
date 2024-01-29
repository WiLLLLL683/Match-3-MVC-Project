using System;

namespace View
{
    public interface IPausePopUp
    {
        event Action<bool> OnSoundIsOn;
        event Action<bool> OnVibrationIsOn;
        event Action OnQuitInput;
        event Action OnReplayInput;

        void Show(bool soundOnStart, bool vibrationOnStart);
        void Hide();
    }
}