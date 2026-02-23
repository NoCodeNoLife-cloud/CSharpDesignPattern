// See https://aka.ms/new-console-template for more information
using Interpreter.Expressions;
using Interpreter.Parsers;

namespace Interpreter
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Interpreter Pattern Implementation Examples ===\n");

            try
            {
                // Demonstrate basic arithmetic expressions
                DemonstrateBasicArithmetic();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate variable expressions
                DemonstrateVariableExpressions();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate expression parsing
                DemonstrateExpressionParsing();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate complex mathematical expressions
                DemonstrateComplexExpressions();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate interpreter pattern benefits
                DemonstratePatternBenefits();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
            
            Console.WriteLine("\n=== Interpreter Pattern Demo Completed ===");
            Console.ReadKey();
        }

        /// <summary>
        /// Demonstrates basic arithmetic expressions
        /// </summary>
        private static void DemonstrateBasicArithmetic()
        {
            Console.WriteLine("1. Basic Arithmetic Expressions Demo");
            Console.WriteLine("====================================");
            
            var context = new Context();
            
            // Simple addition
            var expr1 = new AddExpression(
                new NumberExpression(10),
                new NumberExpression(5)
            );
            Console.WriteLine($"Expression: {expr1}");
            var result1 = expr1.Interpret(context);
            Console.WriteLine($"Result: {result1}\n");
            
            // Complex expression
            var expr2 = new MultiplyExpression(
                new AddExpression(new NumberExpression(3), new NumberExpression(4)),
                new SubtractExpression(new NumberExpression(10), new NumberExpression(2))
            );
            Console.WriteLine($"Expression: {expr2}");
            var result2 = expr2.Interpret(context);
            Console.WriteLine($"Result: {result2}\n");
            
            // Division with error handling
            try
            {
                var expr3 = new DivideExpression(
                    new NumberExpression(20),
                    new NumberExpression(4)
                );
                Console.WriteLine($"Expression: {expr3}");
                var result3 = expr3.Interpret(context);
                Console.WriteLine($"Result: {result3}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\n");
            }
        }

        /// <summary>
        /// Demonstrates variable expressions and context usage
        /// </summary>
        private static void DemonstrateVariableExpressions()
        {
            Console.WriteLine("\n2. Variable Expressions Demo");
            Console.WriteLine("=============================");
            
            // Create context with initial variables
            var initialVars = new Dictionary<string, double>
            {
                { "x", 15 },
                { "y", 8 },
                { "z", 3 }
            };
            
            var context = new Context(initialVars);
            context.DisplayContext();
            
            // Variable expression
            var varExpr = new VariableExpression("x");
            Console.WriteLine($"\nVariable Expression: {varExpr}");
            var varResult = varExpr.Interpret(context);
            Console.WriteLine($"Result: {varResult}\n");
            
            // Mixed expression with variables and constants
            var mixedExpr = new AddExpression(
                new MultiplyExpression(new VariableExpression("x"), new VariableExpression("y")),
                new NumberExpression(100)
            );
            Console.WriteLine($"Mixed Expression: {mixedExpr}");
            var mixedResult = mixedExpr.Interpret(context);
            Console.WriteLine($"Result: {mixedResult}\n");
            
            // Update variable and re-evaluate
            context.SetVariable("x", 25);
            Console.WriteLine($"After updating x = 25:");
            var updatedResult = mixedExpr.Interpret(context);
            Console.WriteLine($"New Result: {updatedResult}\n");
            
            context.DisplayContext();
        }

        /// <summary>
        /// Demonstrates expression parsing from strings
        /// </summary>
        private static void DemonstrateExpressionParsing()
        {
            Console.WriteLine("\n3. Expression Parsing Demo");
            Console.WriteLine("===========================");
            
            // Show parsing steps
            ExpressionParser.DisplayParsingSteps("10 + 5");
            ExpressionParser.DisplayParsingSteps("(3 + 4) * (10 - 2)");
            ExpressionParser.DisplayParsingSteps("x * y + z");
            ExpressionParser.DisplayParsingSteps("(a + b) / (c - d)");
            
            // Parse and evaluate expressions
            var context = new Context(new Dictionary<string, double>
            {
                { "a", 20 },
                { "b", 15 },
                { "c", 10 },
                { "d", 5 }
            });
            
            var expressions = new[]
            {
                "100 + 50",
                "a * b",
                "(a + b) / (c - d)",
                "a + b * c - d"
            };
            
            Console.WriteLine("\nEvaluating parsed expressions:");
            foreach (var exprString in expressions)
            {
                try
                {
                    var parsedExpr = ExpressionParser.Parse(exprString);
                    Console.WriteLine($"\nExpression: {exprString}");
                    Console.WriteLine($"Parsed: {parsedExpr}");
                    var result = parsedExpr.Interpret(context);
                    Console.WriteLine($"Result: {result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error parsing '{exprString}': {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Demonstrates complex mathematical expressions
        /// </summary>
        private static void DemonstrateComplexExpressions()
        {
            Console.WriteLine("\n4. Complex Mathematical Expressions Demo");
            Console.WriteLine("========================================");
            
            var context = new Context(new Dictionary<string, double>
            {
                { "principal", 1000 },
                { "rate", 0.05 },
                { "time", 2 },
                { "pi", 3.14159 }
            });
            
            // Compound interest calculation: P(1 + r)^t
            var compoundInterest = new MultiplyExpression(
                new VariableExpression("principal"),
                new PowerExpression(
                    new AddExpression(new NumberExpression(1), new VariableExpression("rate")),
                    new VariableExpression("time")
                )
            );
            
            Console.WriteLine($"Compound Interest Expression: {compoundInterest}");
            var ciResult = compoundInterest.Interpret(context);
            Console.WriteLine($"Result: ${ciResult:F2}\n");
            
            // Circle area calculation: π * r²
            var radius = new NumberExpression(5);
            var circleArea = new MultiplyExpression(
                new VariableExpression("pi"),
                new PowerExpression(radius, new NumberExpression(2))
            );
            
            Console.WriteLine($"Circle Area Expression: {circleArea}");
            var areaResult = circleArea.Interpret(context);
            Console.WriteLine($"Result: {areaResult:F2} square units\n");
            
            // Quadratic formula component: (-b + √(b² - 4ac)) / (2a)
            var a = new NumberExpression(1);
            var b = new NumberExpression(-5);
            var c = new NumberExpression(6);
            
            var discriminant = new SubtractExpression(
                new PowerExpression(b, new NumberExpression(2)),
                new MultiplyExpression(
                    new MultiplyExpression(new NumberExpression(4), a),
                    c
                )
            );
            
            var quadraticNumerator = new AddExpression(
                new MultiplyExpression(new NumberExpression(-1), b),
                new SqrtExpression(discriminant)
            );
            
            var quadraticDenominator = new MultiplyExpression(new NumberExpression(2), a);
            
            var quadraticFormula = new DivideExpression(quadraticNumerator, quadraticDenominator);
            
            Console.WriteLine($"Quadratic Formula Expression: {quadraticFormula}");
            var quadResult = quadraticFormula.Interpret(context);
            Console.WriteLine($"Result: {quadResult:F2}");
        }

        /// <summary>
        /// Demonstrates interpreter pattern benefits and concepts
        /// </summary>
        private static void DemonstratePatternBenefits()
        {
            Console.WriteLine("\n5. Interpreter Pattern Benefits Demo");
            Console.WriteLine("=====================================");
            
            Console.WriteLine("Interpreter Pattern Key Benefits:");
            Console.WriteLine("• Provides way to evaluate sentences in a language");
            Console.WriteLine("• Easy to change and extend grammar");
            Console.WriteLine("• Implements grammar in an object-oriented way");
            Console.WriteLine("• Supports recursive structures naturally");
            Console.WriteLine("• Separates parsing from execution");
            
            Console.WriteLine("\nCommon Use Cases:");
            Console.WriteLine("• Programming languages interpreters");
            Console.WriteLine("• SQL query parsers");
            Console.WriteLine("• Regular expression engines");
            Console.WriteLine("• Configuration file parsers");
            Console.WriteLine("• Mathematical expression evaluators");
            Console.WriteLine("• Domain specific languages (DSL)");
            
            Console.WriteLine("\nWhen to Use Interpreter Pattern:");
            Console.WriteLine("• Grammar is simple and stable");
            Console.WriteLine("• Efficiency is not critical");
            Console.WriteLine("• Need to interpret domain-specific expressions");
            Console.WriteLine("• Want to separate syntax from semantics");
            Console.WriteLine("• Grammar changes frequently");
            
            Console.WriteLine("\nMathematical Expression Features Demonstrated:");
            Console.WriteLine("• Basic arithmetic operations (+, -, ×, ÷)");
            Console.WriteLine("• Variable substitution and evaluation");
            Console.WriteLine("• Parentheses for grouping");
            Console.WriteLine("• Expression tree construction");
            Console.WriteLine("• Context-based variable management");
            Console.WriteLine("• Error handling (division by zero)");
        }
    }

    // Additional expression classes for demonstration
    public class PowerExpression : IExpression
    {
        private readonly IExpression _base;
        private readonly IExpression _exponent;

        public PowerExpression(IExpression baseExpr, IExpression exponent)
        {
            _base = baseExpr;
            _exponent = exponent;
        }

        public double Interpret(Context context)
        {
            var baseValue = _base.Interpret(context);
            var expValue = _exponent.Interpret(context);
            var result = Math.Pow(baseValue, expValue);
            Console.WriteLine($"[Power] {baseValue}^{expValue} = {result}");
            return result;
        }

        public override string ToString()
        {
            return $"({_base}^{_exponent})";
        }
    }

    public class SqrtExpression : IExpression
    {
        private readonly IExpression _expression;

        public SqrtExpression(IExpression expression)
        {
            _expression = expression;
        }

        public double Interpret(Context context)
        {
            var value = _expression.Interpret(context);
            if (value < 0)
            {
                throw new ArgumentException("Cannot calculate square root of negative number");
            }
            var result = Math.Sqrt(value);
            Console.WriteLine($"[Sqrt] √{value} = {result}");
            return result;
        }

        public override string ToString()
        {
            return $"√({_expression})";
        }
    }
}