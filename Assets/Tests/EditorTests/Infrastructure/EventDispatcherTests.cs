using System;
using System.Collections;
using System.Collections.Generic;
using Model.Objects;
using Model.Systems;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.Infrastructure.UnitTests
{
    //public class EventDispatcherTests
    //{
    //    [Test]
    //    public void SubscribeOnLevel_DestroyBlock_BlockEventCellEvent()
    //    {
    //        Level level;
    //        EventDispatcher eventDispatcher;
    //        SetupLevel_TwoCellsOneBlock(out level, out eventDispatcher);

    //        int blockDestroyEvent = 0;
    //        int emptyCellEvent = 0;
    //        eventDispatcher.OnAnyBlockDestroyed += (Block sender, EventArgs eventArgs) => { blockDestroyEvent++; };
    //        eventDispatcher.OnAnyCellEmpty += (Cell sender, EventArgs eventArgs) => { emptyCellEvent++; };

    //        level.gameBoard.cells[0, 0].DestroyBlock(); //TODO заменить на соотв. экшн

    //        Assert.AreEqual(1, blockDestroyEvent);
    //        Assert.AreEqual(1, emptyCellEvent);
    //    }

    //    [Test]
    //    public void SubscribeOnLevel_MoveBlock_BlockEventCellEvent()
    //    {
    //        Level level;
    //        EventDispatcher eventDispatcher;
    //        SetupLevel_TwoCellsOneBlock(out level, out eventDispatcher);

    //        int blockMoveEvent = 0;
    //        int emptyCellEvent = 0;
    //        eventDispatcher.OnAnyBlockChangedPosition += (Block sender, EventArgs eventArgs) => { blockMoveEvent++; };
    //        eventDispatcher.OnAnyCellEmpty += (Cell sender, EventArgs eventArgs) => { emptyCellEvent++; };

    //        new SwapBlocksAction(level.gameBoard.cells[0, 0], level.gameBoard.cells[0, 1]).Execute();
    //        //TODO в экшене не меняется положение блока(именно запись в самом блоке)

    //        Assert.AreEqual(1, blockMoveEvent);
    //        Assert.AreEqual(1, emptyCellEvent);
    //    }

    //    [Test]
    //    public void SubscribeOnLevel_ChangeBlockType_BlockEvent()
    //    {
    //        Level level;
    //        EventDispatcher eventDispatcher;
    //        SetupLevel_TwoCellsOneBlock(out level, out eventDispatcher);

    //        int blockChangeTypeEvent = 0;
    //        eventDispatcher.OnAnyBlockChangedType += (Block sender, EventArgs eventArgs) => { blockChangeTypeEvent++; };

    //        new ChangeBlockTypeAction( new RedBlockType(), level.gameBoard.cells[0,0].block).Execute();

    //        Assert.AreEqual(1, blockChangeTypeEvent);
    //    }

    //    [Test]
    //    public void SubscribeOnLevel_ChangeCellType_CellEvent()
    //    {
    //        Level level;
    //        EventDispatcher eventDispatcher;
    //        SetupLevel_TwoCellsOneBlock(out level, out eventDispatcher);

    //        int cellChangeTypeEvent = 0;
    //        eventDispatcher.OnAnyCellChangedType += (Cell sender, EventArgs eventArgs) => { cellChangeTypeEvent++; };

    //        new ChangeCellTypeAction(level.gameBoard.cells[0,1], new NotPlayableCellType()).Execute();

    //        Assert.AreEqual(1, cellChangeTypeEvent);
    //    }

    //    [Test]
    //    public void SubscribeOnLevel_DestroyCell_CellEvent()
    //    {
    //        Level level;
    //        EventDispatcher eventDispatcher;
    //        SetupLevel_TwoCellsOneBlock(out level, out eventDispatcher);

    //        int cellDestroyEvent = 0;
    //        eventDispatcher.OnAnyCellDestroyed += (Cell sender, EventArgs eventArgs) => { cellDestroyEvent++; };

    //        level.gameBoard.cells[0, 1].DestroyCell();

    //        Assert.AreEqual(1, cellDestroyEvent);
    //    }

    //    [Test]
    //    public void SubscribeOnLevel_UpdateGoal_updateGoalEvent()
    //    {
    //        Level level;
    //        EventDispatcher eventDispatcher;
    //        SetupLevel_TwoCellsOneBlock(out level, out eventDispatcher);

    //        int updateGoalEvent = 0;
    //        int completeGoalEvent = 0;
    //        eventDispatcher.OnAnyGoalUpdate += (Counter sender, EventArgs eventArgs) => { updateGoalEvent++; };
    //        eventDispatcher.OnAnyGoalComplete += (Counter sender, EventArgs eventArgs) => { completeGoalEvent++; };

    //        level.goals[0].UpdateGoal(new BasicBlockType());

    //        Assert.AreEqual(1, updateGoalEvent);
    //        Assert.AreEqual(0, completeGoalEvent);
    //    }

    //    [Test]
    //    public void SubscribeOnLevel_CompleteGoal_updateGoalEvent()
    //    {
    //        Level level;
    //        EventDispatcher eventDispatcher;
    //        SetupLevel_TwoCellsOneBlock(out level, out eventDispatcher);

    //        int updateGoalEvent = 0;
    //        int completeGoalEvent = 0;
    //        eventDispatcher.OnAnyGoalUpdate += (Counter sender, EventArgs eventArgs) => { updateGoalEvent++; };
    //        eventDispatcher.OnAnyGoalComplete += (Counter sender, EventArgs eventArgs) => { completeGoalEvent++; };

    //        level.goals[0].UpdateGoal(new BasicBlockType());
    //        level.goals[0].UpdateGoal(new BasicBlockType());

    //        Assert.AreEqual(2, updateGoalEvent);
    //        Assert.AreEqual(1, completeGoalEvent);
    //    }

    //    [Test]
    //    public void SubscribeOnLevel_UpdateRestriction_updateGoalEvent()
    //    {
    //        Level level;
    //        EventDispatcher eventDispatcher;
    //        SetupLevel_TwoCellsOneBlock(out level, out eventDispatcher);

    //        int updateRestrictionEvent = 0;
    //        int completeRestrictionEvent = 0;
    //        eventDispatcher.OnAnyRestrictionUpdate += (Counter sender, EventArgs eventArgs) => { updateRestrictionEvent++; };
    //        eventDispatcher.OnAnyRestrictionComplete += (Counter sender, EventArgs eventArgs) => { completeRestrictionEvent++; };

    //        level.restrictions[0].UpdateGoal(new Turn());

    //        Assert.AreEqual(1, updateRestrictionEvent);
    //        Assert.AreEqual(0, completeRestrictionEvent);
    //    }

    //    [Test]
    //    public void SubscribeOnLevel_CompleteRestriction_updateGoalEvent()
    //    {
    //        Level level;
    //        EventDispatcher eventDispatcher;
    //        SetupLevel_TwoCellsOneBlock(out level, out eventDispatcher);

    //        int updateRestrictionEvent = 0;
    //        int completeRestrictionEvent = 0;
    //        eventDispatcher.OnAnyRestrictionUpdate += (Counter sender, EventArgs eventArgs) => { updateRestrictionEvent++; };
    //        eventDispatcher.OnAnyRestrictionComplete += (Counter sender, EventArgs eventArgs) => { completeRestrictionEvent++; };

    //        level.restrictions[0].UpdateGoal(new Turn());
    //        level.restrictions[0].UpdateGoal(new Turn());

    //        Assert.AreEqual(2, updateRestrictionEvent);
    //        Assert.AreEqual(1, completeRestrictionEvent);
    //    }

    //    private static void SetupLevel_TwoCellsOneBlock(out Level level, out EventDispatcher eventDispatcher)
    //    {
    //        level = new Level(1, 2);
    //        Block block = level.gameBoard.cells[0, 0].SpawnBlock(new BasicBlockType());
    //        level.gameBoard.RegisterBlock(block);
    //        eventDispatcher = new EventDispatcher();
    //        eventDispatcher.SubscribeOnLevel(level);
    //    }
    //}
}