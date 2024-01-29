using System;
using UnityEngine;

namespace View
{
    public interface IBoosterHintPopUp
    {
        event Action<Vector2Int> OnInputActivate;
        event Action OnInputHide;

        void ShowOverlayWithButton();
        void ShowOverlayWithGameBoard();
        void Show(Sprite icon, string name, string hint);
        void Hide();
    }
}