﻿using System;
using System.Linq;
using NUnit.Framework;

namespace Scheduler.UnitTests
{   
    [TestFixture()]
    public class Scheduler_TestInput
    {
        public const string SINGLE_ELEMENT_TEST_FILE = "single_element_test.json";
        public const string MULTI_ELEMENT_TEST_FILE = "multi_element_test.json";

        [SetUp()]
        public void Init()
        { }

        [TearDown()]
        public void Cleanup()
        { }

        [Test]
        public void ReadSingleInputToArray()
        {
            var count = Scheduler.ReadInput(SINGLE_ELEMENT_TEST_FILE).Count();
            Assert.AreEqual(count, 1);
        }

        [Test]
        public void ReadMultiInputToArray()
        {
            var count = Scheduler.ReadInput(MULTI_ELEMENT_TEST_FILE).Count();
            Assert.AreEqual(count, 2);
        }

        [Test]
        public void SingleInputProject()
        {
            Scheduler.Project testProject = new Scheduler.Project(
                new DateTime(2015, 9, 1), new DateTime(2015, 9, 3), 
                Scheduler.CityType.LowCost);
            var project = Scheduler.ReadInput(SINGLE_ELEMENT_TEST_FILE).First();
            Assert.AreEqual(testProject, project)
        }

        [Test]
        public void MultiInputProject()
        {
            Scheduler.Project testProject = new Scheduler.Project(
                new DateTime(2015, 9, 2), new DateTime(2015, 9, 6),
                Scheduler.CityType.LowCost);
            var project = Scheduler.ReadInput(SINGLE_ELEMENT_TEST_FILE).ElementAt(1);
            Assert.AreEqual(testProject, project)
        }
    }
}
