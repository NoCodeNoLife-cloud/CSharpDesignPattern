namespace Interpreter.Expressions
{
    /// <summary>
    /// Number literal expression
    /// Represents a constant numeric value in the expression tree
    /// </summary>
    public class NumberExpression : IExpression
    {
        private readonly double _value;

        public NumberExpression(double value)
        {
            _value = value;
        }

        public NumberExpression(string value)
        {
            if (!double.TryParse(value, out _value))
            {
                throw new ArgumentException($"Invalid number format: {value}");
            }
        }

        public double Interpret(Context context)
        {
            Console.WriteLine($"[Number] Interpreting constant value: {_value}");
            return _value;
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public double GetValue()
        {
            return _value;
        }

        public bool IsInteger()
        {
            return Math.Abs(_value - Math.Round(_value)) < double.Epsilon;
        }

        public bool IsPositive()
        {
            return _value > 0;
        }

        public bool IsNegative()
        {
            return _value < 0;
        }

        public bool IsZero()
        {
            return Math.Abs(_value) < double.Epsilon;
        }
    }
}