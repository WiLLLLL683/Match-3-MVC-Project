using System;
using View;

namespace Config
{
    [Serializable]
    public class PrefabConfig
    {
        public BlockView blockViewPrefab;
        public CounterView goalCounterPrefab;
        public CounterView restrictionCounterPrefab;
        public BoosterButtonView boosterButtonPrefab;
    }
}