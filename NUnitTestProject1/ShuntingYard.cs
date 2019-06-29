using NUnit.Framework;
using System.Collections.Generic;
using System;
using TokenizeTests;

namespace shuntingYardTests
{
    [TestFixture()]
    public class Tokenize : Generic.TestsGeneric
    {
       

        [Test]
        public void TestSimpleConvertIntInput()
        {
            string expression = "1+2";
            string output = "1 2 +";
            Queue<string> actual = alg.ShuntingYardAlgorithm(expression);
            Queue<string> expected = CreateExpectedOuput(output);
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void TestSimpleConvertFloatInput()
        {

            string expression = "1.342+4.5";
            string output = "1.342 4.5 +";
            Queue<string> actual = CreateExpectedOuput(output);
            Queue<string> result = alg.ShuntingYardAlgorithm(expression);
            Assert.AreEqual(actual, result);

        }

        [Test]
        public void TestSimpleConvertIntInput2()
        {
            string expression = "(1+3)*(5+8)";
            string output = "1 3 + 5 8 + *";
            Queue<string> actual = CreateExpectedOuput(output);
            Queue<string> result = alg.ShuntingYardAlgorithm(expression);
            Assert.AreEqual(result, actual);
        }

        [Test]
        public void TestComplexIntInput()
        {
            string expression = "1+2*3+(5*3+5)*8";
            string output = "1 2 3 * + 5 3 * 5 + 8 * +";
            Queue<string> expected = CreateExpectedOuput(output);
            Queue<string> result = alg.ShuntingYardAlgorithm(expression);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestUnaryInput()
        {
            string expression = "-3";
            string output = "3 -";
            Queue<string> expected = CreateExpectedOuput(output);
            Queue<string> result = alg.ShuntingYardAlgorithm(expression);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestExponent()
        {
            string expression = "(-3^3)^-3";
            string output = "-3 3 ^ -3 ^";
            Queue<string> result = alg.ShuntingYardAlgorithm(expression);
            Queue<string> expected = CreateExpectedOuput(output);
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void TestSimpleMax()
        {
            string expression = "max(2,3)";
            string output = "2 3 max";
            Queue<string> result = alg.ShuntingYardAlgorithm(expression);
            Queue<string> expected = CreateExpectedOuput(output);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestSimpleMin()
        {
            string expression = "min((489233*883999),0)";
            string output = "489233 883999 * 0 min";
            Queue<string> result = alg.ShuntingYardAlgorithm(expression);
            Queue<string> expected = CreateExpectedOuput(output);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestImplicitBraces()
        {
            string expression = "min(489233*883999/10,0)";
            string output = "489233 883999 * 10 / 0 min";
            Queue<string> result = alg.ShuntingYardAlgorithm(expression);
            Queue<string> expected = CreateExpectedOuput(output);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestImplicitBraces2()
        {
            string expression = "min((489233*883999)/10,0)";
            string output = "489233 883999 * 10 / 0 min";
            Queue<string> result = alg.ShuntingYardAlgorithm(expression);
            Queue<string> expected = CreateExpectedOuput(output);
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void TestImplicitBraces3()
        {
            string expression = "min((489233*883999)/10,(120*4)*0)";
            string output = "489233 883999 * 10 / 120 4 * 0 * min";
            Queue<string> result = alg.ShuntingYardAlgorithm(expression);
            Queue<string> expected = CreateExpectedOuput(output);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestImplicitBraces4()
        {
            string expression = "min((489233*883999)/10,120+4*0)";
            string output = "489233 883999 * 10 / 120 4 0 * + min";
            Queue<string> result = alg.ShuntingYardAlgorithm(expression);
            Queue<string> expected = CreateExpectedOuput(output);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestComplexFunctions()
        {
            string expression = "sin ( max ( 2, 3 ) / 3 * 10 )";
            string output = "2 3 max 3 / 10 * sin";
            Queue<string> result = alg.ShuntingYardAlgorithm(expression);
            Queue<string> expected = CreateExpectedOuput(output);
            Assert.AreEqual(expected, result);

        }

        [Test]
        public void TestLogBase()
        {
            string expression = "logB[6](50)";
            string output = "50 logB[6]";
            Queue<string> result = alg.ShuntingYardAlgorithm(expression);
            Queue<string> expected = CreateExpectedOuput(output);
            Assert.AreEqual(expected, result);
        }

        
    }
}
