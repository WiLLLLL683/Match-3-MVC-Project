using Config;
using Model.Factories;
using Model.Infrastructure;
using Model.Objects;
using UnityEngine;
using View;
using Zenject;

namespace CompositionRoot
{
    [CreateAssetMenu(fileName = "ConfigInstaller", menuName = "Installers/ConfigInstaller")]
    public class ConfigInstaller : ScriptableObjectInstaller<ConfigInstaller>
    {
        [SerializeField] private LevelSetSO allLevels;
        [SerializeField] private BlockTypeSetSO allBlockTypes;
        [SerializeField] private BlockView blockViewPrefab;
        [SerializeField] private CellTypeSetSO allCellTypes;
        [SerializeField] private CurrencySetSO allCurrencies;
        [SerializeField] private CounterTargetSetSO allCounterTargets;

        public override void InstallBindings()
        {
            ConfigProvider configProvider = new(allBlockTypes, blockViewPrefab, allCellTypes, allCounterTargets, allCurrencies, allLevels);
            Container.Bind<IConfigProvider>().FromInstance(configProvider).AsSingle();
        }
    }
}