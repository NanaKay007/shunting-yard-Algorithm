using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shuntingYard
{
    public class Operator
    {
        private int precedence, associativity;
        private string name;
        

        public Operator(char symbol)
        {
            name = symbol.ToString();

            switch (symbol)
            {
                
                case ('*'):
                    associativity = 1;
                    precedence = 9;
                    break;
                case ('/'):
                    associativity = 1;
                    precedence = 9;
                    break;
                case ('+'):
                    associativity = 1;
                    precedence = 8;
                    break;
                case ('-'):
                    associativity = 1;
                    precedence = 8;
                    break;
                case ('^'):
                    associativity = -1;
                    precedence = 12;
                    break;
                case ('('):
                    
                    precedence = 14;
                    break;
                default:
                    throw new Exception("invalid operator") ;
            }
        }

        public string getName() => name;

        public int getPrecedence()
        {
            return precedence;
        }

        public static bool operator >(Operator self,Operator other)
        {
            return self.getPrecedence() > other.getPrecedence();
        }

        public static bool operator < (Operator self, Operator other)
        {
            return self.precedence < other.getPrecedence();
        }

        public static bool operator ==(Operator self, Operator other)
        {
            return self.precedence == other.getPrecedence();
        }

        public static bool operator !=(Operator self, Operator other)
        {
            return self.precedence == other.getPrecedence();
        }
        public int getAssociativity()
        {
            return associativity;
        }
        public override string ToString()
        {
            return name;
        }
    }


    public class Program
    {

        public Queue<string> ShuntingYardAlgorithm(List<char> var)
        {
            
            Queue<string> Ouput = new Queue<string>();
            Stack<Operator> Operators = new Stack<Operator>();
            foreach (char x in var)
            {
                int number;


                if (int.TryParse(x.ToString(), out number))
                {
                    Ouput.Enqueue(number.ToString());
                }
                else
                {
                    try
                    {
                        Operator token = new Operator(x);
                        if (Operators.Count != 0)
                        {
                            while ((Operators.Peek() > token) && Operators.Peek().getName() != "(" || (Operators.Peek() == token) && Operators.Peek().getAssociativity() == 1)
                            {
                                Operator current = Operators.Pop();
                                Ouput.Enqueue(current.ToString());
                            }
                        }
                        Operators.Push(token);

                    }
                    catch (Exception)
                    {
                        
                        if (x == ')')
                        {
                            while (Operators.Count != 0 && Operators.Peek().getName() != "(")
                            {
                                Operator current = Operators.Pop();
                                Ouput.Enqueue(current.ToString());
                            }
                            if (Operators.Count != 0 && Operators.Peek().getName() != "(")
                            {
                                Operators.Pop();
                            }
                        }
                    }


                }

            }

            while (Operators.Count != 0)
            {
                Operator @operator = Operators.Pop();
                if (@operator.getName() != "(")
                {
                    Ouput.Enqueue(@operator.ToString());
                }

            }


          
            return Ouput;


        }

        public List<char> tokenize(string expression)
        {
            //purpose: separates an expression into tokens; numbers and operators
            //params: a string expression
            //return: a list of tokens; throws an error if an invalid token is encountered in the expression
            List<char> tokens = new List<char>();

            Regex rx = new Regex(@"(([+*/-])?(\d+[\.(\d)+]?)?([+*/-])(\d+))");

            MatchCollection matches = rx.Matches(expression);

            foreach (Match match in matches)
            {
                GroupCollection groups = match.Groups;
                Console.WriteLine("'{0}' repeated at positions {1} and {2}",
                                  groups["word"].Value,
                                  groups[0].Index,
                                  groups[1].Index);
            }


            return tokens;
        }

        static void Main(string[] args)
        {
            string var = @"1+3*3";
            Program alg = new Program();
            List<char> output = alg.tokenize(var);

            Console.ReadKey();
        }
    }

}
