using Model.Objects;

namespace Model.Services
{
    public class WinLoseService : IWinLoseService
    {
        private Level level;
        private Counter[] goals;
        private Counter[] restrictions;

        public void SetLevel(Level level)
        {
            this.level = level;
            this.goals = level.goals;
            this.restrictions = level.restrictions;
        }

        public bool CheckWin()
        {
            for (int i = 0; i < goals.Length; i++)
            {
                if (goals[i] == null)
                    continue;

                if (!goals[i].IsCompleted)
                    return false;
            }

            level.SetWin();
            return true;
        }

        public bool CheckLose()
        {
            for (int i = 0; i < restrictions.Length; i++)
            {
                if (restrictions[i] == null)
                    continue;

                if (restrictions[i].IsCompleted)
                {
                    level.SetLose();
                    return true;
                }
            }

            return false;
        }

        public void UpdateGoals(ICounterTarget _target)
        {
            for (int i = 0; i < goals.Length; i++)
            {
                goals[i].CheckTarget(_target);
            }
        }

        public void UpdateRestrictions(ICounterTarget _target)
        {
            for (int i = 0; i < restrictions.Length; i++)
            {
                restrictions[i].CheckTarget(_target);
            }
        }
    }
}