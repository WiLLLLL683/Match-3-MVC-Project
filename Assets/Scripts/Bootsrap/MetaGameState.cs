using Data;
using Model.Infrastructure;
using Presenter;
using UnityEngine;
using Utils;

public class MetaGameState : IState
{
    private Game game;
    private PrefabConfig prefabs;
    private Bootstrap bootstrap;

    private ILevelSelectionPresenter levelSelection;
    private Canvas background;
    private Canvas header;
    private LevelSelectionFactory levelSelectionFactory;

    public MetaGameState(Game game, PrefabConfig prefabs, Bootstrap bootstrap)
    {
        this.game = game;
        this.prefabs = prefabs;
        this.bootstrap = bootstrap;
    }

    public void OnStart()
    {
        levelSelectionFactory = new LevelSelectionFactory(prefabs.levelSelectionPrefab, bootstrap);

        //создание окон вью
        levelSelectionFactory.Create(game.LevelSelection, out levelSelection);
        background = GameObject.Instantiate(prefabs.backgroundPrefab);
        header = GameObject.Instantiate(prefabs.headerPrefab);

        //TODO game.StartMetaGame();
    }
    public void OnEnd()
    {
        levelSelectionFactory.Clear();
        GameObject.Destroy(background.gameObject);
        GameObject.Destroy(header.gameObject);
    }
}
