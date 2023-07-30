using Presenter;
using System;
using System.Collections;
using UnityEngine;

namespace Presenter
{
    public class BackgroundScreen : ABackgroundScreen
    {
        [SerializeField] private GameObject background;

        public override void Enable() => background.SetActive(true);
        public override void Disable() => background.SetActive(false);
    }
}
