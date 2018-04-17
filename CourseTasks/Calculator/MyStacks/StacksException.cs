using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackCalculator.MyStacks
{
    public class StacksException : InvalidOperationException
    {
        public StacksException(string message) : base(message) { }
       
    }
}
