using System;
using UnityEngine;

namespace View
{
    public interface IPauseView
    {
        IPausePopUp PausePopUp { get; }

        event Action OnShowInput;
        event Action OnHideInput;
    }
}