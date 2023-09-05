using Model;
using Model.Objects;
using Model.Services;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    /// <summary>
    /// Паттерн для нахождения подсказок для следующего хода
    /// </summary>
    [CreateAssetMenu(fileName = "HintPattern", menuName = "Data/HintPattern")]
    public class HintPatternSO : PatternSO
    {
        public Vector2Int cellToMove;
        public Directions directionToMove;
    }
}