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
        [SerializeField] private InputConfig input;

        public override void InstallBindings()
        {
            ConfigProvider configProvider = new(allSets, delays, prefabs, defaults, input);
            Container.Bind<IConfigProvider>().FromInstance(configProvider).AsSingle();
        }
    }
}