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
    }
}
