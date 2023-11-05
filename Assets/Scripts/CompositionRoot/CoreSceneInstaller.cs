using Config;
using Infrastructure;
using Model.Infrastructure;
using Model.Objects;
using Presenter;
using System;
using UnityEngine;
using View;
using Zenject;

namespace CompositionRoot
{
    public class CoreSceneInstaller : MonoInstaller
    {
        [SerializeField] private AInput input;

        [Header("Screens")]
        [SerializeField] private AHudView hudView;
        [SerializeField] private AGameBoardView gameBoardView;
        [SerializeField] private ABoosterInventoryView boosterInventoryView;
        [SerializeField] private APauseView pauseView;
        [SerializeField] private AEndGameView endGameView;

        [Header("Prefabs")]
        [SerializeField] private ACounterView goalCounterPrefab;
        [SerializeField] private ACounterView restrictionCounterPrefab;
        [SerializeField] private ABlockView blockPrefab;
        [SerializeField] private ACellView cellPrefab;
        [SerializeField] private ACellView notPlayableCellPrefab;
        [SerializeField] private ABoosterView boosterPrefab;

        private Game game;
        private LevelLoader sceneLoader;

        [Inject]
        public void Construct(Game game, LevelLoader sceneLoader)
        {
            this.game = game;
            this.sceneLoader = sceneLoader;
        }

        public override void InstallBindings()
        {
            BindInput();
            BindCurrentLevel();
            BindHud();
            BindGameboard();
            BindBoosterInventory();
            BindPause();
            BindEndGame();
        }

        private void BindInput()
        {
            Container.BindInstance(input).AsSingle();
        }

        private void BindCurrentLevel()
        {
            Container.BindInstance(game.CurrentLevel).AsSingle();
            Container.BindInstance(game.CurrentLevel.gameBoard).AsSingle();
            Container.BindInstance(sceneLoader.CurrentLevel.blockTypeSet).AsSingle();
        }

        private void BindHud()
        {
            Container.Bind<AHudView>().FromInstance(hudView).AsSingle();
            Container.BindFactory<CounterPresenter, CounterPresenter.Factory>();
            Container.BindFactory<CounterView, CounterView.Factory>()
                .WithId("goalViewFactory")
                .FromComponentInNewPrefab(goalCounterPrefab)
                .UnderTransform(hudView.GoalsParent);
            Container.BindFactory<CounterView, CounterView.Factory>()
                .WithId("restrictionViewFactory")
                .FromComponentInNewPrefab(restrictionCounterPrefab)
                .UnderTransform(hudView.RestrictionsParent);
            Container.Bind<IHudPresenter>().To<HudPresenter>().AsSingle();
        }

        private void BindGameboard()
        {
            Container.Bind<AGameBoardView>().FromInstance(gameBoardView).AsSingle();
            Container.BindFactory<Block, ABlockView, BlockTypeSO, BlockTypeSetSO, BlockPresenter, BlockPresenter.Factory>();
            Container.BindFactory<Cell, ACellView, CellTypeSO, CellTypeSetSO, CellPresenter, CellPresenter.Factory>();
            Container.BindFactory<BlockView, BlockView.Factory>()
                .FromComponentInNewPrefab(blockPrefab)
                .UnderTransform(gameBoardView.BlocksParent);
            Container.BindFactory<CellView, CellView.Factory>()
                .WithId("cellViewFactory")
                .FromComponentInNewPrefab(cellPrefab)
                .UnderTransform(gameBoardView.CellsParent);
            Container.BindFactory<CellView, CellView.Factory>()
                .WithId("notPlayableCellViewFactory")
                .FromComponentInNewPrefab(notPlayableCellPrefab)
                .UnderTransform(gameBoardView.CellsParent);
            Container.Bind<IGameBoardPresenter>().To<GameBoardPresenter>().AsSingle();
        }

        private void BindBoosterInventory()
        {
            Container.Bind<ABoosterInventoryView>().FromInstance(boosterInventoryView).AsSingle();
            Container.BindFactory<BoosterPresenter, BoosterPresenter.Factory>();
            Container.BindFactory<BoosterView, BoosterView.Factory>()
                .FromComponentInNewPrefab(boosterPrefab)
                .UnderTransform(boosterInventoryView.BoostersParent);
            Container.Bind<IBoosterInventoryPresenter>().To<BoosterInventoryPresenter>().AsSingle();
        }

        private void BindPause()
        {
            Container.Bind<APauseView>().FromInstance(pauseView).AsSingle();
            Container.Bind<IPausePresenter>().To<PausePresenter>().AsSingle();
        }

        private void BindEndGame()
        {
            Container.Bind<AEndGameView>().FromInstance(endGameView).AsSingle();
            Container.Bind<IEndGamePresenter>().To<EndGamePresenter>().AsSingle();
        }
    }
}