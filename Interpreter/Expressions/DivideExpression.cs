namespace Interpreter.Expressions
{
    /// <summary>
    /// Division expression
    /// Represents division operation between two expressions
    /// </summary>
    public class DivideExpression : IExpression
    {
        private readonly IExpression _dividend;
        private readonly IExpression _divisor;

        public DivideExpression(IExpression dividend, IExpression divisor)
        {
            _dividend = dividend ?? throw new ArgumentNullException(nameof(dividend));
            _divisor = divisor ?? throw new ArgumentNullException(nameof(divisor));
        }

        public double Interpret(Context context)
        {
            Console.WriteLine("[Divide] Evaluating division operation");
            
            var dividendValue = _dividend.Interpret(context);
            var divisorValue = _divisor.Interpret(context);
            
            if (Math.Abs(divisorValue) < double.Epsilon)
            {
                throw new DivideByZeroException("Division by zero is not allowed");
            }
            
            var result = dividendValue / divisorValue;
            
            Console.WriteLine($"[Divide] {dividendValue} ÷ {divisorValue} = {result}");
            return result;
        }

        public override string ToString()
        {
            return $"({_dividend} ÷ {_divisor})";
        }

        public IExpression GetDividend()
        {
            return _dividend;
        }

        public IExpression GetDivisor()
        {
            return _divisor;
        }

        public bool IsCommutative()
        {
            return false; // Division is not commutative
        }

        public double EvaluateWithoutContext()
        {
            // Try to evaluate if both operands are constants
            if (_dividend is NumberExpression dividendNum && _divisor is NumberExpression divisorNum)
            {
                if (Math.Abs(divisorNum.GetValue()) < double.Epsilon)
                {
                    throw new DivideByZeroException("Division by zero");
                }
                return dividendNum.GetValue() / divisorNum.GetValue();
            }
            
            throw new InvalidOperationException("Cannot evaluate without context - contains variables");
        }

        public bool IsAssociative()
        {
            return false; // Division is not associative
        }

        public bool HasZeroDivisor()
        {
            // Check if divisor is a zero constant
            if (_divisor is NumberExpression numExpr)
            {
                return Math.Abs(numExpr.GetValue()) < double.Epsilon;
            }
            return false;
        }

        public IExpression GetSimplifiedForm()
        {
            // Simplify if both operands are constants
            if (_dividend is NumberExpression dividendNum && _divisor is NumberExpression divisorNum)
            {
                if (Math.Abs(divisorNum.GetValue()) > double.Epsilon)
                {
                    var result = dividendNum.GetValue() / divisorNum.GetValue();
                    return new NumberExpression(result);
                }
            }
            
            return this;
        }
    }
}