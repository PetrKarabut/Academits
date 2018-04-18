using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackCalculator.MyStacks
{
    public class StackException : InvalidOperationException
    {
        public StackException(string message) : base(message) { }
       
    }
}
