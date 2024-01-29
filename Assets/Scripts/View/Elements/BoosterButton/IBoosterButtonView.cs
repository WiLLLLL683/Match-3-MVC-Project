using System;
using UnityEngine;

namespace View
{
    /// <summary>
    /// Кнопка запуска бустера, вызывается в IBoosterPresenter.
    /// </summary>
    public interface IBoosterButtonView
    {
        int Id { get; }
        public GameObject gameObject { get; }

        public event Action<IBoosterButtonView> OnActivate;

        public void Init(int id, Sprite iconSprite, int initialNumber, bool isEnabled);
        public void ChangeAmount(int boosterAmmount);
        public void ChangeIcon(Sprite iconSprite);
        public void EnableButton(bool isEnabled);
    }
}
