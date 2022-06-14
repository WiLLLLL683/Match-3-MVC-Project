using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    public class GameBoard
    {
        public Cell[,] cells { get; private set; }

        public GameBoard(int xLength, int yLength)
        {
            cells = new Cell[xLength, yLength];

            for (int i = 0; i < xLength; i++)
            {
                for (int j = 0; j < yLength; j++)
                {
                    cells[i, j] = new Cell(true,new BasicCell());
                    //TODO загрузка начального состояния клеток
                }
            }
        }


    }
}
