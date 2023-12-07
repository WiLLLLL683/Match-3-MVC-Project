using Model.Objects;
using UnityEngine;
using Utils;

namespace Config
{
    /// <summary>
    /// Паттерн для нахождения подсказок для следующего хода
    /// </summary>
    [CreateAssetMenu(fileName = "New HintPattern", menuName = "Config/HintPattern")]
    public class HintPatternSO : PatternSO
    {
        public Vector2Int cellToMove;
        public Directions directionToMove;
    }
}