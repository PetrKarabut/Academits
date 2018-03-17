using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackCalculator.Lexemes;


namespace StackCalculator
{
    class Program
    {

        private static Reader reader;
        private static Calculator calculator;
        private static Dictionary<string, Operand> names;


        static void Main(string[] args)
        {
            reader = new Reader();
            calculator = new Calculator();
            names = new Dictionary<string, Operand>();

            while (true)
            {
                string s = Console.ReadLine();
                var array = s.Split('=');
                if (array.Length == 1)
                {
                    Process(s);
                }
                else if (array.Length == 2)
                {
                    Process(array[0].Trim(), array[1]);
                }
                else
                {
                    Console.WriteLine("Неверный формат строки: присвоение должно быть однократным");
                }
            }
        }


        private static void Process(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return;
            }

            try
            {
                Console.WriteLine(calculator.Calculate(reader.Read(s, names)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void Process(string id, string s)
        {
            if (!isId(id))
            {
                Console.WriteLine($"Неверный формат строки: ({id}) не является идентификатором");
                return;
            }

            double result = 0;

            try
            {
                result = calculator.Calculate(reader.Read(s, names));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            if (names.ContainsKey(id))
            {
                names[id].Value = result;
            }
            else
            {
                var operand = new Operand(id);
                operand.Value = result;

                names.Add(id, operand);
            }
        }


        private static bool isId(string s)
        {
            if (s == null || s.Length == 0 || !char.IsLetter(s[0]))
            {
                return false;
            }

            foreach (var c in s)
            {
                if (!char.IsLetter(c) && !char.IsDigit(c))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
