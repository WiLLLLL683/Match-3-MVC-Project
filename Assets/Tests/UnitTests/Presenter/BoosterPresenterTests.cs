using Config;
using Model.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using View;
using View.Factories;
using TestUtils;
using NSubstitute;
using Model.Objects;
using Infrastructure;
using View.Input;

namespace Presenter.UnitTests
{
    public class BoosterPresenterTests
    {
        private const int BOOSTER_ID = 1;
        private const int BOOSTER_INITIAL_AMOUNT = 1;

        private int buttonSpawnedCount;
        private int hintPopUpShownCount;
        private int hintPopUpHiddenCount;
        private int boosterActivatedCount;
        private int viewChangedAmountCount;

        private (BoostersPresenter presenter,
            Game model,
            IBoosterService service,
            IBoostersView view,
            IBoosterHintPopUp hintPopUp,
            IBoosterButtonFactory buttonFactory,
            IBoosterButtonView button,
            IConfigProvider configProvider,
            IStateMachine stateMachine,
            IGameBoardInput input)
            Setup()
        {
            buttonSpawnedCount = 0;
            hintPopUpShownCount = 0;
            hintPopUpHiddenCount = 0;
            boosterActivatedCount = 0;
            viewChangedAmountCount = 0;

            //var booster = Substitute.For<IBooster>();
            //model
            var model = TestLevelFactory.CreateGame(1,1);
            model.BoosterInventory.boosters.Add(BOOSTER_ID, BOOSTER_INITIAL_AMOUNT);

            var service = Substitute.For<IBoosterService>();

            //view
            var hintPopUp = Substitute.For<IBoosterHintPopUp>();
            hintPopUp.WhenForAnyArgs(x => x.Show(default, default, default)).Do(x => hintPopUpShownCount++);
            hintPopUp.WhenForAnyArgs(x => x.Hide()).Do(x => hintPopUpHiddenCount++);
            var view = Substitute.For<IBoostersView>();
            view.HintPopUp.Returns(hintPopUp);

            //factory
            var button = Substitute.For<IBoosterButtonView>();
            button.WhenForAnyArgs(x => x.ChangeAmount(default)).Do(x => viewChangedAmountCount++);
            var buttonFactory = Substitute.For<IBoosterButtonFactory>();
            buttonFactory.Create(default, default).ReturnsForAnyArgs(button).AndDoes(x => buttonSpawnedCount++);

            //config
            var config = ScriptableObject.CreateInstance(typeof(BoosterSO)); //Substitute.For<BoosterSO>();
            var configProvider = Substitute.For<IConfigProvider>();
            configProvider.GetBoosterSO(default).ReturnsForAnyArgs(config);

            //stateMachine
            var stateMachine = Substitute.For<IStateMachine>();
            stateMachine.WhenForAnyArgs(x => x.EnterState<InputBoosterState, (int, Vector2Int)>(default)).Do(x => boosterActivatedCount++);

            //input
            var inputMode = Substitute.For<ISelectInputMode>();
            var input = Substitute.For<IGameBoardInput>();
            input.GetInputMode<ISelectInputMode>().ReturnsForAnyArgs(inputMode);

            var presenter = new BoostersPresenter(model, service, view, buttonFactory, configProvider, stateMachine, input);

            return (presenter, model, service, view, hintPopUp, buttonFactory, button, configProvider, stateMachine, input);
        }

        [Test]
        public void Enable_ButtonsSpawned()
        {
            var setup = Setup();

            setup.presenter.Enable();

            Assert.AreEqual(setup.model.BoosterInventory.boosters.Count, buttonSpawnedCount);
        }

        [Test]
        public void Enable_ButtonActivateEvent_HintPopUpShown()
        {
            var setup = Setup();

            setup.presenter.Enable();
            setup.button.OnActivate += Raise.Event<Action<IBoosterButtonView>>(setup.button);

            Assert.AreEqual(1, hintPopUpShownCount);
        }

        [Test]
        public void Enable_SelectEvent_BoosterActivated()
        {
            var setup = Setup();

            setup.presenter.Enable();
            setup.input.GetInputMode<ISelectInputMode>().OnInputSelect += Raise.Event<Action<Vector2Int>>(new Vector2Int());

            Assert.AreEqual(1, boosterActivatedCount);
        }

        [Test]
        public void Enable_ActivateEvent_BoosterActivated()
        {
            var setup = Setup();

            setup.presenter.Enable();
            setup.hintPopUp.OnInputActivate += Raise.Event<Action<Vector2Int>>(new Vector2Int());

            Assert.AreEqual(1, boosterActivatedCount);
        }

        [Test]
        public void Enable_HideEvent_HintPopUpHidden()
        {
            var setup = Setup();

            setup.presenter.Enable();
            setup.hintPopUp.OnInputHide += Raise.Event<Action>();

            Assert.AreEqual(1, hintPopUpHiddenCount);
        }

        [Test]
        public void Enable_AmountChangeEvent_ViewChangedAmount()
        {
            var setup = Setup();

            setup.presenter.Enable();
            setup.service.OnAmountChanged += Raise.Event<Action<int,int>>(BOOSTER_ID, 0);

            Assert.AreEqual(1, viewChangedAmountCount);
        }

        [Test]
        public void Disable_ButtonsDisabled()
        {
            var setup = Setup();

            setup.presenter.Enable();
            setup.presenter.Disable();
            setup.button.OnActivate += Raise.Event<Action<IBoosterButtonView>>(setup.button);

            Assert.AreEqual(0, hintPopUpShownCount);
        }

        [Test]
        public void Disable_SelectEvent_NoChange()
        {
            var setup = Setup();

            setup.presenter.Enable();
            setup.presenter.Disable();
            setup.input.GetInputMode<ISelectInputMode>().OnInputSelect += Raise.Event<Action<Vector2Int>>(new Vector2Int());

            Assert.AreEqual(0, boosterActivatedCount);
        }

        [Test]
        public void Disable_ActivateEvent_NoChange()
        {
            var setup = Setup();

            setup.presenter.Enable();
            setup.presenter.Disable();
            setup.hintPopUp.OnInputActivate += Raise.Event<Action<Vector2Int>>(new Vector2Int());

            Assert.AreEqual(0, boosterActivatedCount);
        }

        [Test]
        public void Disable_HideEvent_NoChange()
        {
            var setup = Setup();

            setup.presenter.Enable();
            setup.presenter.Disable();
            setup.hintPopUp.OnInputHide += Raise.Event<Action>();

            Assert.AreEqual(0, hintPopUpHiddenCount);
        }

        [Test]
        public void Disable_AmountChangeEvent_NoChange()
        {
            var setup = Setup();

            setup.presenter.Enable();
            setup.presenter.Disable();
            setup.service.OnAmountChanged += Raise.Event<Action<int, int>>(BOOSTER_ID, 0);

            Assert.AreEqual(0, viewChangedAmountCount);
        }
    }
}