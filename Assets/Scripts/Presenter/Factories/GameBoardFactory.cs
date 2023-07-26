using Model.Readonly;
using Presenter;
using System;
using System.Collections.Generic;
using UnityEngine;
using View;

public class GameBoardFactory : FactoryBase<IGameBoard_Readonly, IGameBoardView, IGameBoardPresenter>
{
    private readonly FactoryBase<IBlock_Readonly, IBlockView, IBlockPresenter> blockFactory;
    private readonly FactoryBase<ICell_Readonly, ICellView, ICellPresenter> cellFactory;

    public GameBoardFactory(
        IGameBoardView viewPrefab,
        FactoryBase<IBlock_Readonly, IBlockView, IBlockPresenter> blockFactory,
        FactoryBase<ICell_Readonly, ICellView, ICellPresenter> cellFactory, 
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
