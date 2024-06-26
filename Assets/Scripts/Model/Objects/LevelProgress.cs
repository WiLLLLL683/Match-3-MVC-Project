﻿using System;
using System.Collections.Generic;

namespace Model.Objects
{
    [Serializable]
    public class LevelProgress
    {
        /// <summary>
        /// Индекс последнего завершенного уровня, все уровни до считаются завершенными,
        /// изначально равен -1, чтобы 0-й уровень не был помечен пройденным
        /// </summary>
        public int LastCompletedLevel = -1;

        /// <summary>
        /// Индекс последнего открытого уровня, все уровни после считаются закрытыми
        /// </summary>
        public int LastOpenedLevel;

        /// <summary>
        /// Индекс текущего уровня, он будет использован для загрузки кор-игры
        /// </summary>
        public int CurrentLevelIndex;
    }
}