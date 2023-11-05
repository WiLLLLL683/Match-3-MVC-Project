using Model.Infrastructure;
using Presenter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using View;
using Zenject;

namespace Infrastructure
{
    /// <summary>
    /// Точка входа для сцены кор-игры
    /// Запускает все презентеры на сцене
    /// </summary>
    public class CoreBootstrap : MonoBehaviour
    {
        private AInput input;
        private IHudPresenter hud;
        private IGameBoardPresenter gameBoard;

        [Inject]
        public void Construct(AInput input, IHudPresenter hud, IGameBoardPresenter gameBoard)
        {
            this.input = input;
            this.hud = hud;
            this.gameBoard = gameBoard;
        }

        private void Start()
        {
            input.Enable();
            hud.Enable();
            gameBoard.Enable();
        }

        private void OnDestroy()
        {
            input?.Disable();
            hud?.Disable();
            gameBoard?.Disable();
        }
    }
}