﻿using UnityEngine;
using View;

namespace Config
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "PrefabConfig", menuName = "Config/PrefabConfig")]
    public class PrefabConfig : ScriptableObject
    {
        [Header("Meta Game Screens")]
        public ALevelSelectionView levelSelectionPrefab;
        public Canvas backgroundPrefab;
        public AHeaderView headerPrefab;
        [Header("Core Game Screens")]
        public AHudView hudPrefab;
        public AGameBoardView gameBoardPrefab;
        public AInput inputPrefab;
        public ABoosterInventoryView boosterInventoryPrefab;
        public APauseView pausePrefab;
        public AEndGameView endGamePrefab;
        [Header("Elements")]
        public ABlockView blockPrefab;
        public ACellView cellPrefab;
        public ACellView invisibleCellPrefab;
        public ABoosterView boosterPrefab;
        public ACounterView goalCounterPrefab;
        public ACounterView restrictionCounterPrefab;
        public ACounterView scorePrefab;
        public ALevelSelectionView selectorPrefab;
        public APausePopUp pausePopUpPrefab;
        public AEndGamePopUp endGamePopUpPrefab;
    }
}