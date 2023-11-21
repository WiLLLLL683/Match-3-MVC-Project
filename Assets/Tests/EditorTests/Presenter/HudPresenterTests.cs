using System;
using System.Collections;
using Config;
using Model.Objects;
using Model.Services;
using NSubstitute;
using NUnit.Framework;
using TestUtils;
using UnityEngine;
using View;
using View.Factories;

namespace Presenter.UnitTests
{
    public class HudPresenterTests
    {
        private int viewUpdatedCount;
        private int viewCompletedCount;
        private int goalSpawnedCount;
        private int restrictionSpawnedCount;

        private (HudPresenter presenter, IHudView view, ICounterService service, Game game) Setup()
        {
            var game = TestLevelFactory.CreateGame(1, 1);
            var service = Substitute.For<ICounterService>();

            //view
            var view = Substitute.For<IHudView>();
            var goalsParent = new GameObject().transform;
            view.GoalsParent.Returns(goalsParent);
            var restrictionsParent = new GameObject().transform;
            view.RestrictionsParent.Returns(restrictionsParent);

            //counterViews
            viewUpdatedCount = 0;
            viewCompletedCount = 0;
            var goalView = Substitute.For<ICounterView>();
            var restrictionView = Substitute.For<ICounterView>();
            goalView.When(x => x.ChangeCount(Arg.Any<int>())).Do(x => viewUpdatedCount++);
            goalView.When(x => x.ShowCompleteIcon()).Do(x => viewCompletedCount++);
            restrictionView.When(x => x.ChangeCount(Arg.Any<int>())).Do(x => viewUpdatedCount++);
            restrictionView.When(x => x.ShowCompleteIcon()).Do(x => viewCompletedCount++);

            //factory
            goalSpawnedCount = 0;
            restrictionSpawnedCount = 0;
            var counterViewFactory = Substitute.For<ICounterViewFactory>();
            counterViewFactory.CreateGoal(Arg.Any<Counter>()).Returns(goalView);
            counterViewFactory.CreateRestriction(Arg.Any<Counter>()).Returns(restrictionView);
            counterViewFactory.When(x => x.CreateGoal(Arg.Any<Counter>())).Do(x => goalSpawnedCount++);
            counterViewFactory.When(x => x.CreateRestriction(Arg.Any<Counter>())).Do(x => restrictionSpawnedCount++);

            var presenter = new HudPresenter(game, view, service, counterViewFactory);

            return (presenter, view, service, game);
        }

        [Test]
        public void Enable_GoalsRestrictionsSpawned()
        {
            var (presenter, view, service, game) = Setup();

            presenter.Enable();

            Assert.AreEqual(1, goalSpawnedCount);
            Assert.AreEqual(1, restrictionSpawnedCount);
        }

        [Test]
        public void Enable_OnUpdateEvent_ViewUpdated()
        {
            var (presenter, view, service, game) = Setup();
            presenter.Enable();

            service.OnUpdateEvent += Raise.Event<Action<Counter>>(game.CurrentLevel.goals[0]);
            service.OnUpdateEvent += Raise.Event<Action<Counter>>(game.CurrentLevel.restrictions[0]);

            Assert.AreEqual(2, viewUpdatedCount);
            Assert.AreEqual(0, viewCompletedCount);
        }

        [Test]
        public void Enable_OnCompleteEvent_ViewCompleted()
        {
            var (presenter, view, service, game) = Setup();
            presenter.Enable();

            service.OnCompleteEvent += Raise.Event<Action<Counter>>(game.CurrentLevel.goals[0]);
            service.OnCompleteEvent += Raise.Event<Action<Counter>>(game.CurrentLevel.restrictions[0]);

            Assert.AreEqual(0, viewUpdatedCount);
            Assert.AreEqual(2, viewCompletedCount);
        }

        [Test]
        public void Disable_OnUpdateEvent_ViewNotUpdated()
        {
            var (presenter, view, service, game) = Setup();
            presenter.Enable();
            presenter.Disable();

            service.OnUpdateEvent += Raise.Event<Action<Counter>>(game.CurrentLevel.goals[0]);
            service.OnUpdateEvent += Raise.Event<Action<Counter>>(game.CurrentLevel.restrictions[0]);

            Assert.AreEqual(0, viewUpdatedCount);
            Assert.AreEqual(0, viewCompletedCount);
        }

        [Test]
        public void Disable_OnCompleteEvent_ViewNotCompleted()
        {
            var (presenter, view, service, game) = Setup();
            presenter.Enable();
            presenter.Disable();

            service.OnCompleteEvent += Raise.Event<Action<Counter>>(game.CurrentLevel.goals[0]);
            service.OnCompleteEvent += Raise.Event<Action<Counter>>(game.CurrentLevel.restrictions[0]);

            Assert.AreEqual(0, viewUpdatedCount);
            Assert.AreEqual(0, viewCompletedCount);
        }
    }
}