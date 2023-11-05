using UnityEngine;

namespace View
{
    /// <summary>
    /// Визуальный элемент счетчика ходов или целей.<br/>
    /// </summary>
    public interface ICounterView
    {
        public void Init(Sprite iconSprite, int initialNumber);
        public void ChangeCount(int count);
        public void ChangeIcon(Sprite iconSprite);
    }
}