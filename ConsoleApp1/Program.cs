using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shuntingYard
{
#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
    public class Operator
#pragma warning restore CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
#pragma warning restore CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
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
                    precedence = 3;
                    break;
                case "/":
                    associativity = 1;
                    precedence = 3;
                    break;
                case "+":
                    associativity = 1;
                    precedence = 2;
                    break;
                case "-":
                    associativity = 1;
                    precedence = 2;
                    break;
                case "^":
                    associativity = -1;
                    precedence = 4;
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
    }


    public class Program
    {

        public Queue<string> ShuntingYardAlgorithm(string var)
        {
            /*accepts an infix expression as a string; converts it into a posfix expression
             * @param : a non-null string of length 0 to max
             *             
             * @return: a Queue containing tokens of the postfix equivalent of the input expression
             */            
            Queue<string> Output = new Queue<string>();
            Stack<Operator> Operators = new Stack<Operator>();

            List<string> tokens = Tokenize(var);

            foreach (string x in tokens)
            {


                if (float.TryParse(x.ToString(), out float number))
                {
                    Output.Enqueue(number.ToString());
                }
                else
                {
                    try
                    {
                        Operator token = new Operator(x);
                        bool breakout = false;
                            while (!breakout && Operators.Count != 0)
                            {
                                if((Operators.Peek() > token) && Operators.Peek().GetName() != "(" || (Operators.Peek() == token) && Operators.Peek().GetAssociativity() == 1)
                                {
                                    Operator current = Operators.Pop();
                                    Output.Enqueue(current.ToString());
                                }
                                else
                                {
                                    breakout = true;
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
                                Output.Enqueue(current.ToString());
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
                    Output.Enqueue(@operator.ToString());
                }

            }


          
            return Output;


        }


        public List<string> Tokenize(string expression)
        {
            //purpose: separates a string expression of numbers and operators into tokens; also simplifies an expression by
            //removing excess operators
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
                                    if (oper.ToString() == "(" || oper.ToString() == ")")
                                    {
                                        tokens.Add(oper.ToString());
                                    }

                                    else if (lastitem == "-")
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
                                        tokens.Add(oper.ToString());
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

        public float HandleMath(float n1,float n2,string Operator)
        {
            switch (Operator)
            {
                case ("+"):
                    return n1 + n2;

                case ("-"):
                    return n1 - n2;

                case ("*"):
                    return n1 * n2;
                case ("/"):
                    return n1 / n2;
                default:
                    return 0;

            }
        }

        public float Evaluator(Queue<string> postfix)
        {
            /*purpose: accepts a postfix expression, computes the result of the expression 
             * @param: a non-null queue containing tokens of a postfix in the appropriate order: queue must contain
             * at least one number;
             * @return: an int representing the result of the expression
             */
            Stack<float> eval = new Stack<float>();
            foreach(string item in postfix)
            {
                if (float.TryParse(item,out float number))
                {
                    eval.Push(number);
                }
                else
                {
                    float first = eval.Pop();
                    float result = 1;
                    try
                    {
                        float second = eval.Pop();
                        result = HandleMath(first, second, item);
                    }
                    catch (System.InvalidOperationException)
                    {
                        string intermediate = item + "1";
                        if (float.TryParse(intermediate,out float second))
                        {
                            result = first * second;
                        }

                    }
                    catch (DivideByZeroException)
                    {
                        return (float)Double.NaN;
                    }

                    eval.Push(result);

                }
            }
            return eval.Pop();
        }

        static void Main(string[] args)
        {
            string expression = "(1+3)*(5+8)";
            Program program = new Program();

            List<string> result = program.Tokenize(expression);


        }
    }

}
