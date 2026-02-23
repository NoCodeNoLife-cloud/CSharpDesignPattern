// See https://aka.ms/new-console-template for more information
using Command.Commands;
using Command.Receivers;
using Command.Invokers;

namespace Command
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Command Pattern Implementation Examples ===\n");

            try
            {
                // Demonstrate basic command operations
                DemonstrateBasicCommands();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate undo/redo functionality
                DemonstrateUndoRedo();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate command composition
                DemonstrateCommandComposition();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate macro commands
                DemonstrateMacroCommands();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate command pattern benefits
                DemonstrateCommandPatternBenefits();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
            
            Console.WriteLine("\n=== Command Pattern Demo Completed ===");
            Console.ReadKey();
        }

        /// <summary>
        /// Demonstrates basic command operations
        /// </summary>
        private static void DemonstrateBasicCommands()
        {
            Console.WriteLine("1. Basic Command Operations Demo");
            Console.WriteLine("=================================");
            
            var editor = new TextEditor("Hello World");
            var commandManager = new CommandManager();
            
            Console.WriteLine($"Initial content: '{editor.Content}'\n");
            
            // Insert text
            var insertCommand = new InsertTextCommand(editor, " Beautiful", 5);
            commandManager.ExecuteCommand(insertCommand);
            Console.WriteLine($"After insert: '{editor.Content}'\n");
            
            // Delete text
            var deleteCommand = new DeleteTextCommand(editor, 0, 5);
            commandManager.ExecuteCommand(deleteCommand);
            Console.WriteLine($"After delete: '{editor.Content}'\n");
            
            // Replace text
            var replaceCommand = new ReplaceTextCommand(editor, "Hi", 0, 5);
            commandManager.ExecuteCommand(replaceCommand);
            Console.WriteLine($"After replace: '{editor.Content}'\n");
            
            // Clear content
            var clearCommand = new ClearCommand(editor);
            commandManager.ExecuteCommand(clearCommand);
            Console.WriteLine($"After clear: '{editor.Content}'\n");
            
            commandManager.DisplayStatus();
        }

        /// <summary>
        /// Demonstrates undo and redo functionality
        /// </summary>
        private static void DemonstrateUndoRedo()
        {
            Console.WriteLine("\n2. Undo/Redo Functionality Demo");
            Console.WriteLine("=================================");
            
            var editor = new TextEditor();
            var commandManager = new CommandManager();
            
            // Build up some content
            commandManager.ExecuteCommand(new InsertTextCommand(editor, "First", 0));
            commandManager.ExecuteCommand(new InsertTextCommand(editor, " Second", editor.GetLength()));
            commandManager.ExecuteCommand(new InsertTextCommand(editor, " Third", editor.GetLength()));
            
            Console.WriteLine($"\nBuilt content: '{editor.Content}'");
            commandManager.DisplayStatus();
            
            // Perform undo operations
            Console.WriteLine("\nPerforming undo operations:");
            commandManager.Undo();
            Console.WriteLine($"After 1st undo: '{editor.Content}'");
            
            commandManager.Undo();
            Console.WriteLine($"After 2nd undo: '{editor.Content}'");
            
            commandManager.Undo();
            Console.WriteLine($"After 3rd undo: '{editor.Content}'");
            
            // Perform redo operations
            Console.WriteLine("\nPerforming redo operations:");
            commandManager.Redo();
            Console.WriteLine($"After 1st redo: '{editor.Content}'");
            
            commandManager.Redo();
            Console.WriteLine($"After 2nd redo: '{editor.Content}'");
            
            commandManager.DisplayStatus();
        }

        /// <summary>
        /// Demonstrates command composition and complex operations
        /// </summary>
        private static void DemonstrateCommandComposition()
        {
            Console.WriteLine("\n3. Command Composition Demo");
            Console.WriteLine("============================");
            
            var editor = new TextEditor("Original Text");
            var commandManager = new CommandManager();
            
            Console.WriteLine($"Starting content: '{editor.Content}'\n");
            
            // Complex operation sequence
            commandManager.ExecuteCommand(new UpperCaseCommand(editor));
            Console.WriteLine($"Uppercase: '{editor.Content}'");
            
            commandManager.ExecuteCommand(new DeleteTextCommand(editor, 0, 8));
            Console.WriteLine($"After delete: '{editor.Content}'");
            
            commandManager.ExecuteCommand(new InsertTextCommand(editor, "Modified ", 0));
            Console.WriteLine($"After insert: '{editor.Content}'");
            
            commandManager.ExecuteCommand(new LowerCaseCommand(editor));
            Console.WriteLine($"Lowercase: '{editor.Content}'\n");
            
            // Show command history
            Console.WriteLine("Command History:");
            var history = commandManager.GetCommandHistory();
            foreach (var command in history)
            {
                Console.WriteLine($"  {command}");
            }
            
            commandManager.DisplayStatus();
        }

        /// <summary>
        /// Demonstrates macro commands and batch operations
        /// </summary>
        private static void DemonstrateMacroCommands()
        {
            Console.WriteLine("\n4. Macro Commands Demo");
            Console.WriteLine("======================");
            
            var editor = new TextEditor();
            var commandManager = new CommandManager();
            
            // Create a formatting macro
            Console.WriteLine("Executing formatting macro:");
            
            commandManager.ExecuteCommand(new InsertTextCommand(editor, "hello world", 0));
            commandManager.ExecuteCommand(new UpperCaseCommand(editor));
            commandManager.ExecuteCommand(new InsertTextCommand(editor, " - Formatted", editor.GetLength()));
            
            Console.WriteLine($"Final result: '{editor.Content}'\n");
            
            // Demonstrate selective undo
            Console.WriteLine("Undoing last operation only:");
            commandManager.Undo();
            Console.WriteLine($"After undo: '{editor.Content}'\n");
            
            // Demonstrate clearing history
            Console.WriteLine("Clearing command history:");
            commandManager.ClearHistory();
            commandManager.DisplayStatus();
        }

        /// <summary>
        /// Demonstrates command pattern benefits and concepts
        /// </summary>
        private static void DemonstrateCommandPatternBenefits()
        {
            Console.WriteLine("\n5. Command Pattern Benefits Demo");
            Console.WriteLine("=================================");
            
            Console.WriteLine("Command Pattern Key Benefits:");
            Console.WriteLine("• Decouples sender from receiver");
            Console.WriteLine("• Supports undo/redo operations");
            Console.WriteLine("• Enables command queuing and logging");
            Console.WriteLine("• Allows dynamic command composition");
            Console.WriteLine("• Facilitates macro commands");
            
            Console.WriteLine("\nCommon Use Cases:");
            Console.WriteLine("• Text editors (undo/redo)");
            Console.WriteLine("• GUI applications (menu commands)");
            Console.WriteLine("• Remote procedure calls");
            Console.WriteLine("• Transaction processing");
            Console.WriteLine("• Job scheduling systems");
            Console.WriteLine("• Game input handling");
            
            Console.WriteLine("\nWhen to Use Command Pattern:");
            Console.WriteLine("• Need to parameterize objects with operations");
            Console.WriteLine("• Want to support undo/redo functionality");
            Console.WriteLine("• Need to queue or log requests");
            Console.WriteLine("• Want to decouple senders from receivers");
            Console.WriteLine("• Need to compose commands into macros");
        }
    }
}