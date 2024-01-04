﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class LevelSelectionView : MonoBehaviour, ILevelSelectionView
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private Image completeMark;
        [SerializeField] private Image lockedMark;
        [SerializeField] private Animation lockedMarkAnimation;
        [SerializeField] private Button previousButton;
        [SerializeField] private Button nextButton;

        public event Action OnStartSelected;
        public event Action OnSelectNext;
        public event Action OnSelectPrevious;

        public void SetPreviousButtonActive(bool isActive) => previousButton.gameObject.SetActive(isActive);
        public void SetNextButtonActive(bool isActive) => nextButton.gameObject.SetActive(isActive);

        public void ShowSelectedLevel(Sprite iconSprite, string name)
        {
            icon.sprite = iconSprite;
            nameText.text = name;
        }

        public void ShowLockedMark()
        {
            HideAllMarks();
            lockedMark.gameObject.SetActive(true);
        }

        public void ShowCompleteMark()
        {
            HideAllMarks();
            completeMark.gameObject.SetActive(true);
        }

        public void HideAllMarks()
        {
            completeMark.gameObject.SetActive(false);
            lockedMark.gameObject.SetActive(false);
        }

        public void PlayLockedAnimation()
        {
            lockedMarkAnimation.Play();
        }

        public void Input_StartSelected() => OnStartSelected?.Invoke();
        public void Input_SelectNext() => OnSelectNext?.Invoke();
        public void Input_SelectPrevious() => OnSelectPrevious?.Invoke();
    }
}