using System;
using System.Collections.Generic;
using System.Text;
using StackCalculator.Lexemes;
using StackCalculator.MyStacks;

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

            //Test("x", "y");

            Run();
        }

        // с помощью этого метода можно проверить, что строка правильно переводится
        // в очередь лексем
        private static void Test(params string[] variables)
        {
            var testNames = new Dictionary<string, Operand>();

            foreach (var variable in variables)
            {
                testNames.Add(variable, new Operand(variable));
            }

            while (true)
            {
                string s = Console.ReadLine();

                if (string.IsNullOrEmpty(s))
                {
                    continue;
                }

                MyStacks.MyQueue<ILexeme> queue = null;

                try
                {
                    queue = reader.Read(s, testNames);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                var builder = new StringBuilder();

                while (queue.Count > 0)
                {
                    builder.Append(queue.Dequeue().Text + " ");
                }

                Console.WriteLine(builder.ToString());

            }
        }


        private static void Run()
        {
            while (true)
            {
                string s = Console.ReadLine();
                var array = s.Split('=');
                if (array.Length == 1)
                {
                    var result = Process(s);
                    if (result != null)
                    {
                        Console.WriteLine(result);
                    }
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


        private static double? Process(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }

            MyQueue<ILexeme> lexemes = null;

            try
            {
                lexemes = reader.Read(s, names);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            try
            {
                return calculator.Calculate(lexemes);
            }
            catch (StackException)
            {
                Console.WriteLine("Не удалось вычислить значение выражения: некорректное выражение");
            }
            catch (OperationException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception)
            {
                Console.WriteLine("Не удалось вычислить значение выражения.");
            }

            return null;
        }

        private static void Process(string id, string s)
        {
            if (!IsId(id))
            {
                Console.WriteLine($"Неверный формат строки: ({id}) не является идентификатором");
                return;
            }

            var result = Process(s);

            if (result == null)
            {
                return;
            }

            if (names.ContainsKey(id))
            {
                names[id].Value = result.Value;
            }
            else
            {
                var operand = new Operand(id);
                operand.Value = result.Value;

                names.Add(id, operand);
            }
        }


        private static bool IsId(string s)
        {
            if (string.IsNullOrEmpty(s) || !char.IsLetter(s[0]))
            {
                return false;
            }

            foreach (var c in s)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
