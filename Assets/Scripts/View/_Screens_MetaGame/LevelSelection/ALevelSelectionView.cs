using System;
using UnityEngine;

namespace View
{
    public abstract class ALevelSelectionView : MonoBehaviour
    {
        public abstract ASelectorView Selector { get; }
    }
}