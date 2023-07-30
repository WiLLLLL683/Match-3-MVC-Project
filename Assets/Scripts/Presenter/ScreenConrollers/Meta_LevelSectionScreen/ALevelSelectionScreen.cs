using Model.Infrastructure;
using Model.Readonly;
using UnityEngine;
using View;

namespace Presenter
{
    public abstract class ALevelSelectionScreen : AScreenController
    {
        public abstract void Init(ILevelSelection_Readonly model,
                         AFactory<ILevelSelection_Readonly, ASelectorView, ISelectorPresenter> selectorFactory);
    }
}