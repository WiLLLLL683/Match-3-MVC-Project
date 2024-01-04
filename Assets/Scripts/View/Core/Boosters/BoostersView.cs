using System;
using UnityEngine;

namespace View
{
    public class BoostersView : MonoBehaviour, IBoostersView
    {
        [SerializeField] private Transform boosterButtonsParent;
        [SerializeField] private BoosterHintPopUp hintPopUp;

        public Transform BoosterButtonsParent => boosterButtonsParent;
        public IBoosterHintPopUp HintPopUp => hintPopUp;
    }
}