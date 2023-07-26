﻿using AYellowpaper;
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
        public InterfaceReference<ILevelSelectionController, MonoBehaviour> levelSelectionPrefab;
        public Canvas backgroundPrefab;
        public Canvas headerPrefab;
        [Header("Core Game Screens")]
        public InterfaceReference<IHudPresenter, MonoBehaviour> hudPrefab;
        public IGameBoardView gameBoardPrefab;
        public InterfaceReference<IInput, MonoBehaviour> inputPrefab;
        public BoosterInventoryViewBase boosterInventoryPrefab;
        public InterfaceReference<IPausePresenter, MonoBehaviour> pausePrefab;
        public InterfaceReference<IEndGamePresenter, MonoBehaviour> endGamePrefab;
        [Header("Elements")]
        public IBlockView blockPrefab;
        public ICellView cellPrefab;
        public IBoosterView boosterPrefab;
        public ICounterView goalCounterPrefab;
        public ICounterView restrictionCounterPrefab;
    }
}