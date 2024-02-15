using Model.Objects;
using System;
using UnityEngine;
using Zenject;

namespace Model.Factories
{
    public class BlockFactory : IBlockFactory
    {
        private readonly IInstantiator instantiator;

        public BlockFactory(IInstantiator instantiator)
        {
            this.instantiator = instantiator;
        }

        public Block Create(IBlockType blockTypeOrigin, Vector2Int position)
        {
            Type type = blockTypeOrigin.GetType();
            IBlockType blockType = (IBlockType)instantiator.Instantiate(type);
            blockType.Id = blockTypeOrigin.Id;
            return new Block(blockType, position);
        }

        //TODO Create random
    }
}