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
            Project testProject = new Project();
            testProject.Name = "Project1";
            testProject.StartDate = new DateTime(2015, 9, 1);
            testProject.EndDate = new DateTime(2015, 9, 3);
            testProject.City = CityType.LowCost;
            var project = Scheduler.ReadInput(SINGLE_ELEMENT_TEST_FILE).First();
            Assert.AreEqual(JsonConvert.SerializeObject(testProject),
                JsonConvert.SerializeObject(project));
        }

        [Test]
        public void MultiInputProject()
        {
            Project testProject = new Project();
            testProject.Name = "Project2";
            testProject.StartDate = new DateTime(2015, 9, 2);
            testProject.EndDate = new DateTime(2015, 9, 6);
            testProject.City = CityType.LowCost;
            var project = Scheduler.ReadInput(MULTI_ELEMENT_TEST_FILE).ElementAt(1);
            Assert.AreEqual(JsonConvert.SerializeObject(testProject),
                JsonConvert.SerializeObject(project));
        }
    }
}
