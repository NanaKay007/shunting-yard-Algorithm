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
        //tests only tokenize


        //tests both shunting-yard algorithm and tokenize
        [Test]
        public void TestSimpleConvert()
        {
            string expression = "1+2";
            List<char> input = CreateInput(expression);
            Queue<string> ouput = alg.shuntingYardAlgorithm(input);
            List<string> expected = new List<string>() { "1","2","+"};

            Assert.AreEqual(expected, ouput);
           
        }

        [Test]
        public void TestFloatInput()
        {

            string expression = "1.342+4.5";
            List<char> input = CreateInput(expression);
            Queue<string> output = alg.shuntingYardAlgorithm(input);
            List<string> expected = new List<string>() { "1.342","4.5","+" };
            Assert.AreEqual(expected, output);
      
        }


    }
}