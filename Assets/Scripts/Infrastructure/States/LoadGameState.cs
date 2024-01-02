using Config;
using Cysharp.Threading.Tasks;
using Model.Services;
using System;
using System.Collections;
using System.Threading;
using Utils;

namespace Infrastructure
{
    /// <summary>
    /// Стейт для загрузки всей игры, в том числе: сохранения, конфигов
    /// После загрузки переход в MetaState
    /// </summary>
    public class LoadGameState : IState
    {
        private readonly IStateMachine stateMachine;
        private readonly IConfigProvider configProvider;
        private readonly ICurrencyService currencyService;

        public LoadGameState(IStateMachine stateMachine, IConfigProvider configProvider, ICurrencyService currencyService)
        {
            this.stateMachine = stateMachine;
            this.configProvider = configProvider;
            this.currencyService = currencyService;
        }

        public async UniTask OnEnter(CancellationToken token)
        {
            //загрузка игры
            LoadCurrencies();

            stateMachine.EnterState<MetaState>();
        }

        public async UniTask OnExit(CancellationToken token)
        {

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