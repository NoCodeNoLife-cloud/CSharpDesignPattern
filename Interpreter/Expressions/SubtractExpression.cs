namespace Interpreter.Expressions
{
    /// <summary>
    /// Subtraction expression
    /// Represents subtraction operation between two expressions
    /// </summary>
    public class SubtractExpression : IExpression
    {
        private readonly IExpression _leftExpression;
        private readonly IExpression _rightExpression;

        public SubtractExpression(IExpression left, IExpression right)
        {
            _leftExpression = left ?? throw new ArgumentNullException(nameof(left));
            _rightExpression = right ?? throw new ArgumentNullException(nameof(right));
        }

        public double Interpret(Context context)
        {
            Console.WriteLine("[Subtract] Evaluating subtraction operation");
            
            var leftValue = _leftExpression.Interpret(context);
            var rightValue = _rightExpression.Interpret(context);
            var result = leftValue - rightValue;
            
            Console.WriteLine($"[Subtract] {leftValue} - {rightValue} = {result}");
            return result;
        }

        public override string ToString()
        {
            return $"({_leftExpression} - {_rightExpression})";
        }

        public IExpression GetLeftExpression()
        {
            return _leftExpression;
        }

        public IExpression GetRightExpression()
        {
            return _rightExpression;
        }

        public bool IsCommutative()
        {
            return false; // Subtraction is not commutative
        }

        public double EvaluateWithoutContext()
        {
            // Try to evaluate if both operands are constants
            if (_leftExpression is NumberExpression leftNum && _rightExpression is NumberExpression rightNum)
            {
                return leftNum.GetValue() - rightNum.GetValue();
            }
            
            throw new InvalidOperationException("Cannot evaluate without context - contains variables");
        }

        public bool IsAssociative()
        {
            return false; // Subtraction is not associative
        }
    }
}