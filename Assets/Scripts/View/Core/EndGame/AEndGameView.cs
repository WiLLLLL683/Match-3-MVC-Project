using UnityEngine;

namespace View
{
    public abstract class AEndGameView : MonoBehaviour
    {
        public abstract AEndGamePopUp CompletePopUp { get; }
        public abstract AEndGamePopUp DefeatPopUp { get; }
    }
}