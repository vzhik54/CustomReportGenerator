using NUnit.Framework;
using System.IO;
using System;
using SumProject;

namespace SumTests
{
    public class ProgramTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckSum()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                Program.Main();

                var result = int.Parse(sw.ToString());
                Assert.AreEqual(7, result);
            }
        }

        [Test]
        public void SumGreaterThenFive()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                Program.Main();

                var result = int.Parse(sw.ToString());
                Assert.Greater(result, 5);
            }
        }
    }
}