using Config;
using Model.Objects;
using Model.Services;
using NSubstitute;
using NUnit.Framework;
using System;
using TestUtils;
using UnityEngine;
using View;
using View.Factories;

namespace Presenter.UnitTests
{
    public class CellsPresenterTests
    {
        private int cellSpawnedCount;
        private int cellDestroyedCount;
        private int cellEmptiedCount;
        private int cellChangedTypeCount;

        private class SetupArgs
        {
            public Game model;
            public CellsPresenter presenter;
            public ICellSetBlockService setBlockService;
            public ICellChangeTypeService changeTypeService;
            public ICellDestroyService cellDestroyService;
        }

        private SetupArgs Setup()
        {
            cellSpawnedCount = 0;
            cellDestroyedCount = 0;
            cellEmptiedCount = 0;
            cellChangedTypeCount = 0;

            var model = TestLevelFactory.CreateGame(1, 1);

            //view
            var view = Substitute.For<IGameBoardView>();
            var cellParent = new GameObject().transform;
            view.CellsParent.Returns(cellParent);

            //factory
            var cellViewFactory = Substitute.For<ICellViewFactory>();
            var cellView = Substitute.For<ICellView>();
            cellViewFactory.Create(Arg.Any<Cell>()).Returns(cellView);
            cellViewFactory.When(x => x.Create(Arg.Any<Cell>())).Do(x => cellSpawnedCount++);
            cellView.When(x => x.PlayDestroyEffect()).Do(x => cellDestroyedCount++);
            cellView.When(x => x.PlayEmptyEffect()).Do(x => cellEmptiedCount++);
            cellView.When(x => x.ChangeType(Arg.Any<Sprite>(), Arg.Any<bool>(), Arg.Any<ParticleSystem>(), Arg.Any<ParticleSystem>())).Do(x => cellChangedTypeCount++);

            //config
            var configProvider = Substitute.For<ICellTypeConfigProvider>();
            var config = Substitute.For<CellTypeSO>();
            configProvider.GetSO(Arg.Any<int>()).Returns(config);

            var setBlockService = Substitute.For<ICellSetBlockService>();
            var changeTypeService = Substitute.For<ICellChangeTypeService>();
            var cellDestroyService = Substitute.For<ICellDestroyService>();
            var validationService = new ValidationService(model);
            var presenter = new CellsPresenter(model, view, cellViewFactory, configProvider, setBlockService, changeTypeService, cellDestroyService, validationService);

            return new SetupArgs()
            {
                model = model,
                presenter = presenter,
                setBlockService = setBlockService,
                changeTypeService = changeTypeService,
                cellDestroyService = cellDestroyService
            };
        }

        [Test]
        public void Enable_CellsSpawned()
        {
            var setup = Setup();

            setup.presenter.Enable();

            Assert.AreEqual(1, cellSpawnedCount);
        }

        [Test]
        public void Enable_OnDestroy_CellDestroyed()
        {
            var setup = Setup();
            var cell = setup.model.CurrentLevel.gameBoard.Cells[0, 0];
            setup.presenter.Enable();

            setup.cellDestroyService.OnDestroy += Raise.Event<Action<Cell>>(cell);

            Assert.AreEqual(1, cellDestroyedCount);
        }

        [Test]
        public void Enable_OnEmpty_CellEmpty()
        {
            var setup = Setup();
            var cell = setup.model.CurrentLevel.gameBoard.Cells[0, 0];
            setup.presenter.Enable();

            setup.setBlockService.OnEmpty += Raise.Event<Action<Cell>>(cell);

            Assert.AreEqual(1, cellEmptiedCount);
        }

        [Test]
        public void Enable_OnChangeType_CellChangedType()
        {
            var setup = Setup();
            var cell = setup.model.CurrentLevel.gameBoard.Cells[0, 0];
            setup.presenter.Enable();

            setup.changeTypeService.OnTypeChange += Raise.Event<Action<Cell>>(cell);

            Assert.AreEqual(1, cellChangedTypeCount);
        }

        [Test]
        public void Disable_OnDestroy_CellDestroyed()
        {
            var setup = Setup();
            var cell = setup.model.CurrentLevel.gameBoard.Cells[0, 0];
            setup.presenter.Enable();
            setup.presenter.Disable();

            setup.cellDestroyService.OnDestroy += Raise.Event<Action<Cell>>(cell);

            Assert.AreEqual(0, cellDestroyedCount);
        }

        [Test]
        public void Disable_OnEmpty_CellEmpty()
        {
            var setup = Setup();
            var cell = setup.model.CurrentLevel.gameBoard.Cells[0, 0];
            setup.presenter.Enable();
            setup.presenter.Disable();

            setup.setBlockService.OnEmpty += Raise.Event<Action<Cell>>(cell);

            Assert.AreEqual(0, cellEmptiedCount);
        }

        [Test]
        public void Disable_OnChangeType_CellChangedType()
        {
            var setup = Setup();
            var cell = setup.model.CurrentLevel.gameBoard.Cells[0, 0];
            setup.presenter.Enable();
            setup.presenter.Disable();

            setup.changeTypeService.OnTypeChange += Raise.Event<Action<Cell>>(cell);

            Assert.AreEqual(0, cellChangedTypeCount);
        }

        [Test]
        public void GetCellView_CellExists_ReturnCellView()
        {
            var setup = Setup();
            setup.presenter.Enable();

            var cellView = setup.presenter.GetCellView(new(0,0));

            Assert.AreNotEqual(null, cellView);
        }

        [Test]
        public void GetCellView_InvalidPosition_ReturnNull()
        {
            var setup = Setup();
            setup.presenter.Enable();

            var cellView = setup.presenter.GetCellView(new(100, 100));

            Assert.AreEqual(null, cellView);
        }
    }
}