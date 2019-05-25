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
            foreach (char x in var)
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

                        if (x == ')')
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

        private static List<T> CreateList<T>(int capacity)
        {
            return Enumerable.Repeat(default(T), capacity).ToList();
        }

        public List<string> Tokenize(string expression)
        {
            //purpose: separates an expression into tokens; numbers and operators
            //params: a string expression
            //return: a list of tokens; throws an error if an invalid token is encountered in the expression


            Regex numberRegex = new Regex(@"\d+\.?\d*");
            Regex operatorRegex = new Regex(@"[\+\-\/\*]");
            Regex invalidRegex = new Regex("[^\\d+*/-]");


            MatchCollection numberMatches = numberRegex.Matches(expression);
            MatchCollection operatorMatches = operatorRegex.Matches(expression);
            MatchCollection invalidMatches = invalidRegex.Matches(expression);

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
                            tokens.Add(oper.ToString());
                            operators.Dequeue();
                        }
                        
                    }
                    if(oper == null && number != null)
                {
                    tokens.Add(number.ToString());
                    numbers.Dequeue();
                }
                    else if (oper != null && number == null)
                {
                    tokens.Add(oper.ToString());
                    operators.Dequeue();
                }
              
                //else
                //{
                //    var current_operator = oper;
                //    while(current_operator.NextMatch() != null)
                //    {
                //        current_operator = current_operator.NextMatch();
                //        if(Math.Abs(current_operator.Index - number.Index) == 1)
                //        {
                //            tokens.Add(current_operator.ToString());

                //        }
                //    }
                //}
            }

            //int j = 0;

            //for (int i = 0; i < size; i++)
            //{
            //Match numberMatch;
            //Match operatorMatch;
            

            //_ = i < numberMatches.Count? numberMatch = numberMatches[i]: numberMatch = null;

            //_ = i < operatorMatches.Count? operatorMatch = operatorMatches[i]: operatorMatch=null;

            //if(numberMatch != null && operatorMatch != null)
            //{


            //        if (numberMatch.Index < operatorMatch.Index)
            //        {

            //                tokens[j] = numberMatch.ToString();
            //                tokens[j + 1] = operatorMatch.ToString();


            //        }
            //        else
            //        {




            //            if(operatorMatch.Index < operatorMatch.NextMatch().Index)
            //            {
            //                tokens[j] = operatorMatch.ToString();
            //            }
            //            else
            //            {
            //                tokens[j] = operatorMatch.NextMatch().ToString();
            //            }
            //            tokens[j + 1] = numberMatch.ToString();
            //        }
            //        j += 2;

            //    } 
            //else if (numberMatch != null && operatorMatch==null)
            //{
            //    tokens[j] = numberMatch.ToString();
            //        j++;
            //}

            //if(j == size)
            //    {
            //        return tokens;
            //    }
            //}
            




            //foreach( Match match in operatorMatches)
            //{
            //    foreach(Group group in match.Groups)
            //    {
            //        Console.WriteLine(group);
            //    }
            //}

            return tokens;
        }

        static void Main(string[] args)
        {
           
            Program program = new Program();
            string expression = "+12+-553";
            List<string> result = program.Tokenize(expression);
            foreach (string x in result)
            {
                Console.WriteLine(x);
            }
        }
    }

}
