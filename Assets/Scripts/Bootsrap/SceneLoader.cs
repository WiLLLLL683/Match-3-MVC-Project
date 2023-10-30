using Config;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneLoader
{
    private readonly ZenjectSceneLoader loader;

    private const string META_SCENE_NAME = "Meta";
    private const string CORE_SCENE_NAME = "Core";

    public SceneLoader(ZenjectSceneLoader loader)
    {
        this.loader = loader;
    }

    public void LoadMetaGame() => loader.LoadSceneAsync(META_SCENE_NAME);
    public void LoadCoreGame(LevelSO level) => loader.LoadSceneAsync(CORE_SCENE_NAME);
}
