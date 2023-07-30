using System;

namespace View
{
    /// <summary>
    /// Меню окончания игры.
    /// </summary>
    public abstract class AEndGamePopUp : PopUp
    {
        public abstract void UpdateScore(int score, int stars = 0);
    }
}