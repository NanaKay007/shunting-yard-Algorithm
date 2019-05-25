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
    
            //Ints
        [Test]
        public void TestIntsOnly()
        {
            string expression = "344455590";
        }

        [Test]
        public void TestSymbolsOnly()
        {
            string expression = "+-";
        }

        [Test]
        public void TestIntsAndSymbolsIncorrectSyntax1()
        {
            string expression = "123+/45";
        }

        [Test]
        public void TestIntsAndSymbolsIncorrectSyntax2()
        {
            string expression = "156*+45";
        }

        [Test]
        public void TestIntsAndSymbolsIncorrectSyntax3()
        {
            string expression = "345/-34";
        }


        //unary symbols +-
        [Test]
        public void TestNumbersAndSymbolsWeirdButCorrectSyntax1()
        {
            string expression = "+12+-553";
        }

        [Test]
        public void TestNumbersAndSymbolsWeirdButCorrectSyntax2()
        {
            string expression = "-45/33";
        }

        //Floats
        [Test]
        public void TestFloatsOnly()
        {
            string expression = "123.44";
        }

        [Test]
        public void TestFloatsAndSymbols()
        {
            string expression = "12.33+874/90";

        }

        [Test]
        public void TestFloatsAndSymbolsIncorrect1()
        {
            string expression = "123.56+/45.02";
        }

        [Test]
        public void TestFloatsAndSymbolsIncorrect2()
        {
            string expression = "156.99*+45.00";
        }

        //Test both floats and ints
        [Test]
        public void TestFloatIntSymbolValid()
        {
            string expression = "12.3/4";
        }

        [Test]
        public void TestFloatIntSymbolWeirdButValid()
        {
            string expression = "12.3+-5";
        }


        
        //tests both shunting-yard algorithm and tokenize
        [Test]
        public void TestSimpleConvert()
        {
            string expression = "1+2";
           

           
        }

        [Test]
        public void TestFloatInput()
        {

            string expression = "1.342+4.5";
           

      
        }


    }
}