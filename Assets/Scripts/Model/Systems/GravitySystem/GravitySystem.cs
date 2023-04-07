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
            for (int x = 0; x < level.gameBoard.Cells.GetLength(0); x++)
            {
                for (int y = level.gameBoard.Cells.GetLength(1); y >= 0; y--) //�������� ����� ����� ����� �� ���� ������
                {
                    TryMoveBlockDown(x, y);
                }
            }
        }



        private void TryMoveBlockDown(int x, int y)
        {
            int lowestY = y;
            for (int i = level.gameBoard.Cells.GetLength(1) - 1; i > y; i--)
            {
                if (level.gameBoard.Cells[x, i].IsEmpty)
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
                SwapBlocksAction action = new SwapBlocksAction(level.gameBoard.Cells[x, y], level.gameBoard.Cells[x, lowestY]);
                action.Execute();
            }
        }
    }
}