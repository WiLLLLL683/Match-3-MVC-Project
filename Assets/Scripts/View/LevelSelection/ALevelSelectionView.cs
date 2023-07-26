using System;
using UnityEngine;

namespace View
{
    public abstract class ALevelSelectionView : MonoBehaviour
    {
        public abstract event Action OnInputStart;
        public abstract event Action OnInputNext;
        public abstract event Action OnInputPrevious;

        public abstract void SetLevelIcon(Sprite icon);
    }
}