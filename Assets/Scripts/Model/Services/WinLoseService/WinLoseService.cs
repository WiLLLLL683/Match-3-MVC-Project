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

        public void TryIncreaseCount(ICounterTarget target, int amount = 1)
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

        public void TryDecreaseCount(ICounterTarget target, int amount = 1)
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

        public bool TryGetGoal(ICounterTarget target, out Counter counter) => FindCounter(Level.goals, target, out counter);
        public bool TryGetRestriction(ICounterTarget target, out Counter counter) => FindCounter(Level.restrictions, target, out counter);

        private bool FindCounter(Counter[] listOfCounters, ICounterTarget target, out Counter counter)
        {
            for (int i = 0; i < listOfCounters.Length; i++)
            {
                counter = listOfCounters[i];

                if (!counter.IsCompleted &&
                    target.GetType() == counter.Target.GetType() &&
                    target.Id == counter.Target.Id)
                {
                    return true;
                }
            }

            counter = null;
            return false;
        }
    }
}