namespace Interpreter.Expressions
{
    /// <summary>
    /// Mathematical expression interface
    /// Defines the contract for all expression types in the interpreter
    /// </summary>
    public interface IExpression
    {
        /// <summary>
        /// Interprets/calculates the expression value
        /// </summary>
        double Interpret(Context context);
        
        /// <summary>
        /// Gets the expression representation
        /// </summary>
        string ToString();
    }
}