using System;
using UnityEngine;

namespace View
{
    public abstract class ASelectorView : MonoBehaviour
    {
        public abstract event Action OnStartSelected;
        public abstract event Action OnSelectNext;
        public abstract event Action OnSelectPrevious;

        public abstract void Init(Sprite iconSprite, string name);
        public abstract void SetIcon(Sprite icon);
        public abstract void SetName(string nameText);
    }
}