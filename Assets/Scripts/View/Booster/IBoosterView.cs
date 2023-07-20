using System;
using UnityEngine;

namespace View
{
    /// <summary>
    /// Визуальный элемент бустера, вызывается в IBoosterPresenter.
    /// </summary>
    public interface IBoosterView
    {
        public event Action OnActivate;

        public void Init(Sprite iconSprite, int initialNumber);
        public void ChangeAmount(int boosterAmmount);
        public void ChangeIcon(Sprite iconSprite);
        public void DisableButton();
        public void EnableButton();
    }
}
