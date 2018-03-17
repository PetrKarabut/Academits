using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using StackCalculator.Lexemes;
using StackCalculator.Lexemes.Operations;
using StackCalculator.MyStacks;

namespace StackCalculator
{
    class Reader
    {
        private MyQueue<ILexeme> queue;

        private Operation[] binaryOperations;

        private CloseBracket closeBracket;

        private OpenBracket openBracket;

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


        public Reader()
        {
            binaryOperations = new Operation[] { new Plus(), new Minus(), new Multiplication(), new Division()};

            closeBracket = new CloseBracket();
            openBracket = new OpenBracket();

            nameBuilder = new StringBuilder();
            numberBuilder = new StringBuilder();
        }


        public MyQueue<ILexeme> Read(string s, Dictionary<string, Operand> dictionary)
        {
            queue = new MyQueue<ILexeme>();

            idNames = dictionary;

            status = State.Begining;

            foreach (var c in s)
            {
                AddChar(c);
            }

            EndName();
            EndNumber();

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
                ThrowException();
            }
        }

        private void AddOperation(Operation operation)
        {
            if (status == State.AfterOperation)
            {
                ThrowException();
                return;
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
                ThrowException();
                return;
            }

            queue.Enqueue(openBracket);
            status = State.AfterOpeningBracket;
        }


        private void AddClosingBracket()
        {
            if (status == State.Begining || status == State.AfterOpeningBracket || status == State.AfterClosingBracket)
            {
                ThrowException();
                return;
            }

            EndName();
            EndNumber();
            queue.Enqueue(closeBracket);
            status = State.AfterClosingBracket;
           
        }


        private void AddDigit(char c)
        {
            if (status == State.AfterClosingBracket)
            {
                ThrowException();
                return;
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
                ThrowException();
                return;
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
                throw new FormatException($"идентификатор {s} неизвестен");
            }

            nameBuilder.Clear();
        }


        private void ThrowException()
        {
            throw new FormatException("Строка имеет неверный формат.");
        }

        public string GetLexemesString()
        {
            var s = new StringBuilder();

            while (queue.Count > 0)
            {
                s.Append(queue.Dequeue().Text);
            }

            return s.ToString();
        }

    }
}
