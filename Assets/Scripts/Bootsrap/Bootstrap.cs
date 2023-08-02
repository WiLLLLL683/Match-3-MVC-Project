using Presenter;
using Data;
using Model.Infrastructure;
using System;
using System.Collections;
using UnityEngine;
using AYellowpaper;
using View;
using Utils;

public class Bootstrap : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PrefabConfig prefabs;
    [SerializeField] private LevelData[] allLevels;
    [Header("Current State")]
    [SerializeField] private LevelData selectedLevel;
    public LevelData SelectedLevel => selectedLevel;
    [NaughtyAttributes.ShowNativeProperty()] public string gameModelState => game?.CurrentStateName;

    private Game game;
    private StateMachine<IState> stateMachine;

    private void Awake()
    {
        game = new(allLevels, 0); //TODO загрузка сохранения
        stateMachine = new();
        stateMachine.AddState(new MetaGameState(game, prefabs, this));
        stateMachine.AddState(new CoreGameState(game, prefabs, this));

        LoadMetaGame();
    }
    public void LoadMetaGame() => stateMachine.SetState<MetaGameState>();
    public void LoadCoreGame() => stateMachine.SetState<CoreGameState>();
}
