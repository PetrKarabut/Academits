using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackCalculator.Lexemes
{
    public class Operand : ILexeme
    {
        private string name;

        public Operand(string name)
        {
            this.name = name;
        }

        public Operand(double value)
        {
            Value = value;
            name = value.ToString();
        }

        public string Text => name;

        public double Value { get; set; }
    }
}
