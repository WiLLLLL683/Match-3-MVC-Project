using System;
using UnityEngine;

namespace View
{
    public interface IToggleView
    {
        public event Action<bool> OnToggle;

        public void Init(bool isOn);
    }
}