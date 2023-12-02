using Model.Objects;
using System;

namespace View.Factories
{
    public interface IBlockViewFactory
    {
        IBlockView Create(Block model);
    }
}