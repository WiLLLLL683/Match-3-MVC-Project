using Model.Infrastructure;
using UnityEngine;
using View;

namespace Presenter
{
    public class EndGamePresenter : IEndGamePresenter
    {
        private AEndGameView view;
        private Game game;
        private IInput input;
        private Bootstrap bootstrap;

        public EndGamePresenter(Game game, AEndGameView view, IInput input, Bootstrap bootstrap)
        {
            this.game = game;
            this.view = view;
            this.input = input;
            this.bootstrap = bootstrap;
        }
        public void Enable()
        {

        }
        public void Disable()
        {

        }
        public void Destroy()
        {
            Disable();
            GameObject.Destroy(view.gameObject);
        }

        public void OnLevelComplete()
        {
            input.Disable();
            view.ShowCompleteMenu(4221, 3); //TODO брать счет из модели
        }
        public void OnDefeat()
        {
            input.Disable();
            view.ShowDefeatMenu(42); //TODO брать счет из модели
        }
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