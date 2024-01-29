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

        public void ClearButtonsParent()
        {
            for (int i = 0; i < BoosterButtonsParent.childCount; i++)
            {
                GameObject.Destroy(BoosterButtonsParent.GetChild(i).gameObject);
            }
        }
    }
}