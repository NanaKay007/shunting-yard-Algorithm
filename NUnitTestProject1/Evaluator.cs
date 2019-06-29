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

        [Test]
        public void TestUnaryPostfix()
        {
            string expression = "3 -";
            Queue<string> input = CreateExpectedOuput(expression);
            float answer = alg.Evaluator(input);
            Assert.AreEqual(-3, answer);
        }

        [Test]
        public void TestUnaryPostfix2()
        {
            string expression = "3 +";
            Queue<string> input = CreateExpectedOuput(expression);
            float answer = alg.Evaluator(input);
            Assert.AreEqual(3, answer);
        }

        [Test]
        //divide by zero
        public void DivideByZero()
        {
            string expression = "3 0 /";
            Queue<string> input = CreateExpectedOuput(expression);
            float answer = alg.Evaluator(input);
            Assert.AreEqual(double.PositiveInfinity, answer);
        }

        [Test]
        public void TestExponent()
        {
            string expression = "3 3 3 ^ ^";
            Queue<string> input = CreateExpectedOuput(expression);
            float answer = alg.Evaluator(input);
            Assert.AreEqual(7.62559752E+12f, answer);
        }

       //max
       [Test]
       public void TestSimpleMax()
        {
            string expression = "2 3 max";
            Queue<string> input = CreateExpectedOuput(expression);
            float answer = alg.Evaluator(input);
            Assert.AreEqual(3, answer);
        }
        //Min
        [Test]
        public void TestSimpleMin()
        {
            string expression = "489233 883999 * -1 min";
            Queue<string> input = CreateExpectedOuput(expression);
            float answer = alg.Evaluator(input);
            Assert.AreEqual(-1, answer);
        }
        //log base

        [Test]
        public void TestSimpleLogBase()
        {
            string expression = "4 logB[2]";
            Queue<string> input = CreateExpectedOuput(expression);
            float answer = alg.Evaluator(input);
            Assert.AreEqual(2, answer);
        }

        //ln
        [Test]
        public void TestNaturalLog()
        {
            string expression = "2.71828 ln";
            Queue<string> input = CreateExpectedOuput(expression);
            float answer = alg.Evaluator(input);
            Assert.AreEqual(0.999999344f, answer);
        }

        //sin
        [Test]
        public void TestSin()
        {
            string expression = "180 + sin";
            Queue<string> input = CreateExpectedOuput(expression);
            float answer = alg.Evaluator(input);
            Assert.AreEqual(0, answer);
            
        }
    }
}
