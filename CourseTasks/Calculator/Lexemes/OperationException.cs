using System;

namespace StackCalculator.Lexemes
{
    public class OperationException : Exception
    {
        public OperationException(string message) : base(message) { }
    }
}
