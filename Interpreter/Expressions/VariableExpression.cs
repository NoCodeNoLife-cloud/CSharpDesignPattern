namespace Interpreter.Expressions
{
    /// <summary>
    /// Variable expression
    /// Represents a named variable that gets its value from the context
    /// </summary>
    public class VariableExpression : IExpression
    {
        private readonly string _variableName;

        public VariableExpression(string variableName)
        {
            if (string.IsNullOrWhiteSpace(variableName))
            {
                throw new ArgumentException("Variable name cannot be null or empty");
            }
            
            _variableName = variableName.Trim();
        }

        public double Interpret(Context context)
        {
            if (!context.HasVariable(_variableName))
            {
                throw new ArgumentException($"Variable '{_variableName}' not defined in context");
            }

            var value = context.GetVariable(_variableName);
            Console.WriteLine($"[Variable] Interpreting '{_variableName}' = {value}");
            return value;
        }

        public override string ToString()
        {
            return _variableName;
        }

        public string GetVariableName()
        {
            return _variableName;
        }

        public bool IsValidVariableName()
        {
            // Simple validation - starts with letter, contains only letters, digits, underscore
            if (string.IsNullOrEmpty(_variableName))
                return false;
                
            if (!char.IsLetter(_variableName[0]))
                return false;
                
            return _variableName.All(c => char.IsLetterOrDigit(c) || c == '_');
        }

        public static bool IsValidVariableName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;
                
            if (!char.IsLetter(name[0]))
                return false;
                
            return name.All(c => char.IsLetterOrDigit(c) || c == '_');
        }
    }
}