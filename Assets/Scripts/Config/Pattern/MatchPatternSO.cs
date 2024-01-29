using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "New MatchPattern", menuName = "Config/Pattern/MatchPattern")]
    public class MatchPatternSO : PatternSO
    {
        public HintPatternSO[] hintPatterns;
    }
}