using System;
using UnityEngine;

namespace View
{
    public abstract class AEndGameView : MonoBehaviour
    {
        public abstract event Action OnNextLevelInput;
        public abstract event Action OnReplayInput;
        public abstract event Action OnQuitInput;

        public abstract void HideAllMenus();
        public abstract void Init();
        public abstract void ShowCompleteMenu(int score, int stars);
        public abstract void ShowDefeatMenu(int score);
    }
}