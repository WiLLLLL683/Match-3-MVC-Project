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
        public class Factory : PlaceholderFactory<CounterView> { }

        [SerializeField] private TMP_Text countText;
        [SerializeField] private Image icon;

        public void Init(Sprite iconSprite, int initialCount)
        {
            ChangeIcon(iconSprite);
            ChangeCount(initialCount);
        }

        public void ChangeCount(int count) => countText.text = count.ToString();
        public void ChangeIcon(Sprite iconSprite)
        {
            if (icon != null && iconSprite != null)
            {
                icon.sprite = iconSprite;
            }
        }
    }
}