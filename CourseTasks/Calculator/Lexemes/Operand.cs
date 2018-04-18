
namespace StackCalculator.Lexemes
{
    public class Operand : ILexeme
    {
        public Operand(string name)
        {
            Text = name;
        }

        public Operand(double value)
        {
            Value = value;
            Text = value.ToString();
        }

        public string Text { get; private set; }

        public double Value { get; set; }
    }
}
