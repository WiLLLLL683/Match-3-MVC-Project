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
        private IConfigProvider configProvider;
        private ICurrencyService currencyService;
        private IHeaderPresenter header;
        private ILevelSelectionPresenter levelSelection;

        [Inject]
        public void Construct(IConfigProvider configProvider,
            ICurrencyService currencyService,
            IHeaderPresenter header,
            ILevelSelectionPresenter levelSelection)
        {
            this.configProvider = configProvider;
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