using Config;
using Presenter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using View;
using Zenject;

namespace CompositionRoot
{
    public class MetaSceneInstaller : MonoInstaller
    {
        [Header("Screens")]
        [SerializeField] private AHeaderView headerView;
        [SerializeField] private ALevelSelectionView levelSelectionView;
        [Header("Prefabs")]
        [SerializeField] private CounterView scoreCounterPrefab;

        public override void InstallBindings()
        {
            BindHeader();
            BindLevelSelector();
        }

        private void BindHeader()
        {
            Container.Bind<AHeaderView>().FromInstance(headerView).AsSingle();
            Container.BindFactory<CurrencyPresenter, CurrencyPresenter.Factory>();
            Container.BindFactory<CounterView, CounterView.Factory>()
                .FromComponentInNewPrefab(scoreCounterPrefab)
                .UnderTransform(headerView.ScoreParent);
            Container.BindInterfacesAndSelfTo<HeaderPresenter>().AsSingle();
        }

        private void BindLevelSelector()
        {
            Container.Bind<ALevelSelectionView>().FromInstance(levelSelectionView).AsSingle();
            Container.BindInterfacesAndSelfTo<LevelSelectionPresenter>().AsSingle();
        }
    }
}