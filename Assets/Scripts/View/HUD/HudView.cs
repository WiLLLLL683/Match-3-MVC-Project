using System;
using System.Collections;
using UnityEngine;

namespace View
{
    public class HudView : AHudView
    {
        [SerializeField] private Transform goalsParent;
        [SerializeField] private Transform restrictionsParent;

        public override Transform GoalsParent => goalsParent;
        public override Transform RestrictionsParent => restrictionsParent;
    }
}