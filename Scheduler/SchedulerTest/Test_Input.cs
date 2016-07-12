using System;
using System.Linq;
using NUnit.Framework;
using Newtonsoft.Json;

namespace Scheduler.UnitTests
{   
    [TestFixture()]
    public class Scheduler_TestInput
    {
        private const string SINGLE_ELEMENT_TEST_FILE = "single_element_test.json";
        private const string MULTI_ELEMENT_TEST_FILE = "multi_element_test.json";

        [SetUp()]
        public void Init()
        {
        }

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
            Project testProject = new Project("Project1",
                new DateTime(2015, 9, 1), new DateTime(2015, 9, 3),
                CityType.LowCost);
            var project = Scheduler.ReadInput(SINGLE_ELEMENT_TEST_FILE).First();
            Assert.AreEqual(JsonConvert.SerializeObject(testProject),
                JsonConvert.SerializeObject(project));
        }

        [Test]
        public void MultiInputProject()
        {
            Project testProject = new Project("Project2",
                new DateTime(2015, 9, 2), new DateTime(2015, 9, 6),
                CityType.LowCost);
            var project = Scheduler.ReadInput(MULTI_ELEMENT_TEST_FILE).ElementAt(1);
            Assert.AreEqual(JsonConvert.SerializeObject(testProject),
                JsonConvert.SerializeObject(project));
        }
    }
}
