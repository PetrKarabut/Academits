using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using StackCalculator.Lexemes;
using StackCalculator.MyStacks;

namespace StackCalculator
{
    class Reader
    {
        private MyQueue<ILexeme> queue;

        private Operation[] binaryOperations;

        private CloseBracket closeBracket;

        private OpenBracket openBracket;

        private Operand zero;

        private enum State
        {
            Begining,
            AfterOperation,
            AfterOpeningBracket,
            AfterClosingBracket,
            InName,
            InNumber
        }

        private State status;

        private StringBuilder nameBuilder;

        private StringBuilder numberBuilder;

        private Dictionary<string, Operand> idNames;

        private int bracketsCount;


        public Reader()
        {
            binaryOperations = new Operation[] { Operation.Plus, Operation.Minus, Operation.Multiplication, Operation.Division};

            closeBracket = new CloseBracket();
            openBracket = new OpenBracket();

            nameBuilder = new StringBuilder();
            numberBuilder = new StringBuilder();

            zero = new Operand(0);
        }


        public MyQueue<ILexeme> Read(string s, Dictionary<string, Operand> dictionary)
        {
            queue = new MyQueue<ILexeme>();

            bracketsCount = 0;

            idNames = dictionary;

            status = State.Begining;

            foreach (var c in s)
            {
                AddChar(c);
            }

            EndName();
            EndNumber();

            if (!(queue.Last is Operand) && queue.Last != closeBracket)
            {
                ThrowException("выражение должно заканчиваться на операнд или закрывающую скобку");
            }

            if (bracketsCount != 0)
            {
                ThrowException("количество открывающих и закрывающих скобок различно");
            }

            return queue;
        }

        private void AddChar(char c)
        {
            foreach (var operation in binaryOperations)
            {
                if (c == operation.Symbol)
                {
                    AddOperation(operation);
                    return;
                }
            }

            if (c == openBracket.Symbol)
            {
                AddOpenBracket();
                return;
            }


            if (c == closeBracket.Symbol)
            {
                AddClosingBracket();
                return;
            }


            if (char.IsDigit(c))
            {
                AddDigit(c);
                return;
            }

            if (char.IsLetter(c))
            {
                AddLetter(c);
                return;
            }

            if (!char.IsWhiteSpace(c))
            {
                ThrowException("недопустимый символ " + c.ToString());
            }
        }

        private void AddOperation(Operation operation)
        {
            if (status == State.AfterOperation)
            {
                ThrowException($"операция {operation.Text} следует сразу после другой операции");
            }

            if (operation.AllowsUnarForm && (status == State.AfterOpeningBracket || status == State.Begining))
            {
                queue.Enqueue(zero);
            }

            EndNumber();
            EndName();
            queue.Enqueue(operation);
            status = State.AfterOperation;
        }


        private void AddOpenBracket()
        {
            if (status == State.AfterClosingBracket || status == State.InName || status == State.InNumber)
            {
                ThrowException("открывающая скобка стоит сразу после операнда или закрывающей скобки");
            }

            queue.Enqueue(openBracket);
            bracketsCount++;
            status = State.AfterOpeningBracket;
        }


        private void AddClosingBracket()
        {
            if (status == State.Begining || status == State.AfterOpeningBracket || status == State.AfterOperation)
            {
                ThrowException("закрывающая скобка стоит в начале или сразу после открывающей скобки или после операции");
            }

            EndName();
            EndNumber();
            queue.Enqueue(closeBracket);
            bracketsCount--;
            status = State.AfterClosingBracket;
           
        }


        private void AddDigit(char c)
        {
            if (status == State.AfterClosingBracket)
            {
                ThrowException("цифра стоит сразу после закрывающей скобки");
            }


            if (status == State.InName)
            {
                nameBuilder.Append(c);
                return;
            }


            numberBuilder.Append(c);
            status = State.InNumber;
        }


        private void AddLetter(char c)
        {
            if (status == State.AfterClosingBracket || status == State.InNumber)
            {
                ThrowException("цифра стоит сразу после закрывающей скобки или числа");
            }

            nameBuilder.Append(c);
            status = State.InName;
        }


        private void EndNumber()
        {
            if (numberBuilder.Length == 0)
            {
                return;
            }

            queue.Enqueue(new Operand(double.Parse(numberBuilder.ToString())));
            numberBuilder.Clear();
        }

        private void EndName()
        {
            if (nameBuilder.Length == 0)
            {
                return;
            }

            var s = nameBuilder.ToString();

            if (idNames.ContainsKey(s))
            {
                queue.Enqueue(idNames[s]);
            }
            else
            {
                ThrowException($"идентификатор {s} неизвестен");
            }

            nameBuilder.Clear();
        }


        private void ThrowException(string s)
        {
            numberBuilder.Clear();
            nameBuilder.Clear();
            throw new FormatException("Строка имеет неверный формат: " + s);
        }

       

    }
}
