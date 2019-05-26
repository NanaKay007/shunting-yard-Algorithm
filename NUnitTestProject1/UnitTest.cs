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
        //tests only tokenize
    
            //Ints
        [Test]
        public void TestIntsOnly()
        {
            string expression = "344455590";
            List<string> actual = alg.Tokenize(expression);
            List<string> expected = new List<string>{ "344455590" };
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void TestSymbolsOnly()
        {
            string expression = "+-";
            List<string> actual = alg.Tokenize(expression);
            List<string> expected = new List<string>() { "-" };
            //to do assert
            Assert.AreEqual(expected, actual);
            
        }

        [Test]
        public void TestIntsAndSymbolsIncorrectSyntax1()
        {
            string expression = "123+/45";
            List<string> actual = alg.Tokenize(expression);
            List<string> expected = new List<string>() { "123", "/","45" };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestIntsAndSymbolsIncorrectSyntax2()
        {
            string expression = "156*+45";
            List<string> actual = alg.Tokenize(expression);
            List<string> expected = new List<string>() { "156", "+", "45" };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestIntsAndSymbolsIncorrectSyntax3()
        {
            string expression = "345/-34";
            List<string> actual = alg.Tokenize(expression);
            List<string> expected = new List<string>() { "345", "-", "34" };
            Assert.AreEqual(expected, actual);
        }


        //unary symbols +-
        [Test]
        public void TestNumbersAndSymbolsWeirdButCorrectSyntax1()
        {
            string expression = "+12+-553";
            List<string> result = alg.Tokenize(expression);
            List<string> expected = new List<string>() { "+", "12", "-", "553" };
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestNumbersAndSymbolsWeirdButCorrectSyntax2()
        {
            string expression = "-45/33";
            List<string> result = alg.Tokenize(expression);
            List<string> expected = new List<string>() { "-", "45", "/", "33" };
            Assert.AreEqual(expected, result);
        }

        //Floats
        [Test]
        public void TestFloatsOnly()
        {
            string expression = "123.44";
            List<string> result = alg.Tokenize(expression);
            List<string> expected = new List<string>() { "123.44" };
            Assert.AreEqual(expected, result,result.ToString());
        }

        [Test]
        public void TestFloatsAndSymbols()
        {
            string expression = "12.33+874/90";
            List<string> result = alg.Tokenize(expression);
            List<string> actual = new List<string>() { "12.33", "+", "874", "/", "90" };
            Assert.AreEqual(actual, result,result.ToString());
        }

        [Test]
        public void TestFloatsAndSymbolsIncorrect1()
        {
            string expression = "123.56+/45.02";
            //to do
            List<string> result = alg.Tokenize(expression);
            List<string> actual = new List<string>() { "123.56", "/", "45.02" };
            Assert.AreEqual(actual, result,result.ToString());
        }

        [Test]
        public void TestFloatsAndSymbolsIncorrect2()
        {
            string expression = "156.99*+45.00";
            List<string> result = alg.Tokenize(expression);
            List<string> actual = new List<string>() { "156.99", "+", "45.00" };
            Assert.AreEqual(result, actual,result.ToString());
        }

        //Test both floats and ints
        [Test]
        public void TestFloatIntSymbolValid()
        {
            string expression = "12.3/4";
            List<string> result = alg.Tokenize(expression);
            List<string> actual = new List<string>() { "12.3", "/", "4" };
            Assert.AreEqual(actual, result,result.ToString());
        }

        [Test]
        public void TestFloatIntSymbolWeirdButValid()
        {
            string expression = "12.3+-5";
            List<string> result = alg.Tokenize(expression);
            List<string> actual = new List<string>() { "12.3", "+", "-","5" };
            Assert.AreEqual(actual, result,result.ToString());
        }


        
        //tests both shunting-yard algorithm and tokenize
        //[Test]
        //public void TestSimpleConvert()
        //{
        //    string expression = "1+2";
        //    Queue<string> result = alg.ShuntingYardAlgorithm(expression);
        //    //Queue<string> expected = new Queue<string>() { "1", "2", "+" };
        //    throw new System.Exception("not implemented");

        //}

        //[Test]
        //public void TestFloatInput()
        //{

        //    string expression = "1.342+4.5";
        //    Queue<string> result = alg.ShuntingYardAlgorithm(expression);
        //    throw new System.Exception("not implemented");


        //}


    }
}