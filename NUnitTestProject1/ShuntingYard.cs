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


        //log
        [Test]
        public void TestComplexFunctions()
        {
            string expression = "sin ( max ( 2, 3 ) ÷ 3 * 10 )";
            string output = "2 3 max 3 ÷ 10 * sin";
            Queue<string> result = alg.ShuntingYardAlgorithm(expression);
            Queue<string> expected = CreateExpectedOuput(output);
            Assert.AreEqual(expected, result);

        }
    }
}
