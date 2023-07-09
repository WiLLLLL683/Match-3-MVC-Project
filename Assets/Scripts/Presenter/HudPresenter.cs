using Model.Infrastructure;
using System;
using System.Collections;
using UnityEngine;

namespace Presenter
{
    /// <summary>
    /// Контроллер для HUD
    /// </summary>
    public class HudPresenter : MonoBehaviour
    {
        private Game game;

        public void Init(Game _game)
        {
            game = _game;
        }
    }
}