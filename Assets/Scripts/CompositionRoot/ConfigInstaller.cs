using Config;
using Model.Factories;
using Model.Infrastructure;
using Model.Objects;
using UnityEngine;
using Zenject;

namespace CompositionRoot
{
    [CreateAssetMenu(fileName = "ConfigInstaller", menuName = "Installers/ConfigInstaller")]
    public class ConfigInstaller : ScriptableObjectInstaller<ConfigInstaller>
    {
        [SerializeField] private LevelSetSO allLevels;
        [SerializeField] private BlockTypeSetSO allBlockTypes;
        [SerializeField] private CellTypeSetSO allCellTypes;
        [SerializeField] private CurrencySetSO allCurrencies;
        [SerializeField] private CounterTargetSetSO allCounterTargets;

        public override void InstallBindings()
        {
            BindLevels();
            BindBlockTypes();
            BindCellTypes();
            BindCurrencies();
            BindCounterTargets();
        }

        private void BindLevels()
        {
            Container.Bind<ILevelConfigProvider>().FromInstance(allLevels).AsSingle();
        }

        private void BindBlockTypes()
        {
            Container.Bind<IBlockTypeConfigProvider>().FromInstance(allBlockTypes).AsSingle();
        }

        private void BindCellTypes()
        {
            Container.Bind<ICellTypeConfigProvider>().FromInstance(allCellTypes).AsSingle();
        }

        private void BindCurrencies()
        {
            Container.Bind<ICurrencyConfigProvider>().FromInstance(allCurrencies).AsSingle();
        }

        private void BindCounterTargets()
        {
            Container.Bind<ICounterTargetConfigProvider>().FromInstance(allCounterTargets).AsSingle();
            Container.Bind<ICounterTarget>()
                .FromInstance(allCounterTargets.turnSO.CounterTarget)
                .AsSingle()
                .WhenInjectedInto<DestroyState>();
        }
    }
}