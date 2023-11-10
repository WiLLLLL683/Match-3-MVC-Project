using Config;
using Model.Factories;
using Model.Objects;
using UnityEngine;
using Zenject;

namespace CompositionRoot
{
    [CreateAssetMenu(fileName = "ConfigInstaller", menuName = "Installers/ConfigInstaller")]
    public class ConfigInstaller : ScriptableObjectInstaller<ConfigInstaller>
    {
        [SerializeField] private LevelSO[] allLevels;
        [SerializeField] private CellTypeSetSO allCellTypes;
        [SerializeField] private CurrencySetSO allCurrencies;

        public override void InstallBindings()
        {
            Container.Bind<LevelSO[]>().FromInstance(allLevels).AsSingle();
            Container.Bind<CellTypeSetSO>().FromInstance(allCellTypes).AsSingle();
            Container.Bind<CellType>().FromInstance(allCellTypes.invisibleCellType.type).AsSingle().WhenInjectedInto<CellFactory>();
            Container.Bind<ICurrencyConfigProvider>().FromInstance(allCurrencies).AsSingle();
        }
    }
}