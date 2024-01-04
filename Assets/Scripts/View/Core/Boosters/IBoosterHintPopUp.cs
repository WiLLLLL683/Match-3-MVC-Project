using System;
using UnityEngine;

namespace View
{
    public interface IBoosterHintPopUp
    {
        void Show(Sprite icon, string name, string hint, bool showGameBoard = true);
        void Hide();
    }
}