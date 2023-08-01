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