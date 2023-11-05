using Model.Services;
using Presenter;
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
        private IBoosterInventoryPresenter boosterInventory;
        private IPausePresenter pause;
        private IEndGamePresenter endGame;

        [Inject]
        public void Construct(AInput input, IHudPresenter hud, IGameBoardPresenter gameBoard, IBoosterInventoryPresenter boosterInventory, IPausePresenter pause, IEndGamePresenter endGame)
        {
            this.input = input;
            this.hud = hud;
            this.gameBoard = gameBoard;
            this.boosterInventory = boosterInventory;
            this.pause = pause;
            this.endGame = endGame;
        }

        private void Start()
        {
            input.Enable();
            hud.Enable();
            gameBoard.Enable();
            boosterInventory.Enable();
            pause.Enable();
            endGame.Enable();
        }

        private void OnDestroy()
        {
            //input?.Disable(); //MonoBehavior будет уничтожен при выгрузке сцены и не требует отключения
            hud?.Disable();
            gameBoard?.Disable();
            boosterInventory?.Disable();
            pause?.Disable();
            endGame.Disable();
        }
    }
}