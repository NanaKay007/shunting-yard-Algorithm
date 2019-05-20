using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Operator
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

        public string getName()
        {
            return name;
        }

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


    class Program
    {
       
        static void Main(string[] args)
        {
            string var = Console.ReadLine();
            Queue<Object> Ouput = new Queue<Object>();
            Stack<Operator> Operators = new Stack<Operator>();
            foreach (char x in var)
            {
                int number;
                

                if (int.TryParse(x.ToString(),out number))
                {
                    Ouput.Enqueue(number);
                }
                else
                {
                    try
                    {
                        Operator token = new Operator(x);
                        if (Operators.Count != 0)
                        {
                            while ((Operators.Peek() > token) && Operators.Peek().getName() != "(" || (Operators.Peek() == token) && Operators.Peek().getAssociativity()==1)
                            {
                                Operator current = Operators.Pop();
                                Ouput.Enqueue(current);
                            }
                        }
                        Operators.Push(token);
                       
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message); 
                        if(x == ')')
                        {
                            while (Operators.Count != 0 && Operators.Peek().getName()!= "(")
                            {
                                Operator current = Operators.Pop();
                                Ouput.Enqueue(current);
                            }
                            if(Operators.Count != 0 && Operators.Peek().getName() != "(")
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
                if(@operator.getName() != "(")
                {
                    Ouput.Enqueue(@operator);
                }
                
            }


            //print operands in Operand Stack
            foreach (Object x in Ouput)
            {        
                    Console.Write(x.ToString());
            }
            

            //print operators in Operator Stack
            Console.ReadKey();

        }
    }

}
