using Config;
using UnityEngine;
using Zenject;

namespace CompositionRoot
{
    [CreateAssetMenu(fileName = "ConfigInstaller", menuName = "Installers/ConfigInstaller")]
    public class ConfigInstaller : ScriptableObjectInstaller<ConfigInstaller>
    {
        [SerializeField] private AllSetsConfig allSets;
        [SerializeField] private DefaultsConfig defaults;
        [SerializeField] private PrefabConfig prefabs;
        [SerializeField] private DelayConfig delays;

        public override void InstallBindings()
        {
            ConfigProvider configProvider = new(allSets, delays, prefabs, defaults);
            Container.Bind<IConfigProvider>().FromInstance(configProvider).AsSingle();
        }
    }
}