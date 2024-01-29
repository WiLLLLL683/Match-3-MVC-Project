using UnityEngine;

namespace View
{
    public interface IEndGameView
    {
        IEndGamePopUp CompletePopUp { get; }
        IEndGamePopUp DefeatPopUp { get; }
    }
}