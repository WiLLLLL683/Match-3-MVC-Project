using System;
using System.Collections;
using UnityEngine;

namespace View
{
    public class PauseView : APauseView
    {
        [SerializeField] private APausePopUp pausePopUp;

        public override APausePopUp PausePopUp => pausePopUp;
    }
}