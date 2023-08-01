using System;
using System.Collections;
using UnityEngine;
using View;

namespace View
{
    public class BoosterInventoryView : ABoosterInventoryView
    {
        [SerializeField] private Transform boostersParent;

        public override Transform BoostersParent => boostersParent;
    }
}