using UnityEngine;

namespace View
{
    public interface IBoostersView
    {
        IBoosterHintPopUp HintPopUp { get; }
        Transform BoosterButtonsParent { get; }

        void ClearButtonsParent();
    }
}