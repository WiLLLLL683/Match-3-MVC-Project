using UnityEngine;
using Model.Objects;
using Model.Services;

namespace Config
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Turn", menuName = "Config/Turn")]
    public class TurnSO : CounterTargetSO
    {
        public override ICounterTarget CounterTarget => turn;

        private readonly ICounterTarget turn = new Turn();
    }
}