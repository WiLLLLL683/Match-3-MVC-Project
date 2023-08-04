using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    /// <summary>
    /// Визуальный элемент счетчика ходов или целей.<br/>
    /// Часть HUD, может изменять иконку и текст с количеством.
    /// </summary>
    public class CounterView : ACounterView
    {
        [SerializeField] private TMP_Text countText;
        [SerializeField] private Image icon;

        public override void Init(Sprite iconSprite, int initialCount)
        {
            ChangeIcon(iconSprite);
            ChangeCount(initialCount);
        }

        public override void ChangeCount(int count) => countText.text = count.ToString();
        public override void ChangeIcon(Sprite iconSprite)
        {
            if (icon != null && iconSprite != null)
            {
                icon.sprite = iconSprite;
            }
        }
    }
}