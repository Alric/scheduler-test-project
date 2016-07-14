using System;
using System.Linq;
using NUnit.Framework;
using Newtonsoft.Json;

namespace Scheduler.UnitTests
{   
    [TestFixture()]
    public class Scheduler_TestComputation
    {
        private const string SINGLE_ELEMENT_TEST_FILE = "single_element_test.json";
        private const string SEPARATED_ELEMENT_TEST_FILE = "separated_element_test.json";
        private const string OVERLAP_ELEMENT_TEST_FILE = "overlap_element_test.json";

        [SetUp()]
        public void Init()
        {
        }

        [TearDown()]
        public void Cleanup()
        { }

        [Test]
        public void SingleProjectComputation()
        {
            System.Diagnostics.Trace.WriteLine("Single!!!!!");
            var cost = Scheduler.ComputeCost(SINGLE_ELEMENT_TEST_FILE);
            Assert.AreEqual(165, cost);
        }

        [Test]
        public void SeparateProjectComputation()
        {
            System.Diagnostics.Trace.WriteLine("Separate!!!!!");
            var cost = Scheduler.ComputeCost(SEPARATED_ELEMENT_TEST_FILE);
            Assert.AreEqual(530, cost);
        }

        [Test]
        public void OverlapProjectComputation()
        {
            System.Diagnostics.Trace.WriteLine("Overlap!!!!!");
            var cost = Scheduler.ComputeCost(OVERLAP_ELEMENT_TEST_FILE);
            Assert.AreEqual(655, cost);
        }
        }
}
