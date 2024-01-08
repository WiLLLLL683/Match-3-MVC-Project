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
        [Header("Input")]
        [SerializeField] private Input_Touch input;
        [Header("Views")]
        [SerializeField] private HudView hudView;
        [SerializeField] private GameBoardView gameBoardView;
        [SerializeField] private BoostersView boostersView;
        [SerializeField] private PauseView pauseView;
        [SerializeField] private EndGameView endGameView;

        //TODO перенести в ConfigInstaller
        [Header("Prefabs")]
        [SerializeField] private BoosterButtonView boosterPrefab;

        public override void InstallBindings()
        {
            BindInput();
            BindHud();
            BindGameboard();
            BindBoosters();
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

        private void BindBoosters()
        {
            Container.Bind<IBoostersView>().FromInstance(boostersView).AsSingle();
            Container.Bind<IBoostersPresenter>().To<BoostersPresenter>().AsSingle();

            //factories
            Container.Bind<IBoosterButtonFactory>().To<BoosterButtonFactory>().AsSingle();
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