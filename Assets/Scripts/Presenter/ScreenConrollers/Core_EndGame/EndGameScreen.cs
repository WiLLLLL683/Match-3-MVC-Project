using Model.Infrastructure;
using UnityEngine;
using View;

namespace Presenter
{
    public class EndGameScreen : AEndGameScreen
    {
        [SerializeField] private AEndGamePopUp completePopUp;
        [SerializeField] private AEndGamePopUp defeatPopUp;

        private Game game;
        private IInput input;

        public override void Init(Game game, IInput input)
        {
            this.game = game;
            this.input = input;
        }
        public override void Enable()
        {
            //TODO
            Debug.Log($"{this} enabled", this);
        }
        public override void Disable()
        {
            //TODO
            Debug.Log($"{this} disabled", this);
        }

        public override void OnLevelComplete()
        {
            input.Disable();

            defeatPopUp.Hide();
            completePopUp.UpdateScore(4221, 3); //TODO брать счет из модели
            completePopUp.Show();
        }
        public override void OnDefeat()
        {
            input.Disable();

            completePopUp.Hide();
            defeatPopUp.UpdateScore(42); //TODO брать счет из модели
            defeatPopUp.Show();
        }
    }
}