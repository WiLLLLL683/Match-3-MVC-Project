using Config;
using Infrastructure;
using Model.Infrastructure;
using Model.Objects;
using Presenter;
using System;
using UnityEngine;
using Utils;
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
        [SerializeField] private CounterView goalCounterPrefab;
        [SerializeField] private CounterView restrictionCounterPrefab;
        [SerializeField] private BlockView blockPrefab;
        [SerializeField] private CellView cellPrefab;
        [SerializeField] private CellView notPlayableCellPrefab;
        [SerializeField] private BoosterView boosterPrefab;

        private Game game;
        private ILevelLoader levelLoader;

        [Inject]
        public void Construct(Game game, ILevelLoader sceneLoader)
        {
            this.game = game;
            this.levelLoader = sceneLoader;
        }

        public override void InstallBindings()
        {
            BindModelStateMachine();
            BindInput();
            BindHud();
            BindGameboard();
            BindBoosterInventory();
            BindPause();
            BindEndGame();
        }

        private void BindModelStateMachine()
        {
            Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();
            Container.Bind<IStateMachine>().To<StateMachine>().AsSingle();
        }

        private void BindInput()
        {
            Container.Bind<IInput>().FromInstance(input).AsSingle();
            Container.Bind<IModelInput>().To<ModelInput>().AsSingle();
        }

        private void BindHud()
        {
            Container.Bind<IHudView>().FromInstance(hudView).AsSingle();
            Container.Bind<IHudPresenter>().To<HudPresenter>().AsSingle();

            //factories
            Container.BindFactory<CounterView, CounterView.Factory>()
                .WithId(ViewFactoryId.Goal)
                .FromComponentInNewPrefab(goalCounterPrefab)
                .UnderTransform(hudView.GoalsParent);
            Container.BindFactory<CounterView, CounterView.Factory>()
                .WithId(ViewFactoryId.Restriction)
                .FromComponentInNewPrefab(restrictionCounterPrefab)
                .UnderTransform(hudView.RestrictionsParent);
        }

        private void BindGameboard()
        {
            Container.Bind<IGameBoardView>().FromInstance(gameBoardView).AsSingle();
            Container.Bind<IGameBoardPresenter>().To<GameBoardPresenter>().AsSingle();

            //factories
            Container.BindFactory<Block, IBlockView, BlockTypeSO, BlockTypeSetSO, BlockPresenter, BlockPresenter.Factory>();
            Container.BindFactory<Cell, ICellView, CellTypeSO, CellTypeSetSO, CellPresenter, CellPresenter.Factory>();
            Container.BindFactory<BlockView, BlockView.Factory>()
                .FromComponentInNewPrefab(blockPrefab)
                .UnderTransform(gameBoardView.BlocksParent);
            Container.BindFactory<CellView, CellView.Factory>()
                .WithId(ViewFactoryId.Cell)
                .FromComponentInNewPrefab(cellPrefab)
                .UnderTransform(gameBoardView.CellsParent);
            Container.BindFactory<CellView, CellView.Factory>()
                .WithId(ViewFactoryId.CellNotPlayable)
                .FromComponentInNewPrefab(notPlayableCellPrefab)
                .UnderTransform(gameBoardView.CellsParent);
        }

        private void BindBoosterInventory()
        {
            Container.Bind<IBoosterInventoryView>().FromInstance(boosterInventoryView).AsSingle();
            Container.Bind<IBoosterInventoryPresenter>().To<BoosterInventoryPresenter>().AsSingle();

            //factories
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