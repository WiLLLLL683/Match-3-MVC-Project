
using System;

namespace View
{
    /// <summary>
    /// Меню окончания игры.
    /// </summary>
    public interface IEndGamePopUp
    {
        event Action OnNextLevelInput;
        event Action OnQuitInput;
        event Action OnReplayInput;

        void Show(int score, int stars = 0);
        void Hide();
    }
}