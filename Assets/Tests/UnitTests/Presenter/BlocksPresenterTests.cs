using Config;
using Infrastructure;
using Model.Objects;
using Model.Services;
using NSubstitute;
using NUnit.Framework;
using System;
using TestUtils;
using UnityEngine;
using UnityEngine.TestTools;
using Utils;
using View;
using View.Factories;
using View.Input;

namespace Presenter.UnitTests
{
    public class BlocksPresenterTests
    {
        private int blockSpawnedCount;
        private int blockDestroyedCount;
        private int blockChangedPositionCount;
        private int blockChangedTypeCount;
        private int inputMoveCount;
        private int inputActivateCount;

        private class SetupArgs
        {
            public Game model;
            public BlocksPresenter presenter;
            public IBlockSpawnService spawnService;
            public IBlockDestroyService destroyService;
            public IBlockMoveService moveService;
            public IBlockChangeTypeService changeTypeService;
            public IBlockView blockView;
            public IGameBoardInput input;
        }

        private SetupArgs Setup()
        {
            blockSpawnedCount = 0;
            blockDestroyedCount = 0;
            blockChangedPositionCount = 0;
            blockChangedTypeCount = 0;
            inputMoveCount = 0;
            inputActivateCount = 0;

            //model
            var model = TestLevelFactory.CreateGame(2, 1);
            var gameBoard = model.CurrentLevel.gameBoard;
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 0], gameBoard);

            //view
            var view = Substitute.For<IGameBoardView>();
            var blockParent = new GameObject().transform;
            view.BlocksParent.Returns(blockParent);

            //factory
            var blockViewFactory = Substitute.For<IBlockViewFactory>();
            var blockView = Substitute.For<IBlockView>();
            blockViewFactory.Create(Arg.Any<Block>()).Returns(blockView);
            blockViewFactory.When(x => x.Create(Arg.Any<Block>())).Do(x => blockSpawnedCount++);
            blockView.When(x => x.Destroy()).Do(x => blockDestroyedCount++);
            blockView.When(x => x.SetModelPosition(Arg.Any<Vector2Int>())).Do(x => blockChangedPositionCount++);
            blockView.When(x => x.SetType(Arg.Any<Sprite>(), Arg.Any<BlockTypeConfig>())).Do(x => blockChangedTypeCount++);

            //stateMachine
            var stateMachine = Substitute.For<IStateMachine>();
            stateMachine.When(x => x.EnterState<InputMoveBlockState, (Vector2Int, Directions)>(Arg.Any<(Vector2Int, Directions)>())).Do(x => inputMoveCount++);
            stateMachine.When(x => x.EnterState<InputActivateBlockState, Vector2Int>(Arg.Any<Vector2Int>())).Do(x => inputActivateCount++);

            //config
            var configProvider = Substitute.For<IConfigProvider>();
            var config = Substitute.For<BlockTypeSO>();
            configProvider.GetBlockTypeSO(Arg.Any<int>()).Returns(config);

            //input
            var input = Substitute.For<IGameBoardInput>();
            var moveInputMode = Substitute.For<IMoveInputMode>();
            input.GetInputMode<IMoveInputMode>().Returns(moveInputMode);

            var spawnService = Substitute.For<IBlockSpawnService>();
            var destroyService = Substitute.For<IBlockDestroyService>();
            var changeTypeService = Substitute.For<IBlockChangeTypeService>();
            var moveService = Substitute.For<IBlockMoveService>();
            var hudPresenter = Substitute.For<IHudPresenter>();
            var winLoseService = Substitute.For<IWinLoseService>();
            var presenter = new BlocksPresenter(model, view, blockViewFactory, stateMachine, input, configProvider, hudPresenter, spawnService, destroyService, changeTypeService, moveService, winLoseService);

            return new SetupArgs()
            {
                model = model,
                presenter = presenter,
                spawnService = spawnService,
                destroyService = destroyService,
                moveService = moveService,
                changeTypeService = changeTypeService,
                blockView = blockView,
                input = input
            };
        }

        [Test]
        public void Enable_BlocksSpawned()
        {
            var setup = Setup();

            setup.presenter.Enable();

            Assert.AreEqual(1, blockSpawnedCount);
        }

        [Test]
        public void Enable_OnSpawnEvent_BlockSpawned()
        {
            var setup = Setup();
            var gameBoard = setup.model.CurrentLevel.gameBoard;
            var cell = gameBoard.Cells[1, 0];
            setup.presenter.Enable();

            var newBlock = TestBlockFactory.CreateBlockInCell(TestBlockFactory.BLUE_BLOCK, cell, gameBoard);
            setup.spawnService.OnBlockSpawn += Raise.Event<Action<Block>>(newBlock);

            Assert.AreEqual(2, blockSpawnedCount);
        }

        [Test]
        public void Enable_OnDestroyEvent_BlockDestroyed()
        {
            var setup = Setup();
            var block = setup.model.CurrentLevel.gameBoard.Blocks[0];
            setup.presenter.Enable();

            setup.destroyService.OnDestroy += Raise.Event<Action<Block>>(block);

            Assert.AreEqual(1, blockDestroyedCount);
        }

        [Test]
        public void Enable_OnPositionChangeEvent_BlockChangedPosition()
        {
            var setup = Setup();
            var block = setup.model.CurrentLevel.gameBoard.Blocks[0];
            setup.presenter.Enable();

            setup.moveService.OnPositionChange += Raise.Event<Action<Block>>(block);

            Assert.AreEqual(1, blockChangedPositionCount);
        }

        [Test]
        public void Enable_OnChangeTypeEvent_BlockChangedType()
        {
            var setup = Setup();
            var block = setup.model.CurrentLevel.gameBoard.Blocks[0];
            setup.presenter.Enable();

            setup.changeTypeService.OnTypeChange += Raise.Event<Action<Block>>(block);

            Assert.AreEqual(1, blockChangedTypeCount);
        }

        [Test]
        public void SpawnedBlock_InputMove_BlockMoved()
        {
            var setup = Setup();
            setup.presenter.Enable();
            var moveInputMode = setup.input.GetInputMode<IMoveInputMode>();

            moveInputMode.OnInputMove += Raise.Event<Action<IBlockView, Vector2>>(setup.blockView, Vector2.right);

            Assert.AreEqual(1, inputMoveCount);
        }

        [Test]
        public void SpawnedBlock_InputActivate_BlockActivated()
        {
            var setup = Setup();
            setup.presenter.Enable();
            var moveInputMode = setup.input.GetInputMode<IMoveInputMode>();

            moveInputMode.OnInputActivate += Raise.Event<Action<IBlockView>>(setup.blockView);

            Assert.AreEqual(1, inputActivateCount);
        }

        [Test]
        public void DestroyedBlock_InputMove_NoChange()
        {
            var setup = Setup();
            setup.presenter.Enable();
            setup.presenter.Disable();
            var moveInputMode = setup.input.GetInputMode<IMoveInputMode>();

            moveInputMode.OnInputMove += Raise.Event<Action<IBlockView, Vector2>>(setup.blockView, Vector2.right);

            Assert.AreEqual(0, inputMoveCount);
        }

        [Test]
        public void DestroyedBlock_InputActivate_NoChange()
        {
            var setup = Setup();
            setup.presenter.Enable();
            setup.presenter.Disable();
            var moveInputMode = setup.input.GetInputMode<IMoveInputMode>();

            moveInputMode.OnInputActivate += Raise.Event<Action<IBlockView>>(setup.blockView);

            Assert.AreEqual(0, inputActivateCount);
        }
    }
}