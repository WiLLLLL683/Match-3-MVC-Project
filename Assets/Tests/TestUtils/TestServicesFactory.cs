using Model.Objects;
using Model.Services;
using System.Collections.Generic;

namespace TestUtils
{
    public static class TestServicesFactory
    {
        /// <summary>
        /// Указать размеры и типы блоков построчно
        /// </summary>
        public static BlockRandomTypeService CreateRandomBlockTypeService(params int[] typeIds)
        {
            var service = new BlockRandomTypeService();
            service.SetLevelConfig(CreateListOfWeights(typeIds), TestBlockFactory.DefaultBlockType);
            return service;
        }

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

        private static List<BlockType_Weight> CreateListOfWeights(params int[] typeIds)
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