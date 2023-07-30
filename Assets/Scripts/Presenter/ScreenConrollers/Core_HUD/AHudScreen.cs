using Model.Infrastructure;
using Model.Objects;
using Model.Readonly;
using UnityEngine;
using View;

namespace Presenter
{
    public abstract class AHudScreen : AScreenController
    {
        public abstract void Init(ILevel_Readonly model,
            AFactory<ICounter_Readonly, ICounterView, ICounterPresenter> goalFactory,
            AFactory<ICounter_Readonly, ICounterView, ICounterPresenter> restrictionFactory);
    }
}