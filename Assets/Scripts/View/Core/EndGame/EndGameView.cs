using UnityEngine;

namespace View
{
    public class EndGameView : MonoBehaviour, IEndGameView
    {
        [SerializeField] private EndGamePopUp completePopUp;
        [SerializeField] private EndGamePopUp defeatPopUp;

        public IEndGamePopUp CompletePopUp => completePopUp;
        public IEndGamePopUp DefeatPopUp => defeatPopUp;
    }
}