using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model.Objects;
using Data;
using Model.Services;

namespace Model.Systems
{
    /// <summary>
    /// ������� ������ ���������� ������ ����������� �� ���������
    /// </summary>
    public class MatchSystem : IMatchSystem
    {
        private IValidationService validationService;
        private Level level;

        public MatchSystem(IValidationService validationService)
        {
            this.validationService = validationService;
        }

        /// <summary>
        /// �������� ������ �� ������
        /// </summary>
        public void SetLevel(Level _level)
        {
            level = _level;
        }

        /// <summary>
        /// ����� ��� ���������� �� ���� ��������� ��� �����������
        /// </summary>
        public HashSet<Cell> FindAllMatches()
        {
            HashSet<Cell> matchedCells = new();

            //�������� ���� ���������
            for (int i = 0; i < level.matchPatterns.Length; i++)
            {
                HashSet<Cell> cellsAtPattern = CheckPattern(level.matchPatterns[i]);
                matchedCells.UnionWith(cellsAtPattern);
            }

            return matchedCells;
        }

        /// <summary>
        /// ����� ������ ���������� ������� ��� ���������
        /// </summary>
        public HashSet<Cell> FindFirstHint()
        {
            HashSet<Cell> matchedCells = new();

            for (int i = 0; i < level.hintPatterns.Length; i++)
            {
                HashSet<Cell> cellsAtPattern = CheckFirstPattern(level.hintPatterns[i]);
                matchedCells.UnionWith(cellsAtPattern);
            }

            return matchedCells; //TODO ������� ������ ������ ������� ����� ������� ��� ���������
        }



        private HashSet<Cell> CheckPattern(Pattern _pattern)
        {
            HashSet<Cell> matchedCells = new();

            //������ �� ���� ������� �������� ����(����� ���������) � ��������� ��������� ������
            for (int x = 0; x < level.gameBoard.cells.GetLength(0); x++)
            {
                for (int y = 0; y < level.gameBoard.cells.GetLength(1) - level.gameBoard.RowsOfInvisibleCells; y++)
                {
                    HashSet<Cell> cellsAtPos = _pattern.Match(level.gameBoard, new Vector2Int(x, y), validationService);
                    matchedCells.UnionWith(cellsAtPos);
                }
            }

            //������� ��������� ������
            return matchedCells;
        }

        private HashSet<Cell> CheckFirstPattern(Pattern _pattern)
        {
            //������ �� ���� ������� �������� ����(����� ���������) � ������� ������ ��������� ������
            for (int x = 0; x < level.gameBoard.cells.GetLength(0); x++)
            {
                for (int y = 0; y < level.gameBoard.cells.GetLength(1) - level.gameBoard.RowsOfInvisibleCells; y++)
                {
                    HashSet<Cell> cellsAtPos = _pattern.Match(level.gameBoard, new Vector2Int(x, y), validationService);
                    if (cellsAtPos.Count > 0)
                    {
                        return cellsAtPos;
                    }
                }
            }

            //���� ���������� ���, �� ������� ������ ������
            return new HashSet<Cell>();
        }
    }
}