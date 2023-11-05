using System;
using UnityEngine;

namespace View
{
    public interface ILevelSelectionView
    {
        public event Action OnStartSelected;
        public event Action OnSelectNext;
        public event Action OnSelectPrevious;

        public void UpdateSelectedLevel(Sprite iconSprite, string name);
    }
}