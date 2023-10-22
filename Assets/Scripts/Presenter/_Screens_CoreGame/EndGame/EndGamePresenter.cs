using UnityEngine;
using Model.Infrastructure;
using Model.Readonly;
using Utils;
using View;
using Model.Objects;
using Model.Services;

namespace Presenter
{
    public class EndGamePresenter : IEndGamePresenter
    {
        public class Factory : AFactory<Level, AEndGameView, IEndGamePresenter>
        {
            private readonly AInput input;
            private readonly AFactory<Level, AEndGamePopUp, IPopUpPresenter> factory;
            private readonly IWinLoseService winLoseService;

            public Factory(AEndGameView viewPrefab, AInput input, AFactory<Level, AEndGamePopUp, IPopUpPresenter> factory,
            IWinLoseService winLoseService) : base(viewPrefab)
            {
                this.input = input;
                this.factory = factory;
                this.winLoseService = winLoseService;
            }

            public override IEndGamePresenter Connect(AEndGameView existingView, Level model)
            {
                var presenter = new EndGamePresenter(model, existingView, input, factory, winLoseService);
                presenter.Enable();
                allPresenters.Add(presenter);
                return presenter;
            }
        }

        private readonly Level model;
        private readonly AEndGameView view;
        private readonly AInput input;
        private readonly AFactory<Level, AEndGamePopUp, IPopUpPresenter> factory;
        private readonly IWinLoseService winLoseService;

        public EndGamePresenter(Level model,
            AEndGameView view,
            AInput input,
            AFactory<Level, AEndGamePopUp, IPopUpPresenter> factory,
            IWinLoseService winLoseService)
        {
            this.model = model;
            this.view = view;
            this.input = input;
            this.factory = factory;
            this.winLoseService = winLoseService;
        }
        public void Enable()
        {
            factory.Connect(view.CompletePopUp, model);
            factory.Connect(view.DefeatPopUp, model);
            winLoseService.OnLose += ShowDefeatPopUp;
            winLoseService.OnWin += ShowCompletePopUp;

            Debug.Log($"{this} enabled");
        }
        public void Disable()
        {
            winLoseService.OnLose -= ShowDefeatPopUp;
            winLoseService.OnWin -= ShowCompletePopUp;
            
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