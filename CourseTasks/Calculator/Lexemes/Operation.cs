using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackCalculator.Lexemes
{
    public abstract class Operation : ILexeme
    {
        public abstract string Text { get; }
        
        public abstract int Priority { get; }

        public abstract char Symbol { get; }

        public abstract double Result(double x, double y);

        public virtual bool CanBeUnary => false;
    }
}
