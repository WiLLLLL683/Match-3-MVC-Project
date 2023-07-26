using System;
using System.Collections;
using UnityEngine;

namespace View
{
    public class LevelSelectionView : ALevelSelectionView, ILevelSelection_Input
    {
        public override event Action OnInputStart;
        public override event Action OnInputNext;
        public override event Action OnInputPrevious;

        public override void SetLevelIcon(Sprite icon)
        {
            //TODO
            Debug.Log("Level icon set");
        }
        public void Input_Start() => OnInputStart?.Invoke();
        public void Input_Next() => OnInputNext?.Invoke();
        public void Input_Previous() => OnInputPrevious?.Invoke();
    }
}