using Presenter;
using UnityEngine;
using View;
using Zenject;

namespace CompositionRoot
{
    public class MetaSceneInstaller : MonoInstaller
    {
        [Header("Screens")]
        [SerializeField] private HeaderView headerView;
        [SerializeField] private LevelSelectionView levelSelectionView;

        [Header("Prefabs")]
        [SerializeField] private CounterView scoreCounterPrefab;

        public override void InstallBindings()
        {
            BindHeader();
            BindLevelSelector();
        }

        private void BindHeader()
        {
            Container.Bind<IHeaderView>().FromInstance(headerView).AsSingle();
            Container.Bind<IHeaderPresenter>().To<HeaderPresenter>().AsSingle();
        }

        private void BindLevelSelector()
        {
            Container.Bind<ILevelSelectionView>().FromInstance(levelSelectionView).AsSingle();
            Container.Bind<ILevelSelectionPresenter>().To<LevelSelectionPresenter>().AsSingle();
        }
    }
}