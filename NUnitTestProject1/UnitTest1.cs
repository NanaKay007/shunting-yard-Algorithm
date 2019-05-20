using NUnit.Framework;
using System.Collections.Generic;
using shuntingYard;

namespace Tests
{
    public class Tests
    {
        Program alg;

        [SetUp]
        public void Setup()
        {
            alg = new Program();
        }

        public static List<char> CreateInput(string expression)
        {
            List<char> input = new List<char>();
            foreach(char x in expression)
            {
                input.Add(x);
            }
            
            return input;
        }

        [Test]
        public void Test1()
        {
            string expression = "1+2";
            Setup();
            List<char> input = CreateInput(expression);
            Queue<string> ouput = alg.shuntingYardAlgorithm(input);
            List<string> expected = new List<string>() { "1","2","+"};

            Assert.AreEqual(expected, ouput);
            Assert.Pass();
        }

        [Test]
        public void Test2()
        {

        }
    }
}