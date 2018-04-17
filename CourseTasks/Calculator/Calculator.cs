using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackCalculator.Lexemes;
using StackCalculator.MyStacks;

namespace StackCalculator
{
    class Calculator
    {
        private MyQueue<ILexeme> ToPolish(MyQueue<ILexeme> queue)
        {
            var polish = new MyQueue<ILexeme>();
            var stack = new MyStack<ILexeme>();

            while (queue.Count > 0)
            {
                var next = queue.Dequeue();

                if (next is Operand)
                {
                    polish.Enqueue(next);
                    continue;
                }

                if (next is OpenBracket)
                {
                    stack.Push(next);
                    continue;
                }

                if (next is CloseBracket)
                {
                    while (!(stack.Peek() is OpenBracket))
                    {
                        polish.Enqueue(stack.Pop());
                    }

                    stack.Pop();
                    continue;
                }

                if (next is Operation)
                {
                    var priority = ((Operation)next).Priority;
                    while (true)
                    {
                        if (stack.Count == 0)
                        {
                            stack.Push(next);
                            break;
                        }

                        var peek = stack.Peek();

                        if (peek is Operation && ((Operation)peek).Priority >= priority)
                        {
                            polish.Enqueue(stack.Pop());
                            continue;
                        }

                        stack.Push(next);
                        break;
                    }
                }

            }

            while (stack.Count > 0)
            {
                polish.Enqueue(stack.Pop());
            }

            return polish;
        }

        private double CalculatePolish(MyQueue<ILexeme> queue)
        {
            var stack = new MyStack<ILexeme>();

            while (queue.Count > 0)
            {
                var next = queue.Dequeue();

                if (next is Operand)
                {
                    stack.Push(next);
                    continue;
                }

                if (next is Operation)
                {
                    var operand1 = (Operand)stack.Pop();
                    var operand2 = (Operand)stack.Pop();
                    var result = ((Operation)next).Result(operand2.Value, operand1.Value);
                    stack.Push(new Operand(result));
                }

            }

            return ((Operand)stack.Pop()).Value;
        }

        public double Calculate(MyQueue<ILexeme> queue)
        {
            return CalculatePolish(ToPolish(queue));
        }
    }
}
