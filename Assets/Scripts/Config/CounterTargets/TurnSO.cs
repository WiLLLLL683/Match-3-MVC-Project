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
        [SerializeField] private int id = -100;
        public override ICounterTarget CounterTarget => counterTarget;
        private ICounterTarget counterTarget;

        private void OnValidate()
        {
            counterTarget = new Turn(id);
        }
    }
}