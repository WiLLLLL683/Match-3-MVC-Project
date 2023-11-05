using System;

namespace View
{
    public interface IPausePopUp : IPopUpView
    {
        public event Action<bool> OnSoundIsOn;
        public event Action<bool> OnVibrationIsOn;

        public void Init(bool soundOnStart, bool vibrationOnStart);
    }
}