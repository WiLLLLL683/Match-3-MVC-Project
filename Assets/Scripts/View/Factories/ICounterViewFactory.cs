using Model.Objects;
using UnityEngine;

namespace View.Factories
{
    public interface ICounterViewFactory
    {
        ICounterView CreateRestriction(Counter model);
        ICounterView CreateGoal(Counter model);
    }
}