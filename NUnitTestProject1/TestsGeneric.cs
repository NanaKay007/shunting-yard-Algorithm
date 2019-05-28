using NUnit.Framework;
using System;
using shuntingYard;
using System.Collections.Generic;

namespace Generic
{
    [TestFixture()]
    public class TestsGeneric
    {
        public Program alg;

        [SetUp]
        public void Setup()
        {

            alg = new Program();
        }

        public Queue<string> CreateExpectedOuput(string ouput)
        {
            Queue<string> expected = new Queue<string>() { };
            string[] tokens = ouput.Split(' ');
            foreach (string token in tokens)
            {
                expected.Enqueue(token);
            }
            return expected;
        }
    }
}
