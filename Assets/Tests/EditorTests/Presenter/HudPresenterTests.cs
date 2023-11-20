using System;
using Config;
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
        private (HudPresenter presenter, IHudView view) Setup()
        {
            var game = TestLevelFactory.CreateGame(1, 1);
            var view = Substitute.For<IHudView>();
            var counterService = Substitute.For<ICounterService>();
            var counterConfig = Substitute.For<ICounterTargetConfigProvider>();
            var counterViewFactory = Substitute.For<ICounterViewFactory>();
            var presenter = new HudPresenter(game, view, counterService, counterViewFactory);

            return (presenter, view);
        }

        [Test]
        public void Enable_ParentTransformsCleared()
        {
            var (presenter, view) = Setup();
            var existingGoal = new GameObject();
            existingGoal.transform.parent = view.GoalsParent;
            var existingRestriction = new GameObject();
            existingRestriction.transform.parent = view.RestrictionsParent;

            presenter.Enable();

            Assert.AreEqual(0, view.GoalsParent.childCount);
            Assert.AreEqual(0, view.RestrictionsParent.childCount);
        }

        [Test]
        public void Enable_GoalsRestrictionsSpawned()
        {
            var (presenter, view) = Setup();

            presenter.Enable();

            Assert.AreEqual(0, view.GoalsParent.childCount);
            Assert.AreEqual(0, view.RestrictionsParent.childCount);
        }
    }
}