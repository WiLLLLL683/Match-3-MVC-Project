using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public enum Directions
    {
        Up, Down, Left, Right, Zero
    }

    public class Helpers 
    {
        public static bool CheckValidCellByPosition(GameBoard _gameBoard, Vector2Int _position)
        {
            //������� � �������� �������� ����?
            if (_position.x >= 0 &&
                _position.y >= 0 &&
                _position.x < _gameBoard.cells.GetLength(0) &&
                _position.y < _gameBoard.cells.GetLength(1))
            {
                return true;
            }
            else
            {
                Debug.LogError("Cell position out of GameBoards range");
                return false;
            }
        }

        public static bool CheckValidBlockByPosition(GameBoard _gameBoard, Vector2Int _position)
        {
            //������� ��� ������ �������� ����?
            if (!CheckValidCellByPosition(_gameBoard, _position))
            {
                return false;
            }

            //���������� �� ������?
            if (!_gameBoard.cells[_position.x, _position.y].IsPlayable)
            {
                Debug.LogError("Tried to get Block but Cell was notPlayable");
                return false;
            }

            //���� �� ���� � ������?
            if (_gameBoard.cells[_position.x, _position.y].isEmpty)
            {
                Debug.LogError("Tried to get Block but Cell was empty");
                return false;
            }

            return true;
        }
    }
}
