using Model.Infrastructure;
using UnityEngine;
using View;

namespace Presenter
{
    public abstract class APauseScreen : AScreenController
    {
        public abstract void Init(PlayerSettings settings, IInput input, Bootstrap bootstrap);
    }
}