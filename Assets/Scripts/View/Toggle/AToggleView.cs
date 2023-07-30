using System;
using UnityEngine;

namespace View
{
    public abstract class AToggleView : MonoBehaviour
    {
        public abstract event Action<bool> OnToggle;

        public abstract void Init(bool isOn);
    }
}