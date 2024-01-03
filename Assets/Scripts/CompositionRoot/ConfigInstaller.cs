using Config;
using UnityEngine;
using View;
using Zenject;

namespace CompositionRoot
{
    [CreateAssetMenu(fileName = "ConfigInstaller", menuName = "Installers/ConfigInstaller")]
    public class ConfigInstaller : ScriptableObjectInstaller<ConfigInstaller>
    {
        [Header("Sets")]
        [SerializeField] private LevelSetSO allLevels;
        [SerializeField] private BlockTypeSetSO allBlockTypes;
        [SerializeField] private CellTypeSetSO allCellTypes;
        [SerializeField] private CurrencySetSO allCurrencies;
        [SerializeField] private CounterTargetSetSO allCounterTargets;
        [SerializeField] private BoosterSetSO allBoosters;
        [Header("Prefabs")]
        [SerializeField] private PrefabConfig prefabs;
        [Header("Other Config")]
        [SerializeField] private DelayConfig delays;

        public override void InstallBindings()
        {
            ConfigProvider configProvider = new(allBlockTypes, allCellTypes, allCounterTargets, allCurrencies, allLevels, allBoosters, delays, prefabs);
            Container.Bind<IConfigProvider>().FromInstance(configProvider).AsSingle();
        }
    }
}