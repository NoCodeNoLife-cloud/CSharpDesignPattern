using Command.Commands;

namespace Command.Invokers
{
    /// <summary>
    /// Command manager invoker
    /// Manages command execution, undo, and redo operations
    /// </summary>
    public class CommandManager
    {
        private readonly Stack<ICommand> _executedCommands = new Stack<ICommand>();
        private readonly Stack<ICommand> _undoneCommands = new Stack<ICommand>();
        private readonly int _maxHistorySize;

        public CommandManager(int maxHistorySize = 50)
        {
            _maxHistorySize = maxHistorySize;
        }

        public void ExecuteCommand(ICommand command)
        {
            try
            {
                command.Execute();
                _executedCommands.Push(command);
                
                // Clear redo stack when new command is executed
                _undoneCommands.Clear();
                
                // Limit history size
                if (_executedCommands.Count > _maxHistorySize)
                {
                    // Remove oldest commands to maintain size limit
                    var tempStack = new Stack<ICommand>();
                    for (int i = 0; i < _maxHistorySize / 2; i++)
                    {
                        tempStack.Push(_executedCommands.Pop());
                    }
                    _executedCommands.Clear();
                    while (tempStack.Count > 0)
                    {
                        _executedCommands.Push(tempStack.Pop());
                    }
                }
                
                Console.WriteLine($"[CommandManager] Executed: {command.GetDescription()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CommandManager] Execution failed: {ex.Message}");
            }
        }

        public bool CanUndo()
        {
            return _executedCommands.Count > 0;
        }

        public bool CanRedo()
        {
            return _undoneCommands.Count > 0;
        }

        public void Undo()
        {
            if (CanUndo())
            {
                var command = _executedCommands.Pop();
                try
                {
                    command.Undo();
                    _undoneCommands.Push(command);
                    Console.WriteLine($"[CommandManager] Undid: {command.GetDescription()}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[CommandManager] Undo failed: {ex.Message}");
                    // Push command back if undo failed
                    _executedCommands.Push(command);
                }
            }
            else
            {
                Console.WriteLine("[CommandManager] Nothing to undo");
            }
        }

        public void Redo()
        {
            if (CanRedo())
            {
                var command = _undoneCommands.Pop();
                try
                {
                    command.Execute();
                    _executedCommands.Push(command);
                    Console.WriteLine($"[CommandManager] Redid: {command.GetDescription()}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[CommandManager] Redo failed: {ex.Message}");
                    // Push command back if redo failed
                    _undoneCommands.Push(command);
                }
            }
            else
            {
                Console.WriteLine("[CommandManager] Nothing to redo");
            }
        }

        public void ClearHistory()
        {
            _executedCommands.Clear();
            _undoneCommands.Clear();
            Console.WriteLine("[CommandManager] Command history cleared");
        }

        public int GetExecutedCommandsCount()
        {
            return _executedCommands.Count;
        }

        public int GetUndoneCommandsCount()
        {
            return _undoneCommands.Count;
        }

        public List<string> GetCommandHistory()
        {
            var history = new List<string>();
            var executedList = _executedCommands.ToList();
            executedList.Reverse();
            
            for (int i = 0; i < executedList.Count; i++)
            {
                history.Add($"[{i + 1}] {executedList[i].GetDescription()}");
            }
            
            return history;
        }

        public void DisplayStatus()
        {
            Console.WriteLine($"\n=== Command Manager Status ===");
            Console.WriteLine($"Executed Commands: {_executedCommands.Count}");
            Console.WriteLine($"Undone Commands: {_undoneCommands.Count}");
            Console.WriteLine($"Can Undo: {CanUndo()}");
            Console.WriteLine($"Can Redo: {CanRedo()}");
            Console.WriteLine("=============================\n");
        }
    }
}