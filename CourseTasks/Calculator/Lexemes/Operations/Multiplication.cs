using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackCalculator.Lexemes.Operations
{
    public class Multiplication : Operation
    {
        public override string Text => "*";

        public override int Priority => 2;

        public override char Symbol => '*';

        public override double Result(double x, double y)
        {
            return x * y;
        }
    }
}
