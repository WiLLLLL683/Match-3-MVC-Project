using System;

namespace View
{
    /// <summary>
    /// Меню окончания игры.
    /// </summary>
    public interface IEndGameMenu
    {
        public event Action OnNextLevelInput;
        public event Action OnQuitInput;
        public event Action OnReplayInput;

        public void UpdateScore(int score, int stars = 0);
    }
}