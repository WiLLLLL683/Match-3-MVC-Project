using Model.Readonly;
using Presenter;
using System;
using System.Collections.Generic;
using UnityEngine;
using View;

public class GameBoardFactory : AFactory<IGameBoard_Readonly, IGameBoardView, IGameBoardPresenter>
{
    private readonly AFactory<IBlock_Readonly, IBlockView, IBlockPresenter> blockFactory;
    private readonly AFactory<ICell_Readonly, ICellView, ICellPresenter> cellFactory;

    public GameBoardFactory(
        IGameBoardView viewPrefab,
        AFactory<IBlock_Readonly, IBlockView, IBlockPresenter> blockFactory,
        AFactory<ICell_Readonly, ICellView, ICellPresenter> cellFactory, 
        Transform parent = null) : base(viewPrefab, parent)
    {
        this.blockFactory = blockFactory;
        this.cellFactory = cellFactory;
    }

    public override IGameBoardView Create(IGameBoard_Readonly model, out IGameBoardPresenter presenter)
    {
        IGameBoardView view = GameObject.Instantiate(viewPrefab, parent);
        presenter = new GameBoardPresenter(model, view, blockFactory, cellFactory);
        presenter.Enable();
        allPresenters.Add(presenter);
        return view;
    }
}
