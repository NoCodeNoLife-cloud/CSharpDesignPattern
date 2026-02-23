namespace Interpreter.Expressions
{
    /// <summary>
    /// Interpreter context
    /// Holds variable values and provides evaluation context
    /// </summary>
    public class Context
    {
        private readonly Dictionary<string, double> _variables = new Dictionary<string, double>();
        private readonly Stack<double> _evaluationStack = new Stack<double>();

        public Context()
        {
        }

        public Context(Dictionary<string, double> initialVariables)
        {
            foreach (var kvp in initialVariables)
            {
                _variables[kvp.Key] = kvp.Value;
            }
        }

        /// <summary>
        /// Sets a variable value
        /// </summary>
        public void SetVariable(string name, double value)
        {
            _variables[name] = value;
            Console.WriteLine($"[Context] Variable '{name}' set to {value}");
        }

        /// <summary>
        /// Gets a variable value
        /// </summary>
        public double GetVariable(string name)
        {
            if (_variables.TryGetValue(name, out var value))
            {
                return value;
            }
            
            throw new ArgumentException($"Variable '{name}' not found in context");
        }

        /// <summary>
        /// Checks if variable exists
        /// </summary>
        public bool HasVariable(string name)
        {
            return _variables.ContainsKey(name);
        }

        /// <summary>
        /// Pushes value onto evaluation stack
        /// </summary>
        public void PushValue(double value)
        {
            _evaluationStack.Push(value);
        }

        /// <summary>
        /// Pops value from evaluation stack
        /// </summary>
        public double PopValue()
        {
            if (_evaluationStack.Count == 0)
            {
                throw new InvalidOperationException("Evaluation stack is empty");
            }
            return _evaluationStack.Pop();
        }

        /// <summary>
        /// Peeks at top value of evaluation stack
        /// </summary>
        public double PeekValue()
        {
            if (_evaluationStack.Count == 0)
            {
                throw new InvalidOperationException("Evaluation stack is empty");
            }
            return _evaluationStack.Peek();
        }

        /// <summary>
        /// Gets current stack size
        /// </summary>
        public int StackSize => _evaluationStack.Count;

        /// <summary>
        /// Clears the evaluation stack
        /// </summary>
        public void ClearStack()
        {
            _evaluationStack.Clear();
        }

        /// <summary>
        /// Gets all variable names
        /// </summary>
        public List<string> GetVariableNames()
        {
            return _variables.Keys.ToList();
        }

        /// <summary>
        /// Gets variable count
        /// </summary>
        public int VariableCount => _variables.Count;

        /// <summary>
        /// Displays current context state
        /// </summary>
        public void DisplayContext()
        {
            Console.WriteLine("\n=== Context State ===");
            Console.WriteLine($"Variables ({VariableCount}):");
            
            if (_variables.Any())
            {
                foreach (var kvp in _variables.OrderBy(k => k.Key))
                {
                    Console.WriteLine($"  {kvp.Key} = {kvp.Value}");
                }
            }
            else
            {
                Console.WriteLine("  No variables defined");
            }
            
            Console.WriteLine($"Stack size: {StackSize}");
            if (StackSize > 0)
            {
                Console.WriteLine("Stack contents (top to bottom):");
                var stackCopy = _evaluationStack.ToList();
                stackCopy.Reverse();
                for (int i = 0; i < stackCopy.Count; i++)
                {
                    Console.WriteLine($"  [{i}]: {stackCopy[i]}");
                }
            }
            Console.WriteLine(new string('=', 25));
        }

        /// <summary>
        /// Creates a copy of the context
        /// </summary>
        public Context Clone()
        {
            var clone = new Context();
            foreach (var kvp in _variables)
            {
                clone._variables[kvp.Key] = kvp.Value;
            }
            
            foreach (var value in _evaluationStack)
            {
                clone._evaluationStack.Push(value);
            }
            
            return clone;
        }
    }
}