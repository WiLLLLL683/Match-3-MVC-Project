﻿using System;

namespace Model.Objects
{
    [Serializable]
    public class PlayerSettings
    {
        public bool IsSoundOn;
        public bool IsVibrationOn;

        public PlayerSettings(bool isSoundOn, bool isVibrationOn)
        {
            this.IsSoundOn = isSoundOn;
            this.IsVibrationOn = isVibrationOn;
        }
    }
}