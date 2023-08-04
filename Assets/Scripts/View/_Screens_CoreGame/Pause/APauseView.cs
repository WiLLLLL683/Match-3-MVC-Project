using UnityEngine;

namespace View
{
    public abstract class APauseView : MonoBehaviour
    {
        public abstract APausePopUp PausePopUp { get; }
    }
}