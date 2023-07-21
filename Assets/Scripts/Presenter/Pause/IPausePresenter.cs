using Model.Infrastructure;
using UnityEngine;
using View;

namespace Presenter
{
    public interface IPausePresenter
    {
        public GameObject gameObject { get; }
        
        void HidePauseMenu();
        void Init(Game game, IInput input, Bootstrap bootstrap);
        void Quit();
        void Replay();
        void ShowPauseMenu();
    }
}