using NUnit.Framework;
using System.Collections.Generic;


namespace TokenizeTests
{
    public class Tests : Generic.TestsGeneric
    {
       
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
            List<string> expected = new List<string>() { "156", "*", "45" };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestIntsAndSymbolsIncorrectSyntax3()
        {
            string expression = "345/-34";
            List<string> actual = alg.Tokenize(expression);
            List<string> expected = new List<string>() { "345","/", "-34" };
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
            List<string> actual = new List<string>() { "156.99", "*", "45.00" };
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
            List<string> actual = new List<string>() { "12.3", "-","5" };
            Assert.AreEqual(actual, result,result.ToString());
        }

        //Tests with parentheses
        [Test]
        public void TestParenthesis()
        {
            string expression = "1+2*3+(5*3+5)*8";
            List<string> actual = alg.Tokenize(expression);
            List<string> expected = new List<string>() { "1", "+", "2", "*","3", "+", "(", "5", "*", "3", "+", "5", ")", "*", "8" };
            Assert.AreEqual(expected, actual,actual.ToString());

        }

        [Test]
        public void TestParenthesis2()
        {
            string expression = "(1+3)*(5+8)";
            List<string> actual = alg.Tokenize(expression);
            List<string> expected = new List<string>() { "(", "1", "+", "3", ")", "*", "(","5", "+", "8", ")" };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestExponents()
        {
            string expression = "3^5^8";
            List<string> actual = alg.Tokenize(expression);
            List<string> expected = new List<string>() { "3", "^", "5", "^", "8" };
            Assert.AreEqual(actual, expected);
        }
        //tests both shunting-yard algorithm and tokenize
        [Test]
        public void TestExponentWithOther()
        {
            string expression = "3^-7";
            List<string> actual = alg.Tokenize(expression);
            List<string> expected = new List<string>() { "3", "^", "-7" };
            Assert.AreEqual(actual, expected);
        }

        [Test]
        public void TestExponentWithOther2()
        {
            string expression = "3-^7";
            List<string> actual = alg.Tokenize(expression);
            List<string> expected = new List<string>() { "3", "^", "7" };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestExponentWithNegativeOther()
        {
            string expression = "(-3^3)^-3";
            List<string> actual = alg.Tokenize(expression);
            List<string> expected = new List<string>() { "(", "-3", "^", "3", ")", "^", "-3" };
            Assert.AreEqual(expected, actual);
        }

        //Logarithm (base 10) tests
        [Test]
        public void TestLogarithm1()
        {
            string expression = "3*log(90)";
            List<string> actual = alg.Tokenize(expression);
            List<string> expected = new List<string>() { "3", "*", "log", "(", "90", ")" };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ComplexLogarithm()
        {
            string expression = "3*log((84+5*5)^10)";
            List<string> actual = alg.Tokenize(expression);
            List<string> expected = new List<string>() { "3", "*", "log", "(", "(", "84", "+", "5", "*", "5", ")", "^", "10", ")" };
            Assert.AreEqual(expected, actual);
        }

        //Logarithm base y tests
        [Test]
        public void TestLogarithmWithBase()
        {
            string expression = "logB[6](50)";
            List<string> actual = alg.Tokenize(expression);
            List<string> expected = new List<string>() { "logB[6]", "(", "50", ")" };
            Assert.AreEqual(expected, actual);
        }


        //Max
        [Test]
        public void TestMax()
        {
            string expression = "max(345,893)";
            List<string> actual = alg.Tokenize(expression);
            List<string> expected = new List<string>() { "max", "(", "345",",", "893", ")" };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestComplexMax()
        {
            string expression = "max((4*5-384),(56^738))";
            List<string> actual = alg.Tokenize(expression);
            List<string> expected = new List<string>() { "max", "(", "(", "4", "*","5", "-", "384", ")", ",", "(", "56", "^", "738", ")", ")" };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestComplexMin()
        {
            string expression = "min(489233*883999389393,0)";
            List<string> actual = alg.Tokenize(expression);
            List<string> expected = new List<string> { "min", "(", "489233", "*", "883999389393", ",", "0" ,")"};
            Assert.AreEqual(expected, actual);
        }
        //ln
        [Test]
        public void SimpleNaturalLog()
        {
            string expression = "ln(76)";
            List<string> actual = alg.Tokenize(expression);
            List<string> expected = new List<string>() { "ln", "(", "76", ")" };
            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void ComplexNaturalLog()
        {
            string expression = "ln(max(54,89*40))";
            List<string> actual = alg.Tokenize(expression);
            List<string> expected = new List<string>() { "ln", "(", "max", "(", "54", ",", "89", "*", "40", ")", ")" };
            Assert.AreEqual(expected, actual);
        }
    }
}