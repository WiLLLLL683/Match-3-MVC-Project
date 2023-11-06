using System;
using UnityEngine;

namespace View
{
    public interface ILevelSelectionView
    {
        public event Action OnStartSelected;
        public event Action OnSelectNext;
        public event Action OnSelectPrevious;

        public void ShowSelectedLevel(Sprite iconSprite, string name);
        void SetPreviousButtonActive(bool isActive);
        void SetNextButtonActive(bool isActive);
        void ShowLockedMark();
        void ShowCompleteMark();
        void ShowNewMark();
        void PlayLockedAnimation();
    }
}