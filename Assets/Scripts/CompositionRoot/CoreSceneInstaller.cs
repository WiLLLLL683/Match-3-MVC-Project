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
        [SerializeField] private Input_Touch input;

        [Header("Screens")]
        [SerializeField] private HudView hudView;
        [SerializeField] private GameBoardView gameBoardView;
        [SerializeField] private BoosterInventoryView boosterInventoryView;
        [SerializeField] private PauseView pauseView;
        [SerializeField] private EndGameView endGameView;

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
            Container.Bind<IInput>().FromInstance(input).AsSingle();
        }

        private void BindCurrentLevel()
        {
            Container.BindInstance(game.CurrentLevel).AsSingle();
            Container.BindInstance(game.CurrentLevel.gameBoard).AsSingle();
            Container.BindInstance(sceneLoader.CurrentLevel.blockTypeSet).AsSingle();
        }

        private void BindHud()
        {
            Container.Bind<IHudView>().FromInstance(hudView).AsSingle();
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
            Container.Bind<IGameBoardView>().FromInstance(gameBoardView).AsSingle();
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
            Container.Bind<IBoosterInventoryView>().FromInstance(boosterInventoryView).AsSingle();
            Container.BindFactory<BoosterPresenter, BoosterPresenter.Factory>();
            Container.BindFactory<BoosterView, BoosterView.Factory>()
                .FromComponentInNewPrefab(boosterPrefab)
                .UnderTransform(boosterInventoryView.BoostersParent);
            Container.Bind<IBoosterInventoryPresenter>().To<BoosterInventoryPresenter>().AsSingle();
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