using System.Collections;
using System.Collections.Generic;
using Model.Objects;
using NUnit.Framework;
using UnitTests;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.Systems.UnitTests
{
    public class AllSystemTests
    {
        class TestSystem : ISystem
        {
            public Level level;
            public int id;
            public void SetLevel(Level _level)
            {
                level = _level;
            }
        }

        class TestSystem2 : ISystem
        {
            public Level level;
            public int id;
            public void SetLevel(Level _level)
            {
                level = _level;
            }
        }

        [Test]
        public void AddSystem_NewSystem_SystemAdded()
        {
            var allSystems = new AllSystems();
            var system = new TestSystem();

            allSystems.AddSystem(system);

            Assert.AreEqual(system, allSystems.GetSystem<TestSystem>());
        }

        [Test]
        public void AddSystem_ExistingSystem_SystemChanged()
        {
            var allSystems = new AllSystems();
            var systemA = new TestSystem();
            systemA.id = 0;
            var systemB = new TestSystem();
            systemB.id = 1;
            allSystems.AddSystem(systemA);

            allSystems.AddSystem(systemB);

            Assert.AreEqual(1, allSystems.GetSystem<TestSystem>().id);
        }

        [Test]
        public void GetSystem_ExistingSystem_ValidSystem()
        {
            var system = new TestSystem();
            var allSystems = new AllSystems();
            allSystems.AddSystem(system);

            var systemGet = allSystems.GetSystem<TestSystem>();

            Assert.AreEqual(system, systemGet);
        }

        [Test]
        public void GetSystem_NonExistingSystem_Error()
        {
            var allSystems = new AllSystems();

            var systemGet = allSystems.GetSystem<TestSystem>();

            LogAssert.Expect(LogType.Error, "There is no " + typeof(TestSystem) + " registered");
        }

        [Test]
        public void SetLevel_NewLevel_LevelSet()
        {
            var allSystems = new AllSystems();
            var systemA = new TestSystem();
            var systemB = new TestSystem2();
            allSystems.AddSystem(systemA);
            allSystems.AddSystem(systemB);
            Level level = TestUtils.CreateLevel(1, 1);

            allSystems.SetLevel(level);

            Assert.AreEqual(level, allSystems.GetSystem<TestSystem>().level);
            Assert.AreEqual(level, allSystems.GetSystem<TestSystem2>().level);
        }
    }
}