using UnityEngine;

namespace View
{
    /// <summary>
    /// Визуальный элемент счетчика ходов или целей.<br/>
    /// </summary>
    public abstract class ICounterView : MonoBehaviour
    {
        public abstract void Init(Sprite iconSprite, int initialNumber);
        public abstract void ChangeCount(int count);
        public abstract void ChangeIcon(Sprite iconSprite);
    }
}