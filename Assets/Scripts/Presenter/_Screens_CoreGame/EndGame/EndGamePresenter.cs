using Model.Infrastructure;
using Model.Readonly;
using UnityEngine;
using Utils;
using View;

namespace Presenter
{
    public class EndGamePresenter : IEndGamePresenter
    {
        public class Factory : AFactory<ILevel_Readonly, AEndGameView, IEndGamePresenter>
        {
            private readonly AFactory<ILevel_Readonly, AEndGamePopUp, IPopUpPresenter> factory;
            private readonly AInput input;

            public Factory(AEndGameView viewPrefab, AInput input, AFactory<ILevel_Readonly, AEndGamePopUp, IPopUpPresenter> factory) : base(viewPrefab)
            {
                this.input = input;
                this.factory = factory;
            }

            public override IEndGamePresenter Connect(AEndGameView existingView, ILevel_Readonly model)
            {
                var presenter = new EndGamePresenter(model, existingView, input, factory);
                presenter.Enable();
                allPresenters.Add(presenter);
                return presenter;
            }
        }

        private readonly ILevel_Readonly model;
        private readonly AEndGameView view;
        private readonly AInput input;
        private readonly AFactory<ILevel_Readonly, AEndGamePopUp, IPopUpPresenter> factory;

        public EndGamePresenter(ILevel_Readonly model,
            AEndGameView view,
            AInput input,
            AFactory<ILevel_Readonly, AEndGamePopUp, IPopUpPresenter> factory)
        {
            this.model = model;
            this.view = view;
            this.input = input;
            this.factory = factory;
        }
        public void Enable()
        {
            factory.Connect(view.CompletePopUp, model);
            factory.Connect(view.DefeatPopUp, model);
            model.OnLose += ShowDefeatPopUp;
            model.OnWin += ShowCompletePopUp;

            Debug.Log($"{this} enabled");
        }
        public void Disable()
        {
            model.OnLose -= ShowDefeatPopUp;
            model.OnWin -= ShowCompletePopUp;
            
            Debug.Log($"{this} disabled");
        }
        public void Destroy()
        {
            Disable();
            GameObject.Destroy(view.gameObject);
        }

        public void ShowCompletePopUp()
        {
            input.Disable();

            view.DefeatPopUp.Hide();
            view.CompletePopUp.Show();
        }
        public void ShowDefeatPopUp()
        {
            input.Disable();

            view.CompletePopUp.Hide();
            view.DefeatPopUp.Show();
        }
    }
}