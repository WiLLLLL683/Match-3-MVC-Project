using UnityEngine;

namespace View
{
    public interface IPauseView
    {
        public abstract IPausePopUp PausePopUp { get; }
    }
}