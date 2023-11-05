using UnityEngine;

namespace View
{
    public class PauseView : MonoBehaviour, IPauseView
    {
        [SerializeField] private PausePopUp pausePopUp;

        public IPausePopUp PausePopUp => pausePopUp;
    }
}