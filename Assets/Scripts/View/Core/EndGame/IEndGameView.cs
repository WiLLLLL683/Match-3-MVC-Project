using UnityEngine;

namespace View
{
    public interface IEndGameView
    {
        public abstract IEndGamePopUp CompletePopUp { get; }
        public abstract IEndGamePopUp DefeatPopUp { get; }
    }
}