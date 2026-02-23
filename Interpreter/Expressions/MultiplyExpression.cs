namespace Interpreter.Expressions
{
    /// <summary>
    /// Multiplication expression
    /// Represents multiplication operation between two expressions
    /// </summary>
    public class MultiplyExpression : IExpression
    {
        private readonly IExpression _leftExpression;
        private readonly IExpression _rightExpression;

        public MultiplyExpression(IExpression left, IExpression right)
        {
            _leftExpression = left ?? throw new ArgumentNullException(nameof(left));
            _rightExpression = right ?? throw new ArgumentNullException(nameof(right));
        }

        public double Interpret(Context context)
        {
            Console.WriteLine("[Multiply] Evaluating multiplication operation");
            
            var leftValue = _leftExpression.Interpret(context);
            var rightValue = _rightExpression.Interpret(context);
            var result = leftValue * rightValue;
            
            Console.WriteLine($"[Multiply] {leftValue} × {rightValue} = {result}");
            return result;
        }

        public override string ToString()
        {
            return $"({_leftExpression} × {_rightExpression})";
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
            return true; // Multiplication is commutative
        }

        public double EvaluateWithoutContext()
        {
            // Try to evaluate if both operands are constants
            if (_leftExpression is NumberExpression leftNum && _rightExpression is NumberExpression rightNum)
            {
                return leftNum.GetValue() * rightNum.GetValue();
            }
            
            throw new InvalidOperationException("Cannot evaluate without context - contains variables");
        }

        public bool IsAssociative()
        {
            return true; // Multiplication is associative
        }

        public bool IsDistributiveOverAddition()
        {
            return true; // Multiplication distributes over addition
        }
    }
}