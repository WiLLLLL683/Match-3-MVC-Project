using System;
using System.Collections;
using UnityEngine;

namespace View
{
    public class LevelSelectionView : ALevelSelectionView
    {
        [SerializeField] private ASelectorView selector;

        public override ASelectorView Selector => selector;
    }
}