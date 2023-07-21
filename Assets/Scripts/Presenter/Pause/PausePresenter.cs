using Model.Infrastructure;
using UnityEngine;
using View;

namespace Presenter
{
    public class PausePresenter : MonoBehaviour, IPausePresenter
    {
        [SerializeField] private GameObject pauseMenu;

        private Game game;
        private IInput input;
        private Bootstrap bootstrap;

        public void Init(Game game, IInput input, Bootstrap bootstrap)
        {
            this.game = game;
            this.input = input;
            this.bootstrap = bootstrap;
        }

        //функционал кнопок
        public void ShowPauseMenu()
        {
            pauseMenu.SetActive(true);
            input.Disable();
        }
        public void HidePauseMenu()
        {
            pauseMenu.SetActive(false);
            input.Enable();
        }
        public void Quit() => bootstrap.LoadMetaGame();
        public void Replay()
        {
            //TODO replay
            Debug.Log("Replay");
        }
    }
}