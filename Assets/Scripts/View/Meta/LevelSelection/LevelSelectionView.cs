using System;
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
        [Header("Buttons")]
        [SerializeField] private Button previousButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button startButton;

        public event Action OnStartSelected;
        public event Action OnSelectNext;
        public event Action OnSelectPrevious;

        private void Awake()
        {
            previousButton.onClick.AddListener(Input_SelectPrevious);
            nextButton.onClick.AddListener(Input_SelectNext);
            startButton.onClick.AddListener(Input_StartSelected);
        }

        private void OnDestroy()
        {
            previousButton.onClick.RemoveListener(Input_SelectPrevious);
            nextButton.onClick.RemoveListener(Input_SelectNext);
            startButton.onClick.RemoveListener(Input_StartSelected);
        }

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

        public void PlayLockedAnimation() => lockedMarkAnimation.Play();

        private void Input_StartSelected() => OnStartSelected?.Invoke();
        private void Input_SelectNext() => OnSelectNext?.Invoke();
        private void Input_SelectPrevious() => OnSelectPrevious?.Invoke();
    }
}