using Model.Infrastructure;
using UnityEngine;
using View;

namespace Presenter
{
    public class EndGamePresenter : MonoBehaviour, IEndGamePresenter
    {
        [SerializeField] private EndGameMenu completeMenu;
        [SerializeField] private EndGameMenu defeatMenu;

        private Game game;
        private IInput input;
        private Bootstrap bootstrap;

        public void Init(Game game, IInput input, Bootstrap bootstrap)
        {
            this.game = game;
            this.input = input;
            this.bootstrap = bootstrap;
        }
        public void Enable()
        {

        }
        public void Disable()
        {

        }

        public void ShowCompleteMenu()
        {
            defeatMenu.gameObject.SetActive(false);
            input.Disable();

            completeMenu.UpdateScore(4221, 3); //TODO брать счет из модели
            completeMenu.gameObject.SetActive(true);
        }
        public void ShowDefeatMenu()
        {
            completeMenu.gameObject.SetActive(false);
            input.Disable();

            defeatMenu.UpdateScore(42); //TODO брать счет из модели
            defeatMenu.gameObject.SetActive(true);
        }
        public void HideAllMenus()
        {
            defeatMenu.gameObject.SetActive(false);
            completeMenu.gameObject.SetActive(false);

            input.Enable();
        }

        //функционал кнопок
        public void Replay()
        {
            //TODO replay
            Debug.Log("Replay");
        }
        public void NextLevel()
        {
            //TODO nextLevel
            Debug.Log("Next Level");
        }
        public void Quit() => bootstrap.LoadMetaGame();
    }
}