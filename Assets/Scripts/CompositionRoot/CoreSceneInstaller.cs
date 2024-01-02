using System;
using Presenter;
using UnityEngine;
using View;
using View.Factories;
using Zenject;

namespace CompositionRoot
{

    public class CoreSceneInstaller : MonoInstaller
    {
        [SerializeField] private Input_Touch input;

        [Header("Screens")]
        [SerializeField] private HudView hudView;
        [SerializeField] private GameBoardView gameBoardView;
        [SerializeField] private BoosterInventoryView boosterInventoryView;
        [SerializeField] private PauseView pauseView;
        [SerializeField] private EndGameView endGameView;

        //TODO перенести в ConfigInstaller
        [Header("Prefabs")]
        [SerializeField] private BoosterView boosterPrefab;

        public override void InstallBindings()
        {
            BindInput();
            BindHud();
            BindGameboard();
            BindBoosterInventory();
            BindPause();
            BindEndGame();
        }

        private void BindInput()
        {
            Container.Bind<IInput>().FromInstance(input).AsSingle();
        }

        private void BindHud()
        {
            Container.Bind<IHudView>().FromInstance(hudView).AsSingle();
            Container.Bind<IHudPresenter>().To<HudPresenter>().AsSingle();

            //factories
            Container.Bind<ICounterViewFactory>().To<CounterViewFactory>().AsSingle();
        }

        private void BindGameboard()
        {
            Container.Bind<IGameBoardView>().FromInstance(gameBoardView).AsSingle();
            Container.Bind<ICellsPresenter>().To<CellsPresenter>().AsSingle();
            Container.Bind<IBlocksPresenter>().To<BlocksPresenter>().AsSingle();

            //factories
            Container.Bind<ICellViewFactory>().To<CellViewFactory>().AsSingle();
            Container.Bind<IBlockViewFactory>().To<BlockViewFactory>().AsSingle();
        }

        private void BindBoosterInventory()
        {
            Container.Bind<IBoosterInventoryView>().FromInstance(boosterInventoryView).AsSingle();
            Container.Bind<IBoosterInventoryPresenter>().To<BoosterInventoryPresenter>().AsSingle();

            //factories //TODO заменить на свою фабрику и префаб переместить в ConfigProvider
            Container.BindFactory<BoosterPresenter, BoosterPresenter.Factory>();
            Container.BindFactory<BoosterView, BoosterView.Factory>()
                .FromComponentInNewPrefab(boosterPrefab)
                .UnderTransform(boosterInventoryView.BoostersParent);
        }

        private void BindPause()
        {
            Container.Bind<IPauseView>().FromInstance(pauseView).AsSingle();
            Container.Bind<IPausePresenter>().To<PausePresenter>().AsSingle();
        }
        private void BindEndGame()
        {
            Container.Bind<IEndGameView>().FromInstance(endGameView).AsSingle();
            Container.Bind<IEndGamePresenter>().To<EndGamePresenter>().AsSingle();
        }
    }
}