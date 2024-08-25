using Config;
using Model.Services;
using NaughtyAttributes;
using System;
using UnityEngine;
using Zenject;

namespace Utils
{
    public class CounterDebugTool : MonoBehaviour
    {
        public ACounterTargetSO counterTarget;
        public int amount = 5;

        private IWinLoseService winLoseService;
        private bool ShowButtons => counterTarget != null;

        [Inject]
        public void Construct(IWinLoseService winLoseService)
        {
            this.winLoseService = winLoseService;
        }

        [Button, EnableIf("ShowButtons")] public void Add() => winLoseService.TryIncreaseCount(counterTarget.CounterTarget, amount);

        [Button, EnableIf("ShowButtons")] public void Remove() => winLoseService.TryDecreaseCount(counterTarget.CounterTarget, amount);

        [Button, EnableIf("ShowButtons")]
        public void CountDown() => winLoseService.TryDecreaseCount(counterTarget.CounterTarget, 1);
    }
}