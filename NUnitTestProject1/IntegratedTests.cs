using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace IntegratedTest
{
    [TestFixture]
    public class Tests : Generic.TestsGeneric
    {
        [Test]
        public void TestExponentWithBrackets()
        {
            string expression = "(3^3)^3";
            float answer = alg.Solve(expression);
            Assert.AreEqual(19683, answer);
        }

        [Test]
        public void TestNegativeExponentWithBrackets()
        {
            string expression = "(3^3)^-3";
            float answer = alg.Solve(expression);
            Assert.AreEqual(5.08052617E-05f, answer);
        }

        [Test]
        public void TestNegativeAndNegative()
        {
            string expression = "-2+3^-3";
            
            float answer = alg.Solve(expression);
            Assert.AreEqual(-1.96296296296f, answer);
        }

        [Test]
        public void TestRoots()
        {
            string expression = "27^(1/3)";
            float answer = alg.Solve(expression);
            Assert.AreEqual(3, answer);
        }

    }
}
