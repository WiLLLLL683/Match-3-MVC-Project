using UnityEngine;
using Model.Objects;
using Model.Services;

namespace Config
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Turn", menuName = "Config/Turn")]
    public class TurnSO : CounterTargetSO
    {
        private const int TURN_ID = -100;
        public override ICounterTarget CounterTarget { get; } = new Turn(TURN_ID);
    }
}