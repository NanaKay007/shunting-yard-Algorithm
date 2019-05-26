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
        

        public Operator(string symbol)
        {
            name = symbol;

            switch (symbol)
            {
                
                case "*":
                    associativity = 1;
                    precedence = 9;
                    break;
                case "/":
                    associativity = 1;
                    precedence = 9;
                    break;
                case "+":
                    associativity = 1;
                    precedence = 8;
                    break;
                case "-":
                    associativity = 1;
                    precedence = 8;
                    break;
                case "^":
                    associativity = -1;
                    precedence = 12;
                    break;
                case "(":
                    precedence = 14;
                    break;
                default:
                    throw new Exception("invalid operator") ;
            }
        }

        public string GetName() => name;

        public int GetPrecedence()
        {
            return precedence;
        }

        public static bool operator >(Operator self,Operator other)
        {
            return self.GetPrecedence() > other.GetPrecedence();
        }

        public static bool operator < (Operator self, Operator other)
        {
            return self.precedence < other.GetPrecedence();
        }

        public static bool operator ==(Operator self, Operator other)
        {
            return self.precedence == other.GetPrecedence();
        }

        public static bool operator !=(Operator self, Operator other)
        {
            return self.precedence == other.GetPrecedence();
        }
        public int GetAssociativity()
        {
            return associativity;
        }
        public override string ToString()
        {
            return name;
        }

        //public override bool Equals(Operator obj)
        //{
        //    if (obj == null)
        //        return false;
        //    if (this.GetType() != obj.GetType())
        //        return false;
        //    return true;
        //}
    }


    public class Program
    {

        public Queue<string> ShuntingYardAlgorithm(string var)
        {
            
            Queue<string> Ouput = new Queue<string>();
            Stack<Operator> Operators = new Stack<Operator>();

            List<string> tokens = Tokenize(var);

            foreach (string x in tokens)
            {


                if (int.TryParse(x.ToString(), out int number))
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
                            while ((Operators.Peek() > token) && Operators.Peek().GetName() != "(" || (Operators.Peek() == token) && Operators.Peek().GetAssociativity() == 1)
                            {
                                Operator current = Operators.Pop();
                                Ouput.Enqueue(current.ToString());
                            }
                        }
                        Operators.Push(token);

                    }
                    catch (Exception)
                    {

                        if (x == ")")
                        {
                            while (Operators.Count != 0 && Operators.Peek().GetName() != "(")
                            {
                                Operator current = Operators.Pop();
                                Ouput.Enqueue(current.ToString());
                            }
                            if (Operators.Count != 0 && Operators.Peek().GetName() != "(")
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
                if (@operator.GetName() != "(")
                {
                    Ouput.Enqueue(@operator.ToString());
                }

            }


          
            return Ouput;


        }


        public List<string> Tokenize(string expression)
        {
            //purpose: separates an expression into tokens; numbers and operators
            //params: a string expression
            //return: a list of tokens; throws an error if an invalid token is encountered in the expression

           
            Regex numberRegex = new Regex(@"\d+\.?\d*");
            Regex operatorRegex = new Regex(@"[\+\-\/\*\)\(]");


            MatchCollection numberMatches = numberRegex.Matches(expression);
            MatchCollection operatorMatches = operatorRegex.Matches(expression);

            string[] operatorsArray = { "+", "-", "/", "*" };

            int size = numberMatches.Count + operatorMatches.Count;

            List<string> tokens = new List<string>();

            Queue<Match> numbers = new Queue<Match>();
            Queue<Match> operators = new Queue<Match>();

            foreach(Match number in numberMatches)
            {
                numbers.Enqueue(number);
            }

            foreach(Match oper in operatorMatches)
            {
                operators.Enqueue(oper);
            }

            for(int i = 0; i < size; i++)
            {
                Match number;
                Match oper;

                _ = numbers.Count != 0 ? number = numbers.Peek() : number = null;
                _ = operators.Count != 0 ? oper = operators.Peek() : oper = null;


                    if (numbers.Count!= 0)
                    {
                    if(oper != null && number != null)
                        if (number.Index < oper.Index)
                        {
                            tokens.Add(number.ToString());
                            numbers.Dequeue();
                        }
                        
                    }
                    if(operators.Count != 0)
                    {


                    if (oper != null && number != null)
                        if (number.Index > oper.Index)
                        {
                            if (tokens.Count != 0)
                            {
                                //if last item is an operator, replace it with oper,else just add oper
                                string lastitem = tokens[tokens.Count - 1];
                                if (operatorsArray.Contains(lastitem))
                                {
                                    //if the last operator was a - and the incoming is a -, replace the first with a +
                                    //if the incoming was a +, do no addition
                                    if (lastitem == "-")
                                    {
                                        _ = oper.ToString() == "-" ? tokens[tokens.Count - 1] = "+" : tokens[tokens.Count - 1] = oper.ToString();
                                    }
                                    else if (lastitem == "+")
                                    {
                                        if (oper.ToString() == "-")
                                        {
                                            tokens[tokens.Count - 1] = "-";
                                        }
                                        else
                                        {
                                            tokens[tokens.Count - 1] = oper.ToString();
                                        }
                                    }
                                    else if (lastitem == "*" || lastitem=="/")
                                    {
                                        //if incoming is a +, leave * in place
                                        //if incoming is a -, add - to tokens
                                        if (oper.ToString() == "-")
                                        {
                                            tokens.Add(oper.ToString());
                                        }
                                    } else
                                    {
                                        tokens[tokens.Count - 1] = oper.ToString();
                                    }


                                    operators.Dequeue();
                                }
                                else
                                {
                                    tokens.Add(oper.ToString());
                                    operators.Dequeue();
                                }

                            }
                            else
                            {
                                tokens.Add(oper.ToString());
                                operators.Dequeue();
                            }

                        }
                        
                    }
                    if(oper == null && number != null)
                {
                    tokens.Add(number.ToString());
                    numbers.Dequeue();
                }
                    else if (oper != null && number == null)
                {
                    if (tokens.Count != 0)
                    {
                        //if last item is an operator, replace it with oper,else just add oper
                        string lastitem = tokens[tokens.Count - 1];
                        if (operatorsArray.Contains(lastitem))
                        {
                            tokens[tokens.Count - 1] = oper.ToString();
                            operators.Dequeue();
                        }
                        else
                        {
                            tokens.Add(oper.ToString());
                            operators.Dequeue();
                        }

                    }
                    else
                    {
                        tokens.Add(oper.ToString());
                        operators.Dequeue();
                    }
                }
              
                
            }


            return tokens;
        }

        static void Main(string[] args)
        {
           
            Program program = new Program();
            string expression = "+-";
           
            List<string> result = program.Tokenize(expression);
            foreach (string x in result)
            {
                Console.WriteLine(x);
            }


        }
    }

}
