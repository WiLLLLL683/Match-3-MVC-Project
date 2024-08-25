using Model.Factories;
using Model.Objects;
using Model.Services;
using System.Collections.Generic;

namespace TestUtils
{
    public static class TestServicesFactory
    {
        /// <summary>
        /// Создать инвентарь с 1 валютой заданного типа
        /// </summary>
        public static CurrencyService CreateCurrencyService(CurrencyType type, int amount)
        {
            Game game = new();
            game.CurrencyInventory.currencies.Add(type, amount);
            CurrencyService Service = new(game);
            return Service;
        }

        public static List<BlockType_Weight> CreateListOfWeights(params int[] typeIds)
        {
            List<BlockType_Weight> list = new();
            for (int i = 0; i < typeIds.Length; i++)
            {
                list.Add(new(TestBlockFactory.CreateBlockType(typeIds[i]), 100));
            }
            return list;
        }
    }
}