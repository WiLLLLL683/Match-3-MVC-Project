using Model.Objects;
using System;

namespace Model.Services
{
    public class WinLoseService : IWinLoseService
    {
        private readonly Game game;
        private readonly ICounterService counterService;

        private Level Level => game.CurrentLevel;

        public event Action OnWin;
        public event Action OnLose;

        public WinLoseService(Game game, ICounterService counterService)
        {
            this.game = game;
            this.counterService = counterService;
        }

        public bool CheckWin()
        {
            for (int i = 0; i < Level.goals.Length; i++)
            {
                if (!counterService.CheckCompletion(Level.goals[i]))
                    return false;
            }

            return true;
        }

        public bool CheckLose()
        {
            for (int i = 0; i < Level.restrictions.Length; i++)
            {
                if (counterService.CheckCompletion(Level.restrictions[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public void RaiseWinEvent() => OnWin?.Invoke();
        public void RaiseLoseEvent() => OnLose?.Invoke();

        public void IncreaseCountIfPossible(ICounterTarget target, int amount = 1)
        {
            for (int i = 0; i < Level.goals.Length; i++)
            {
                counterService.IncreaseCount(Level.goals[i], target, amount);
            }

            for (int i = 0; i < Level.restrictions.Length; i++)
            {
                counterService.IncreaseCount(Level.restrictions[i], target, amount);
            }
        }

        public void DecreaseCountIfPossible(ICounterTarget target, int amount = 1)
        {
            for (int i = 0; i < Level.goals.Length; i++)
            {
                counterService.DecreaseCount(Level.goals[i], target, amount);
            }

            for (int i = 0; i < Level.restrictions.Length; i++)
            {
                counterService.DecreaseCount(Level.restrictions[i], target, amount);
            }
        }
    }
}