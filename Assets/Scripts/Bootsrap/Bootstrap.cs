using Model.Infrastructure;
using UnityEngine;
using Zenject;

public class Bootstrap : MonoBehaviour
{
    [NaughtyAttributes.ShowNativeProperty()] public string gameModelState => game?.CurrentStateName;

    [SerializeField] private Game game;
    private SceneLoader sceneLoader;

    [Inject]
    public void Construct(Game game, SceneLoader sceneLoader)
    {
        this.game = game;
        this.sceneLoader = sceneLoader;
    }

    private void Awake()
    {
        game.Init();
        sceneLoader.LoadMetaGame();
    }
}
