using Config;
using Cysharp.Threading.Tasks;
using Model.Objects;
using Model.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
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
        private readonly IBoosterService boosterService;

        public LoadGameState(IStateMachine stateMachine,
            IConfigProvider configProvider,
            ICurrencyService currencyService,
            IBoosterService boosterService)
        {
            this.stateMachine = stateMachine;
            this.configProvider = configProvider;
            this.currencyService = currencyService;
            this.boosterService = boosterService;
        }

        public async UniTask OnEnter(CancellationToken token)
        {
            Application.targetFrameRate = 60; //TODO вынести в настройки

            //загрузка игры
            LoadCurrencies();
            LoadBoosters();

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
            List<CurrencySO> defaultCurrencies = configProvider.Defaults.Currencies.currencies;
            for (int i = 0; i < defaultCurrencies.Count; i++)
            {
                CurrencyType type = defaultCurrencies[i].type;
                int amount = defaultCurrencies[i].defaultAmount;
                currencyService.AddCurrency(type, amount);
            }
        }

        private void LoadBoosters()
        {
            //TODO load save game, else =>
            //loading defaults
            List<BoosterSO> defaultBoosters = configProvider.Defaults.Boosters.boosters;
            for (int i = 0; i < defaultBoosters.Count; i++)
            {
                int id = defaultBoosters[i].booster.Id;
                int amount = defaultBoosters[i].InitialAmount;
                boosterService.AddBooster(id, amount);
            }
        }
    }
}