using System;
using UnityEngine;

namespace View
{
    public interface ILevelSelectionView
    {
        public event Action OnStartSelected;
        public event Action OnSelectNext;
        public event Action OnSelectPrevious;

        void ShowSelectedLevel(Sprite iconSprite, string name);
        void SetPreviousButtonActive(bool isActive);
        void SetNextButtonActive(bool isActive);
        void ShowLockedMark();
        void ShowCompleteMark();
        void HideAllMarks();
        void PlayLockedAnimation();
        void Input_StartSelected();
        void Input_SelectNext();
        void Input_SelectPrevious();
    }
}