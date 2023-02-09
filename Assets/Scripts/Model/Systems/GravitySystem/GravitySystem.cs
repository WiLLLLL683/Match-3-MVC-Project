using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Systems
{
    /// <summary>
    /// ������� ��� ����������� ������ ����, ���� ������ ����� �����
    /// </summary>
    public class GravitySystem : IGravitySystem
    {
        private Level level;

        /// <summary>
        /// �������� ������ �� ������
        /// </summary>
        public void SetLevel(Level _level)
        {
            level = _level;
        }

        /// <summary>
        /// ����������� ��� "������� � �������" ����� ����
        /// </summary>
        public void Execute()
        {
            for (int x = 0; x < level.gameBoard.cells.GetLength(0); x++)
            {
                for (int y = level.gameBoard.cells.GetLength(1); y >= 0; y--) //�������� ����� ����� ����� �� ���� ������
                {
                    TryMoveBlockDown(x, y);
                }
            }
        }



        private void TryMoveBlockDown(int x, int y)
        {
            int lowestY = y;
            for (int i = level.gameBoard.cells.GetLength(1) - 1; i > y; i--)
            {
                if (level.gameBoard.cells[x, i].isEmpty)
                {
                    lowestY = i;
                    break;
                }
            }

            if (lowestY == y)
            {
                return;
            }
            else
            {
                SwapBlocksAction action = new SwapBlocksAction(level.gameBoard.cells[x, y], level.gameBoard.cells[x, lowestY]);
                action.Execute();
            }
        }

        /// <summary>
        /// for tests only
        /// </summary>
        public Level GetLevel() => level;
    }
}