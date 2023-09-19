﻿using UnityEngine;

namespace Config
{
    /// <summary>
    /// Паттерн для нахождения подсказок для следующего хода
    /// </summary>
    [CreateAssetMenu(fileName = "HintPattern", menuName = "Config/HintPattern")]
    public class HintPatternSO : PatternSO
    {
        public Vector2Int cellToMove;
        public Directions directionToMove;
    }
}