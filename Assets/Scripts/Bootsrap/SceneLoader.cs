using Config;
using Model.Infrastructure;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneLoader //TODO нейминг? не понятная ответственность
{
    public LevelSO CurrentLevel { get; private set; }

    private readonly ZenjectSceneLoader loader;
    private readonly Game game;

    private const string META_SCENE_NAME = "Meta";
    private const string CORE_SCENE_NAME = "Core";

    public SceneLoader(ZenjectSceneLoader loader, Game game)
    {
        this.loader = loader;
        this.game = game;
    }

    public void LoadMetaGame() => loader.LoadSceneAsync(META_SCENE_NAME);
    public void LoadCoreGame(LevelSO level)
    {
        Debug.Log($"Loading level: {level.levelName}");
        CurrentLevel = level;
        game.StartLevel(level);
        loader.LoadSceneAsync(CORE_SCENE_NAME);
    }
}
