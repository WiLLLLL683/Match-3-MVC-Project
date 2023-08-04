using UnityEngine;

namespace View
{
    public class EndGameView : AEndGameView
    {
        [SerializeField] private AEndGamePopUp completePopUp;
        [SerializeField] private AEndGamePopUp defeatPopUp;

        public override AEndGamePopUp CompletePopUp => completePopUp;
        public override AEndGamePopUp DefeatPopUp => defeatPopUp;
    }
}