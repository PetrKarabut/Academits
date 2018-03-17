using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackCalculator.Lexemes
{
    class OpenBracket : ILexeme
    {
        public string Text => "(";

        public char Symbol => '(';
    }
}
