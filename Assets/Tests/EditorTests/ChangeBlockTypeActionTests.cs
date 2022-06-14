using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Model.Objects;

namespace Model.Systems.Tests
{
    public class ChangeBlockTypeActionTests
    {
        [Test]
        public void ChangeType_NullToRed_Null()
        {
            Level level = new Level();
            IAction action = new ChangeBlockTypeAction(level, new RedBlockType(), new Vector2Int(0,0));

            action.Execute();

            Assert.AreEqual(null, level.gameBoard.cells[0,0].block.type);
        }

    }
}