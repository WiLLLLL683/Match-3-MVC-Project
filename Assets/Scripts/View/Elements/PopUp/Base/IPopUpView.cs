using System;

namespace View
{
    public interface IPopUpView
    {
        event Action OnHide;
        event Action OnNextLevelInput;
        event Action OnQuitInput;
        event Action OnReplayInput;
        event Action OnShow;

        void Hide();
        void Show();
    }
}