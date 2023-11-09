using Config;
using Model.Objects;
using Model.Services;
using Presenter;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    /// <summary>
    /// Точка входа для сцены мета-игры
    /// Запускает все презентеры на сцене
    /// </summary>
    public class MetaBootstrap : MonoBehaviour
    {
        private CurrencySetSO currencyConfig;
        private ICurrencyService currencyService;
        private IHeaderPresenter header;
        private ILevelSelectionPresenter levelSelection;

        [Inject]
        public void Construct(CurrencySetSO currencyConfig,
            ICurrencyService currencyService,
            IHeaderPresenter header,
            ILevelSelectionPresenter levelSelection)
        {
            this.currencyConfig = currencyConfig;
            this.currencyService = currencyService;
            this.header = header;
            this.levelSelection = levelSelection;
        }

        private void Start()
        {
            //загрузка игры
            LoadCurrencies();

            header.Enable();
            levelSelection.Enable();
        }

        private void OnDestroy()
        {
            header?.Disable();
            levelSelection?.Disable();
        }

        private void LoadCurrencies()
        {
            currencyService.ClearAllCurrencies();

            //TODO load save game, else =>
            //loading defaults
            for (int i = 0; i < currencyConfig.currencies.Count; i++)
            {
                var type = currencyConfig.currencies[i].type;
                var amount = currencyConfig.currencies[i].defaultAmount;
                currencyService.AddCurrency(type, amount);
            }
        }
    }
}