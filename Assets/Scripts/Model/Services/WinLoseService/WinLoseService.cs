using Model.Objects;

namespace Model.Services
{
    public class WinLoseService : IWinLoseService
    {
        private readonly ICounterService counterService;

        private Level level;
        private Counter[] goals;
        private Counter[] restrictions;

        public WinLoseService(ICounterService counterService)
        {
            this.counterService = counterService;
        }

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

        public void UpdateGoals(ICounterTarget target)
        {
            for (int i = 0; i < goals.Length; i++)
            {
                counterService.CheckTarget(goals[i], target);
            }
        }

        public void UpdateRestrictions(ICounterTarget target)
        {
            for (int i = 0; i < restrictions.Length; i++)
            {
                counterService.CheckTarget(restrictions[i], target);
            }
        }
    }
}