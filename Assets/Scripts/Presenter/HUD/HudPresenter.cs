using Model.Infrastructure;
using UnityEngine;

namespace Presenter
{
    /// <summary>
    /// Контроллер для HUD
    /// </summary>
    public class HudPresenter : MonoBehaviour, IHudPresenter
    {
        private Game game;

        public void Init(Game _game)
        {
            game = _game;
        }
    }
}