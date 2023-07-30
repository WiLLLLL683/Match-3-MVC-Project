﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public abstract class APausePopUp : PopUp
    {
        public abstract event Action<bool> OnSoundIsOn;
        public abstract event Action<bool> OnVibrationIsOn;

        public abstract void Init(bool soundOnStart, bool vibrationOnStart);
    }
}