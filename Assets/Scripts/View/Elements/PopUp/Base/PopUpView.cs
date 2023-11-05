using System;
using UnityEngine;

namespace View
{
    /// <summary>
    /// Базовое всплывающее окно, которое может быть показано и скрыто
    /// Получает инпут перехода на следующий уровень, рестарта и выхода из кор-игры.
    /// </summary>
    public class PopUpView : MonoBehaviour, IPopUpView, IPopUpInput
    {
        [SerializeField] private GameObject popUpContainer;

        public event Action OnShow;
        public event Action OnHide;
        public event Action OnNextLevelInput;
        public event Action OnReplayInput;
        public event Action OnQuitInput;

        public virtual void Show()
        {
            popUpContainer.SetActive(true);
            OnShow?.Invoke();
        }
        public virtual void Hide()
        {
            popUpContainer.SetActive(false);
            OnHide?.Invoke();
        }

        public void Input_NextLevel() => OnNextLevelInput?.Invoke();
        public void Input_Replay() => OnReplayInput?.Invoke();
        public void Input_Quit() => OnQuitInput?.Invoke();
    }
}