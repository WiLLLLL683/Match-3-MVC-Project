using Model.Objects;
using System;

namespace Model.Services
{
    public class WinLoseService : IWinLoseService
    {
        private readonly ICounterService counterService;

        private Level level;

        public event Action OnWin;
        public event Action OnLose;

        public WinLoseService(ICounterService counterService)
        {
            this.counterService = counterService;
        }

        public void SetLevel(Level level) => this.level = level;

        public bool CheckWin()
        {
            for (int i = 0; i < level.goals.Length; i++)
            {
                if (!counterService.CheckCompletion(level.goals[i]))
                    return false;
            }

            OnWin?.Invoke();
            return true;
        }

        public bool CheckLose()
        {
            for (int i = 0; i < level.restrictions.Length; i++)
            {
                if (counterService.CheckCompletion(level.restrictions[i]))
                {
                    OnLose?.Invoke();
                    return true;
                }
            }

            return false;
        }

        public void UpdateGoals(ICounterTarget target)
        {
            for (int i = 0; i < level.goals.Length; i++)
            {
                counterService.CheckTarget(level.goals[i], target);
            }
        }

        public void UpdateRestrictions(ICounterTarget target)
        {
            for (int i = 0; i < level.restrictions.Length; i++)
            {
                counterService.CheckTarget(level.restrictions[i], target);
            }
        }
    }
}