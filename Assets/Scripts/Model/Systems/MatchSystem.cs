using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model.Objects;

namespace Model.Systems
{
    /// <summary>
    /// ������� ������ ���������� ������ ����������� �� ���������
    /// </summary>
    public class MatchSystem
    {
        private Level level;

        public MatchSystem(Level _level)
        {
            level = _level;
        }

        /// <summary>
        /// ����� ��� ���������� �� ���� ��������� ��� �����������
        /// </summary>
        public List<Cell> FindMatches()
        {
            List<Cell> matchedCells = new List<Cell>();

            for (int i = 0; i < level.matchPatterns.Length; i++)
            {
                List<Cell> cellsAtPattern = CheckPattern(level.matchPatterns[i]);
                matchedCells.AddRange(cellsAtPattern);
            }

            return matchedCells;
        }

        /// <summary>
        /// ����� ������ ���������� ������� ��� ��������� 
        /// </summary>
        public List<Cell> FindFirstHint()
        {
            List<Cell> matchedCells = new List<Cell>();

            for (int i = 0; i < level.hintPatterns.Length; i++)
            {
                List<Cell> cellsAtPattern = CheckFirstPattern(level.hintPatterns[i]);
                matchedCells.AddRange(cellsAtPattern);
            }

            return matchedCells; //TODO ������� ������ ������ ������� ����� ������� ��� ���������
        }

        private List<Cell> CheckPattern(Pattern _pattern)
        {
            List<Cell> matchedCells = new List<Cell>();

            //������ �� ���� ������� �������� ���� � ��������� ��������� ������
            for (int x = 0; x < level.gameBoard.cells.GetLength(0); x++)
            {
                for (int y = 0; y < level.gameBoard.cells.GetLength(1); y++)
                {
                    List<Cell> cellsAtPos = _pattern.Match(level.gameBoard, new Vector2Int(x,y));
                    matchedCells.AddRange(cellsAtPos);
                }
            }

            //������� ��������� ������
            return matchedCells;
        }

        private List<Cell> CheckFirstPattern(Pattern _pattern)
        {
            //������ �� ���� ������� �������� ���� � ������� ������ ��������� ������
            for (int x = 0; x < level.gameBoard.cells.GetLength(0); x++)
            {
                for (int y = 0; y < level.gameBoard.cells.GetLength(1); y++)
                {
                    List<Cell> cellsAtPos = _pattern.Match(level.gameBoard, new Vector2Int(x, y));
                    if (cellsAtPos.Count > 0)
                    {
                        return cellsAtPos;
                    }
                }
            }

            //���� ���������� ���, �� ������� ������ ������
            return new List<Cell>();
        }
    }
}