using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class LevelSelectionView : MonoBehaviour, ILevelSelectionView, ILevelSelectionInput
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private Button previousButton;
        [SerializeField] private Button nextButton;

        public event Action OnStartSelected;
        public event Action OnSelectNext;
        public event Action OnSelectPrevious;

        public void UpdateSelectedLevel(Sprite iconSprite, string name)
        {
            SetIcon(iconSprite);
            SetName(name);
        }

        public void SetPreviousButtonActive(bool isActive) => previousButton.gameObject.SetActive(isActive);
        public void SetNextButtonActive(bool isActive) => nextButton.gameObject.SetActive(isActive);

        public void Input_StartSelected() => OnStartSelected?.Invoke();
        public void Input_SelectNext() => OnSelectNext?.Invoke();
        public void Input_SelectPrevious() => OnSelectPrevious?.Invoke();

        private void SetIcon(Sprite iconSprite) => icon.sprite = iconSprite;
        private void SetName(string name) => nameText.text = name;
    }
}