using Presenter;
using Data;
using Model.Infrastructure;
using System;
using System.Collections;
using UnityEngine;
using AYellowpaper;
using View;

public class Bootstrap : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PrefabConfig prefabs;
    [Header("Current State")]
    [SerializeField] private LevelData selectedLevel;

    private Game game;
    private MetaGameFactory metaGameFactory;
    private CoreGameFactory coreGameFactory;

    private bool isCoreGameLoaded;

    private void Awake()
    {
        game = new();
        coreGameFactory = new(game, prefabs, this);
        metaGameFactory = new(game, prefabs, this);

        LoadMetaGame();
    }
    public void LoadMetaGame()
    {
        if (isCoreGameLoaded)
            UnloadCoreGame();
        metaGameFactory.Create();
        isCoreGameLoaded = false;
    }
    public void LoadCoreGame()
    {
        if (!isCoreGameLoaded)
            UnloadMetaGame();
        coreGameFactory.Create(selectedLevel);
        isCoreGameLoaded = true;
    }



    private void UnloadCoreGame() => coreGameFactory.Clear();
    private void UnloadMetaGame() => metaGameFactory.Clear();
}
