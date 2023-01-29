using Model;
using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    /// <summary>
    /// Паттерн для нахождения подсказок для следующего хода
    /// </summary>
    [CreateAssetMenu(fileName = "HintPattern", menuName = "Data/HintPattern")]
    public class HintPattern : Pattern
    {
        [SerializeField] private Vector2Int cellToMove;
        [SerializeField] private Directions directionToMove;

        public HintPattern(bool[,] _grid, Vector2Int _cellToMove, Directions _directionToMove) : base(_grid)
        {
            grid = _grid;
            cellToMove = _cellToMove;
            directionToMove = _directionToMove;

            originPosition = GetOriginPosition();
            totalSum = CalculateTotalSum();
        }

        public void GetHint(GameBoard _gameBoard, Vector2Int _startPosition, InputMoveDelegate _hintMove)
        {
            if (Match(_gameBoard,_startPosition).Count == 0)
            {
                return;
            }

            Vector2Int cellToMoveOnGameboard = new Vector2Int(cellToMove.x + _startPosition.x, cellToMove.y + _startPosition.y);

            _hintMove?.Invoke(cellToMoveOnGameboard, directionToMove);
        }
    }
}