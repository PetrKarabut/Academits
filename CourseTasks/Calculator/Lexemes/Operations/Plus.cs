using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackCalculator.Lexemes.Operations
{
    public class Plus : Operation
    {
        public override string Text => "+";

        public override int Priority => 1;

        public override char Symbol => '+';

        public override double Result(double x, double y)
        {
            return x + y;
        }

        public override bool CanBeUnary => true;
    }
}
