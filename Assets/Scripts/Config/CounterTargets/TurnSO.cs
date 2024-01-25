using UnityEngine;
using Model.Objects;
using Model.Services;
using NaughtyAttributes;

namespace Config
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Turn", menuName = "Config/Turn")]
    public class TurnSO : ACounterTargetSO
    {
        [ShowNonSerializedField] private const int id = -100;
        public override ICounterTarget CounterTarget { get; } = new Turn(id);
    }
}