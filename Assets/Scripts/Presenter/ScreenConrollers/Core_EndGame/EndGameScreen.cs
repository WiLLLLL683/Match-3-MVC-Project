using Model.Infrastructure;
using Model.Readonly;
using UnityEngine;
using Utils;
using View;

namespace Presenter
{
    public class EndGameScreen : AEndGameScreen
    {
        [SerializeField] private AEndGamePopUp completePopUp;
        [SerializeField] private AEndGamePopUp defeatPopUp;

        private ILevel_Readonly model;
        private AInput input;
        private AFactory<ILevel_Readonly, AEndGamePopUp, IPopUpPresenter> factory;

        public override void Init(ILevel_Readonly model, AInput input, AFactory<ILevel_Readonly, AEndGamePopUp, IPopUpPresenter> factory)
        {
            this.model = model;
            this.input = input;
            this.factory = factory;
        }
        public override void Enable()
        {
            factory.Connect(completePopUp, model);
            factory.Connect(defeatPopUp, model);
            model.OnLose += ShowDefeatPopUp;
            model.OnWin += ShowCompletePopUp;

            Debug.Log($"{this} enabled", this);
        }
        public override void Disable()
        {
            model.OnLose -= ShowDefeatPopUp;
            model.OnWin -= ShowCompletePopUp;
            
            Debug.Log($"{this} disabled", this);
        }

        public override void ShowCompletePopUp()
        {
            input.Disable();

            defeatPopUp.Hide();
            completePopUp.Show();
        }
        public override void ShowDefeatPopUp()
        {
            input.Disable();

            completePopUp.Hide();
            defeatPopUp.Show();
        }
    }
}