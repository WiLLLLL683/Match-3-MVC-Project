using Model;
using Model.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������� ��� ���������� ���������� ������, ����������� � ���
/// </summary>
public class Pattern
{
    private bool[,] grid;
    private int totalSum; //����� ��������� ������ � ��������
    private Vector2Int originPosition = new Vector2Int(0,0);
    private Type originType;

    public Pattern(bool[,] _grid)
    {
        grid = _grid;
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                if (grid[x,y] == true)
                {
                    //����� ��������� ����� ���������� ������ ����� ����� ����� �������� ��� ���������
                    originPosition.x = x;
                    originPosition.y = y;
                    //���������� ����� ���������� ������
                    totalSum++;
                }
            }
        }
    }

    public List<Cell> FindPattern(GameBoard _gameBoard, Vector2Int _startPosition)
    {
        //���� �� �������?
        if (totalSum == 0)
            return null;

        Vector2Int originPosOnGameboard = new Vector2Int(originPosition.x + _startPosition.x, originPosition.y + _startPosition.y);

        //���� �� ������ ��������� �� ����?
        if (!Helpers.CheckValidCellByPosition(_gameBoard, originPosOnGameboard))
            return null;

        //����� ��� ������������� �����
        originType = _gameBoard.cells[originPosOnGameboard.x, originPosOnGameboard.y].block.type.GetType();

        int sum = 0;
        List<Cell> matchedCells = new List<Cell>();

        //��������� � ���������� ����������, ������ �� ����������� ��������
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                Vector2Int posOnGameboard = new Vector2Int(x + _startPosition.x, y + _startPosition.y);
                
                //�������� �� ������?
                if (grid[x,y] == false)
                    continue;

                //���� �� ������ �� ����?
                if (!Helpers.CheckValidCellByPosition(_gameBoard, posOnGameboard))
                    continue;

                //����������� �� ������?
                if (!_gameBoard.cells[posOnGameboard.x, posOnGameboard.y].IsPlayable)
                    continue;

                //���� �� � ������ ����?
                if (_gameBoard.cells[posOnGameboard.x, posOnGameboard.y].isEmpty)
                    continue;

                //��������� �� ���� ������?
                if (_gameBoard.cells[posOnGameboard.x,posOnGameboard.y].block.type.GetType().Equals(originType))
                {
                    sum ++;
                    matchedCells.Add(_gameBoard.cells[posOnGameboard.x, posOnGameboard.y]);
                }
            }
        }

        //��� �� ���������� ������ �������� �������?
        if (sum == totalSum)
            return matchedCells;
        else
            return null;
    }
}
