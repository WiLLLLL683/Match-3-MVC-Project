using Config;
using Model.Infrastructure;
using Model.Objects;
using Model.Services;
using NSubstitute;
using NUnit.Framework;
using System;
using TestUtils;
using UnityEngine;
using UnityEngine.TestTools;
using View;
using View.Factories;

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

        private const string DESTROY_ERROR_LOG = "Destroy may not be called from edit mode! Use DestroyImmediate instead.\nDestroying an object in edit mode destroys it permanently.";

        private class SetupArgs
        {
            public Game model;
            public BlocksPresenter presenter;
            public IBlockSpawnService spawnService;
            public IBlockDestroyService destroyService;
            public IBlockMoveService moveService;
            public IBlockChangeTypeService changeTypeService;
            public IBlockView blockView;
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
            blockView.When(x => x.PlayDestroyEffect()).Do(x => blockDestroyedCount++);
            blockView.When(x => x.ChangeModelPosition(Arg.Any<Vector2Int>())).Do(x => blockChangedPositionCount++);
            blockView.When(x => x.ChangeType(Arg.Any<Sprite>(), Arg.Any<ParticleSystem>())).Do(x => blockChangedTypeCount++);

            //modelInput
            var modelInput = Substitute.For<IModelInput>();
            modelInput.When(x => x.MoveBlock(Arg.Any<Vector2Int>(), Arg.Any<Directions>())).Do(x => inputMoveCount++);
            modelInput.When(x => x.ActivateBlock(Arg.Any<Vector2Int>())).Do(x => inputActivateCount++);

            //config
            var configProvider = Substitute.For<IBlockTypeConfigProvider>();
            var config = Substitute.For<BlockTypeSO>();
            configProvider.GetSO(Arg.Any<int>()).Returns(config);

            var spawnService = Substitute.For<IBlockSpawnService>();
            var destroyService = Substitute.For<IBlockDestroyService>();
            var changeTypeService = Substitute.For<IBlockChangeTypeService>();
            var moveService = Substitute.For<IBlockMoveService>();
            var presenter = new BlocksPresenter(model, view, blockViewFactory, configProvider, spawnService, destroyService, changeTypeService, moveService, modelInput);

            return new SetupArgs()
            {
                model = model,
                presenter = presenter,
                spawnService = spawnService,
                destroyService = destroyService,
                moveService = moveService,
                changeTypeService = changeTypeService,
                blockView = blockView
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

            LogAssert.Expect(LogType.Error, DESTROY_ERROR_LOG);
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

            setup.blockView.OnInputMove += Raise.Event<Action<Vector2Int,Directions>>(new Vector2Int(0,0), Directions.Right);

            Assert.AreEqual(1, inputMoveCount);
        }

        [Test]
        public void SpawnedBlock_InputActivate_BlockActivated()
        {
            var setup = Setup();
            setup.presenter.Enable();

            setup.blockView.OnInputActivate += Raise.Event<Action<Vector2Int>>(new Vector2Int(0, 0));

            Assert.AreEqual(1, inputActivateCount);
        }

        [Test]
        public void DestroyedBlock_InputMove_NoChange()
        {
            var setup = Setup();
            setup.presenter.Enable();
            setup.presenter.Disable();

            setup.blockView.OnInputMove += Raise.Event<Action<Vector2Int, Directions>>(new Vector2Int(0, 0), Directions.Right);

            LogAssert.Expect(LogType.Error, DESTROY_ERROR_LOG);
            Assert.AreEqual(0, inputMoveCount);
        }

        [Test]
        public void DestroyedBlock_InputActivate_NoChange()
        {
            var setup = Setup();
            setup.presenter.Enable();
            setup.presenter.Disable();

            setup.blockView.OnInputActivate += Raise.Event<Action<Vector2Int>>(new Vector2Int(0, 0));

            LogAssert.Expect(LogType.Error, DESTROY_ERROR_LOG);
            Assert.AreEqual(0, inputActivateCount);
        }
    }
}