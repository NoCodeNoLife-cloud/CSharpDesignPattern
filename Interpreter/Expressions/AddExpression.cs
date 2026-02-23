namespace Interpreter.Expressions
{
    /// <summary>
    /// Addition expression
    /// Represents addition operation between two expressions
    /// </summary>
    public class AddExpression : IExpression
    {
        private readonly IExpression _leftExpression;
        private readonly IExpression _rightExpression;

        public AddExpression(IExpression left, IExpression right)
        {
            _leftExpression = left ?? throw new ArgumentNullException(nameof(left));
            _rightExpression = right ?? throw new ArgumentNullException(nameof(right));
        }

        public double Interpret(Context context)
        {
            Console.WriteLine("[Add] Evaluating addition operation");
            
            var leftValue = _leftExpression.Interpret(context);
            var rightValue = _rightExpression.Interpret(context);
            var result = leftValue + rightValue;
            
            Console.WriteLine($"[Add] {leftValue} + {rightValue} = {result}");
            return result;
        }

        public override string ToString()
        {
            return $"({_leftExpression} + {_rightExpression})";
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
            return true; // Addition is commutative
        }

        public double EvaluateWithoutContext()
        {
            // Try to evaluate if both operands are constants
            if (_leftExpression is NumberExpression leftNum && _rightExpression is NumberExpression rightNum)
            {
                return leftNum.GetValue() + rightNum.GetValue();
            }
            
            throw new InvalidOperationException("Cannot evaluate without context - contains variables");
        }
    }
}