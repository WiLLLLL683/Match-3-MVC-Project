using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View
{
    /// <summary>
    /// Визуальный элемент счетчика ходов или целей.<br/>
    /// Часть HUD, может изменять иконку и текст с количеством.
    /// </summary>
    public class CounterView : MonoBehaviour, ICounterView
    {
        [SerializeField] private TMP_Text countText;
        [SerializeField] private Image icon;
        [SerializeField] private Image completeIcon;

        public void Init(Sprite iconSprite, int initialCount)
        {
            ChangeIcon(iconSprite);
            ChangeCount(initialCount);
        }

        public void ChangeCount(int count)
        {
            if (countText == null)
                return;

            countText.text = count.ToString();
        }

        public void ChangeIcon(Sprite iconSprite)
        {
            if (icon == null || iconSprite == null)
                return;

            icon.sprite = iconSprite;
        }

        public void ShowCompleteIcon()
        {
            if (countText == null || completeIcon == null)
                return;

            countText.enabled = false;
            completeIcon.enabled = true;
        }
    }
}