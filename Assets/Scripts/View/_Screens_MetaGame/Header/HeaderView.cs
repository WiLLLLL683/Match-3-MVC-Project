using System;
using System.Collections;
using UnityEngine;

namespace View
{
    public class HeaderView : AHeaderView
    {
        [SerializeField] private Transform scoreParent;

        public override Transform ScoreParent => scoreParent;
    }
}