using System.Text.RegularExpressions;

namespace Interpreter.Parsers
{
    /// <summary>
    /// Mathematical expression parser
    /// Converts string expressions into expression tree objects
    /// </summary>
    public class ExpressionParser
    {
        private static readonly Regex NumberRegex = new Regex(@"^-?\d+(\.\d+)?");
        private static readonly Regex VariableRegex = new Regex(@"^[a-zA-Z_][a-zA-Z0-9_]*");
        private static readonly Regex WhitespaceRegex = new Regex(@"^\s+");

        public static Interpreter.Expressions.IExpression Parse(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                throw new ArgumentException("Expression cannot be null or empty");
            }

            var tokens = Tokenize(expression);
            var parser = new ExpressionParser();
            return parser.ParseTokens(tokens);
        }

        private static List<string> Tokenize(string expression)
        {
            var tokens = new List<string>();
            var index = 0;

            while (index < expression.Length)
            {
                // Skip whitespace
                var wsMatch = WhitespaceRegex.Match(expression.Substring(index));
                if (wsMatch.Success)
                {
                    index += wsMatch.Length;
                    continue;
                }

                // Check for numbers
                var numMatch = NumberRegex.Match(expression.Substring(index));
                if (numMatch.Success)
                {
                    tokens.Add(numMatch.Value);
                    index += numMatch.Length;
                    continue;
                }

                // Check for variables
                var varMatch = VariableRegex.Match(expression.Substring(index));
                if (varMatch.Success)
                {
                    tokens.Add(varMatch.Value);
                    index += varMatch.Length;
                    continue;
                }

                // Check for operators and parentheses
                var currentChar = expression[index];
                if ("+-*/()".Contains(currentChar))
                {
                    tokens.Add(currentChar.ToString());
                    index++;
                    continue;
                }

                throw new ArgumentException($"Invalid character at position {index}: '{currentChar}'");
            }

            return tokens;
        }

        private int _currentIndex;
        private List<string> _tokens = new List<string>();

        private Interpreter.Expressions.IExpression ParseTokens(List<string> tokens)
        {
            _tokens = tokens;
            _currentIndex = 0;
            var result = ParseExpression();
            
            if (_currentIndex < _tokens.Count)
            {
                throw new ArgumentException($"Unexpected token: {_tokens[_currentIndex]}");
            }
            
            return result;
        }

        private Interpreter.Expressions.IExpression ParseExpression()
        {
            return ParseAdditive();
        }

        private Interpreter.Expressions.IExpression ParseAdditive()
        {
            var left = ParseMultiplicative();

            while (_currentIndex < _tokens.Count)
            {
                var op = _tokens[_currentIndex];
                if (op != "+" && op != "-")
                    break;

                _currentIndex++;
                var right = ParseMultiplicative();

                if (op == "+")
                {
                    left = new Interpreter.Expressions.AddExpression(left, right);
                }
                else
                {
                    left = new Interpreter.Expressions.SubtractExpression(left, right);
                }
            }

            return left;
        }

        private Interpreter.Expressions.IExpression ParseMultiplicative()
        {
            var left = ParseFactor();

            while (_currentIndex < _tokens.Count)
            {
                var op = _tokens[_currentIndex];
                if (op != "*" && op != "/")
                    break;

                _currentIndex++;
                var right = ParseFactor();

                if (op == "*")
                {
                    left = new Interpreter.Expressions.MultiplyExpression(left, right);
                }
                else
                {
                    left = new Interpreter.Expressions.DivideExpression(left, right);
                }
            }

            return left;
        }

        private Interpreter.Expressions.IExpression ParseFactor()
        {
            if (_currentIndex >= _tokens.Count)
            {
                throw new ArgumentException("Unexpected end of expression");
            }

            var token = _tokens[_currentIndex];

            // Parentheses
            if (token == "(")
            {
                _currentIndex++;
                var expr = ParseExpression();
                
                if (_currentIndex >= _tokens.Count || _tokens[_currentIndex] != ")")
                {
                    throw new ArgumentException("Missing closing parenthesis");
                }
                
                _currentIndex++;
                return expr;
            }

            // Numbers
            if (double.TryParse(token, out var number))
            {
                _currentIndex++;
                return new Interpreter.Expressions.NumberExpression(number);
            }

            // Variables
            if (Interpreter.Expressions.VariableExpression.IsValidVariableName(token))
            {
                _currentIndex++;
                return new Interpreter.Expressions.VariableExpression(token);
            }

            throw new ArgumentException($"Unexpected token: {token}");
        }

        public static void DisplayParsingSteps(string expression)
        {
            Console.WriteLine($"\n=== Parsing Expression: {expression} ===");
            
            try
            {
                var tokens = Tokenize(expression);
                Console.WriteLine("Tokens:");
                for (int i = 0; i < tokens.Count; i++)
                {
                    Console.WriteLine($"  [{i}]: '{tokens[i]}'");
                }

                var expr = Parse(expression);
                Console.WriteLine($"\nParsed Expression Tree: {expr}");
                Console.WriteLine(new string('=', 40));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Parsing Error: {ex.Message}");
                Console.WriteLine(new string('=', 40));
            }
        }
    }
}