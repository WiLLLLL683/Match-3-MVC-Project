using System;
using UnityEngine;

namespace View
{
    /// <summary>
    /// Визуальный элемент бустера, вызывается в IBoosterPresenter.
    /// </summary>
    public abstract class IBoosterView : MonoBehaviour
    {
        public abstract event Action OnActivate;

        public abstract void Init(Sprite iconSprite, int initialNumber);
        public abstract void ChangeAmount(int boosterAmmount);
        public abstract void ChangeIcon(Sprite iconSprite);
        public abstract void DisableButton();
        public abstract void EnableButton();
    }
}
