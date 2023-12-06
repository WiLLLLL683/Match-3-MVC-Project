using Config;
using Infrastructure;
using Model.Objects;
using NSubstitute;
using NSubstitute.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using View;

namespace Presenter.UnitTests
{
    public class LevelSelectionPresenterTests
    {
        private const string LEVEL_0 = "Level0";
        private const string LEVEL_1 = "Level1";
        private const int NO_LEVEL_INDEX = -999;

        private string selectedLevelName;

        private (LevelSelectionPresenter presenter, ILevelSelectionView view, LevelProgress levelProgress) Setup()
        {
            selectedLevelName = "";

            var levelProgress = new LevelProgress();
            levelProgress.CurrentLevelIndex = NO_LEVEL_INDEX;
            //view
            var view = Substitute.For<ILevelSelectionView>();
            view.ShowSelectedLevel(Arg.Any<Sprite>(), Arg.Do<string>(x => selectedLevelName = x));
            //config
            var configProvider = Substitute.For<IConfigProvider>();
            configProvider.LastLevelIndex.Returns(1);
            var level0 = ScriptableObject.CreateInstance<LevelSO>();
            var level1 = ScriptableObject.CreateInstance<LevelSO>();
            level0.levelName = LEVEL_0;
            level1.levelName = LEVEL_1;
            configProvider.GetLevelSO(0).Returns(level0);
            configProvider.GetLevelSO(1).Returns(level1);
            //levelLoader
            var gameStateMachine = Substitute.For<GameStateMachine>();

            var presenter = new LevelSelectionPresenter(levelProgress, view, gameStateMachine, configProvider);

            return (presenter, view, levelProgress);
        }

        [Test]
        public void Enable_NoProgress_Level0Selected()
        {
            var (presenter, view, levelProgress) = Setup();

            presenter.Enable();

            Assert.AreEqual(LEVEL_0, selectedLevelName);
        }

        [Test]
        public void Enable_HasProgress_Level1Selected()
        {
            var (presenter, view, levelProgress) = Setup();
            levelProgress.LastCompletedLevel = 0;

            presenter.Enable();

            Assert.AreEqual(LEVEL_1, selectedLevelName);
        }

        [Test]
        public void Enable_AllProgress_LastLevelSelected()
        {
            var (presenter, view, levelProgress) = Setup();
            levelProgress.LastCompletedLevel = 1;

            presenter.Enable();

            Assert.AreEqual(LEVEL_1, selectedLevelName);
        }

        [Test]
        public void OnStartSelected_OpenedLevel_SelectedLevelStarted()
        {
            var (presenter, view, levelProgress) = Setup();
            presenter.Enable();

            view.OnStartSelected += Raise.Event<Action>();

            Assert.AreEqual(LEVEL_0, selectedLevelName);
            Assert.AreEqual(0, levelProgress.CurrentLevelIndex);
        }

        [Test]
        public void OnStartSelected_LockedLevel_NoLevelStarted()
        {
            var (presenter, view, levelProgress) = Setup();
            levelProgress.LastOpenedLevel = -1;
            presenter.Enable();

            view.OnStartSelected += Raise.Event<Action>();

            Assert.AreEqual(LEVEL_0, selectedLevelName);
            Assert.AreEqual(NO_LEVEL_INDEX, levelProgress.CurrentLevelIndex);
        }

        [Test]
        public void OnSelectNext_NextExists_NextLevelSelected()
        {
            var (presenter, view, levelProgress) = Setup();
            presenter.Enable();
            Assert.AreEqual(LEVEL_0, selectedLevelName);

            view.OnSelectNext += Raise.Event<Action>();

            Assert.AreEqual(LEVEL_1, selectedLevelName);
        }

        [Test]
        public void OnSelectNext_LastLevel_CurrentLevelSelected()
        {
            var (presenter, view, levelProgress) = Setup();
            levelProgress.LastCompletedLevel = 1;
            presenter.Enable();
            Assert.AreEqual(LEVEL_1, selectedLevelName);

            view.OnSelectNext += Raise.Event<Action>();

            Assert.AreEqual(LEVEL_1, selectedLevelName);
        }

        [Test]
        public void OnSelectPrevious_PreviousExists_PreviousLevelSelected()
        {
            var (presenter, view, levelProgress) = Setup();
            levelProgress.LastCompletedLevel = 1;
            presenter.Enable();
            Assert.AreEqual(LEVEL_1, selectedLevelName);

            view.OnSelectPrevious += Raise.Event<Action>();

            Assert.AreEqual(LEVEL_0, selectedLevelName);
        }

        [Test]
        public void OnSelectPrevious_FirstLevel_CurrentLevelSelected()
        {
            var (presenter, view, levelProgress) = Setup();
            presenter.Enable();
            Assert.AreEqual(LEVEL_0, selectedLevelName);

            view.OnSelectPrevious += Raise.Event<Action>();

            Assert.AreEqual(LEVEL_0, selectedLevelName);
        }
    }
}