using System;

namespace Model.Objects
{
    /// <summary>
    /// �������� ������ ������ ����
    /// �������� ����������� ��� �������� � �����������
    /// </summary>
    [Serializable]
    public class Game
    {
        //meta game
        public LevelProgress LevelProgress = new();
        public PlayerSettings PlayerSettings = new();
        public CurrencyInventory CurrencyInventory = new();
        public BoosterInventory BoosterInventory = new();

        //core game
        public Level CurrentLevel;
    }
}