
namespace View
{
    /// <summary>
    /// Меню окончания игры.
    /// </summary>
    public interface IEndGamePopUp : IPopUpView
    {
        public void UpdateScore(int score, int stars = 0);
    }
}