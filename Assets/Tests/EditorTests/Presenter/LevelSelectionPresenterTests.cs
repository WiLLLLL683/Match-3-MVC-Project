using Config;
using Infrastructure;
using Model.Objects;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using View;

namespace Presenter.UnitTests
{
    public class LevelSelectionPresenterTests
    {
        private int selectedLevelIndex;

        private (LevelSelectionPresenter presenter, LevelProgress levelProgress) Setup()
        {
            var levelProgress = new LevelProgress();
            var view = Substitute.For<ILevelSelectionView>();
            var levelLoader = Substitute.For<ILevelLoader>();
            var config = Substitute.For<ILevelConfigProvider>();
            var presenter = new LevelSelectionPresenter(levelProgress, view, levelLoader, config);

            return (presenter, levelProgress);
        }

        [Test]
        public void Enable_FirstNewLevelSelected()
        {
            var (presenter, levelProgress) = Setup();

            presenter.Enable();

            Assert.AreEqual(levelProgress.CompletedLevels + 1, selectedLevelIndex);
        }
    }
}