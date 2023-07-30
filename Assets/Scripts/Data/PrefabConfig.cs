using AYellowpaper;
using Presenter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using View;

namespace Data
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "PrefabConfig", menuName = "Config/PrefabConfig")]
    public class PrefabConfig : ScriptableObject
    {
        [Header("Meta Game Screens")]
        public ALevelSelectionScreen levelSelectionPrefab;
        public ABackgroundScreen backgroundPrefab;
        public AHeaderScreen headerPrefab;
        [Header("Core Game Screens")]
        public AHudScreen hudPrefab;
        public AGameBoardScreen gameBoardPrefab;
        public AInput inputPrefab;
        public ABoosterInventoryScreen boosterInventoryPrefab;
        public APauseScreen pausePrefab;
        public AEndGameScreen endGamePrefab;
        [Header("Elements")]
        public IBlockView blockPrefab;
        public ICellView cellPrefab;
        public IBoosterView boosterPrefab;
        public ICounterView goalCounterPrefab;
        public ICounterView restrictionCounterPrefab;
        public ICounterView scorePrefab;
        public ASelectorView selectorPrefab;
        public APausePopUp pausePopUpPrefab;
        public AEndGamePopUp endGamePopUpPrefab;
    }
}