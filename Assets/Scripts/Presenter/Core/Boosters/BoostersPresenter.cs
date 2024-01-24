﻿using Config;
using Cysharp.Threading.Tasks;
using Infrastructure;
using Model.Objects;
using Model.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using View;
using View.Factories;
using View.Input;

namespace Presenter
{
    /// <summary>
    /// Перезентер инвентаря бустеров в кор-игре
    /// </summary>
    public class BoostersPresenter : IBoostersPresenter
    {
        private readonly Game model;
        private readonly IBoosterService service;
        private readonly IBoostersView view;
        private readonly IBoosterButtonFactory buttonFactory;
        private readonly IConfigProvider configProvider;
        private readonly IStateMachine stateMachine;
        private readonly IGameBoardInput input;
        private readonly ISelectInputMode selectInputMode;
        private readonly Dictionary<int, IBoosterButtonView> idButtons = new();

        private int selectedBoosterId;

        public BoostersPresenter(Game model,
            IBoosterService service,
            IBoostersView view,
            IBoosterButtonFactory buttonFactory,
            IConfigProvider configProvider,
            IStateMachine stateMachine,
            IGameBoardInput input)
        {
            this.model = model;
            this.service = service;
            this.view = view;
            this.buttonFactory = buttonFactory;
            this.configProvider = configProvider;
            this.stateMachine = stateMachine;
            this.input = input;
            this.selectInputMode = input.GetInputMode<ISelectInputMode>();
        }
        public void Enable()
        {
            SpawnButtons();

            selectInputMode.OnInputSelect += ActivateBooster;
            view.HintPopUp.OnInputActivate += ActivateBooster;
            view.HintPopUp.OnInputHide += HideHintPopUp;
            service.OnAmountChanged += ChangeAmountOnButton;

            Debug.Log($"{this} enabled");
        }

        public void Disable()
        {
            DisableButtons();

            selectInputMode.OnInputSelect -= ActivateBooster;
            view.HintPopUp.OnInputActivate -= ActivateBooster;
            view.HintPopUp.OnInputHide -= HideHintPopUp;
            service.OnAmountChanged -= ChangeAmountOnButton;

            Debug.Log($"{this} disabled");
        }

        private void SpawnButtons()
        {
            idButtons.Clear();

            for (int i = 0; i < view.BoosterButtonsParent.childCount; i++)
            {
                GameObject.Destroy(view.BoosterButtonsParent.GetChild(i).gameObject);
            }

            foreach (var booster in model.BoosterInventory.boosters)
            {
                IBoosterButtonView button = buttonFactory.Create(booster.Key, booster.Value);
                idButtons.Add(booster.Key, button);
                button.OnActivate += ShowHintPopUp;
            }
        }

        private void DisableButtons()
        {
            foreach (var button in idButtons)
            {
                button.Value.OnActivate -= ShowHintPopUp;
            }
        }

        private void ShowHintPopUp(IBoosterButtonView button)
        {
            input.SetCurrentMode<ISelectInputMode>();
            selectedBoosterId = button.Id;
            BoosterSO config = configProvider.GetBoosterSO(selectedBoosterId);
            view.HintPopUp.Show(config.Icon, config.Name, config.Hint);

            switch (config.InputType)
            {
                case BoosterInputType.GameBoard:
                    view.HintPopUp.ShowOverlayWithGameBoard();
                    break;
                case BoosterInputType.Button:
                    view.HintPopUp.ShowOverlayWithButton();
                    break;
                default:
                    view.HintPopUp.ShowOverlayWithButton();
                    break;
            }
        }

        private void HideHintPopUp() => HideHintPopUpAsync().Forget();
        private async UniTaskVoid HideHintPopUpAsync()
        {
            await UniTask.DelayFrame(configProvider.Delays.beforeBoosterHintHide);
            input.SetCurrentMode<IMoveInputMode>();
            view.HintPopUp.Hide();
        }

        private void ChangeAmountOnButton(int id, int amount)
        {
            idButtons[id].ChangeAmount(amount);
            idButtons[id].EnableButton(amount > 0);
        }

        private void ActivateBooster(Vector2Int startPosition)
        {
            stateMachine.EnterState<InputBoosterState, (int, Vector2Int)>((selectedBoosterId, startPosition));
            HideHintPopUp();
        }
    }
}
