using System;
using UnityEngine;

namespace View
{
    public class PopUp : MonoBehaviour
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