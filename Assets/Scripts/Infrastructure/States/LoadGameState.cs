using Config;
using Model.Services;
using System;
using System.Collections;
using Utils;

namespace Infrastructure
{
    public class LoadGameState : IState
    {
        private readonly IGameStateMachine gameStateMachine;
        private readonly IConfigProvider configProvider;
        private readonly ICurrencyService currencyService;

        public LoadGameState(IGameStateMachine gameStateMachine, IConfigProvider configProvider, ICurrencyService currencyService)
        {
            this.gameStateMachine = gameStateMachine;
            this.configProvider = configProvider;
            this.currencyService = currencyService;
        }

        public IEnumerator OnEnter()
        {
            //загрузка игры
            LoadCurrencies();

            gameStateMachine.EnterState<MetaState>();
            yield break;
        }

        public IEnumerator OnExit()
        {
            yield break;
        }

        private void LoadCurrencies()
        {
            currencyService.ClearAllCurrencies();

            //TODO load save game, else =>
            //loading defaults
            var allCurrencies = configProvider.GetAllCurrenciesSO();
            for (int i = 0; i < allCurrencies.Count; i++)
            {
                var type = allCurrencies[i].type;
                var amount = allCurrencies[i].defaultAmount;
                currencyService.AddCurrency(type, amount);
            }
        }
    }
}