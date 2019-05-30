using NUnit.Framework;
using System.Collections.Generic;
using System;


namespace EvaluatorTests
{
    [TestFixture()]
    public class Evaluator : Generic.TestsGeneric
    {

        [Test()]
        public void TestSimplePostfix()
        {
            string expression = "1 2 +";
            Queue<string> input = CreateExpectedOuput(expression);
            float answer = alg.Evaluator(input);
            Assert.AreEqual(3.0, answer);
        }

        [Test]
        public void TestSimplePostfix2()
        {
            string expression = "1 3 + 5 8 + *";
            Queue<string> input = CreateExpectedOuput(expression);
            float answer = alg.Evaluator(input);
            Assert.AreEqual(52.0, answer);
        }


    }
}
