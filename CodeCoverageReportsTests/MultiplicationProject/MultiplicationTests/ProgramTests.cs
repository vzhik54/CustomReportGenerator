using NUnit.Framework;
using System.IO;
using System;
using MultiplicationProject;

namespace MultiplicationTests
{
    public class ProgramTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void IsItGood()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                Program.Main();

                var result = sw.ToString();
                Assert.AreEqual("Good\r\n", result);
            }
        }
    }
}