using Config;
using Model.Infrastructure;
using UnityEngine;
using Utils;

public class Bootstrap : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PrefabConfig prefabs;
    [SerializeField] private LevelSO[] allLevels;
    [SerializeField] private CellTypeSetSO allCellTypes;
    [Header("Current State")]
    [SerializeField] private LevelSO selectedLevel;
    public LevelSO SelectedLevel => selectedLevel;
    [NaughtyAttributes.ShowNativeProperty()] public string gameModelState => game?.CurrentStateName;

    [SerializeField] private Game game;
    private StateMachine<IState> stateMachine;

    private void Awake()
    {
        game = new(allCellTypes);
        stateMachine = new();
        stateMachine.AddState(new MetaGameState(game, prefabs, allLevels, this));
        stateMachine.AddState(new CoreGameState(game, prefabs, allCellTypes, this));

        LoadMetaGame();
    }
    public void LoadMetaGame() => stateMachine.SetState<MetaGameState>();
    public void LoadCoreGame() => stateMachine.SetState<CoreGameState>();
}
