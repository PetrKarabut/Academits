using System;

namespace StackCalculator.Lexemes
{
    public class Operation : ILexeme
    {

        public string Text => Symbol.ToString();

        public int Priority { get; private set; }

        public char Symbol { get; private set; }

        public bool AllowsUnarForm { get; private set; }

        private Func<double, double, double> operate;

        public double Result(double x, double y)
        {
            var result = operate(x, y);

            if (double.IsNaN(result))
            {
                throw new OperationException($"результат выполнения операции {Text} над числами {x} и {y} не определен");
            }

            if (double.IsPositiveInfinity(result))
            {
                throw new OperationException($"результат выполнения операции {Text} над числами {x} и {y} слишком велик");
            }

            if (double.IsNegativeInfinity(result))
            {
                throw new OperationException($"результат выполнения операции {Text} над числами {x} и {y} слишком мал");
            }

            return result;
        }

       


       
        public Operation(int priority, char symbol, bool allowsUnarForm, Func<double, double, double> operate)
        {
            Priority = priority;
            Symbol = symbol;
            AllowsUnarForm = allowsUnarForm;
            this.operate = operate;
        }

       
        public static Operation Plus
        {
            get
            {
                if (plus == null)
                {
                    plus = new Operation(1, '+', true, (x, y) => x + y);
                }

                return plus;
            }
        }

        public static Operation Minus
        {
            get
            {
                if (minus == null)
                {
                    minus = new Operation(1, '-', true, (x, y) => x - y);
                }

                return minus;
            }
        }

        public static Operation Division
        {
            get
            {
                if (division == null)
                {
                    division = new Operation(2, '/', false, (x, y) => Math.Truncate(x / y));
                }

                return division;
            }
        }

        public static Operation Multiplication
        {
            get
            {
                if (multiplication == null)
                {
                    multiplication = new Operation(2, '*', false, (x, y) => x * y);
                }

                return multiplication;
            }
        }

        private static Operation plus;

        private static Operation minus;

        private static Operation division;

        private static Operation multiplication;

    }
}
