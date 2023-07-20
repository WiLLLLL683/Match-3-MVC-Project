using UnityEngine;

namespace View
{
    /// <summary>
    /// Визуальный элемент счетчика ходов или целей.<br/>
    /// </summary>
    public interface ICounterView
    {
        void Init(Sprite iconSprite, int initialNumber);
        void ChangeCount(int count);
        void ChangeIcon(Sprite iconSprite);
    }
}