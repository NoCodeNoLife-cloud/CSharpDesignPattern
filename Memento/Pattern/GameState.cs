namespace Memento.Pattern
{
    /// <summary>
    /// Game state originator
    /// Maintains game state and can save/restore checkpoints
    /// </summary>
    public class GameState : IOriginator
    {
        private int _level;
        private int _score;
        private int _lives;
        private string _playerName;
        private List<string> _inventory;
        private DateTime _gameTime;
        private readonly List<GameEvent> _eventLog;

        public GameState(string playerName = "Player")
        {
            _playerName = playerName;
            _level = 1;
            _score = 0;
            _lives = 3;
            _inventory = new List<string>();
            _gameTime = DateTime.Now;
            _eventLog = new List<GameEvent>();

            LogEvent("Game started", "New game initialized");
            Console.WriteLine($"[GameState] New game created for player: {_playerName}");
        }

        public IMemento SaveState(string name = "")
        {
            var gameStateData = new GameStateData
            {
                Level = _level,
                Score = _score,
                Lives = _lives,
                PlayerName = _playerName,
                Inventory = new List<string>(_inventory),
                GameTime = _gameTime,
                EventLog = new List<GameEvent>(_eventLog)
            };

            var mementoName = string.IsNullOrEmpty(name) ? $"Level{_level}_Checkpoint" : name;
            var memento = new Memento(gameStateData, mementoName);

            Console.WriteLine($"[GameState] State saved: {mementoName}");
            Console.WriteLine($"  Level: {_level}, Score: {_score}, Lives: {_lives}");
            Console.WriteLine($"  Inventory items: {_inventory.Count}");

            return memento;
        }

        public void RestoreState(IMemento memento)
        {
            if (memento.GetState() is not GameStateData state)
            {
                throw new ArgumentException("Invalid memento state type");
            }

            _level = state.Level;
            _score = state.Score;
            _lives = state.Lives;
            _playerName = state.PlayerName;
            _inventory = new List<string>(state.Inventory);
            _gameTime = state.GameTime;
            _eventLog.Clear();
            _eventLog.AddRange(state.EventLog);

            Console.WriteLine($"[GameState] State restored from: {memento.GetName()}");
            Console.WriteLine($"  Player: {_playerName}");
            Console.WriteLine($"  Level: {_level}, Score: {_score}, Lives: {_lives}");
            Console.WriteLine($"  Inventory: {string.Join(", ", _inventory)}");
        }

        public string GetCurrentStateInfo()
        {
            return $"Player: {_playerName} | " +
                   $"Level: {_level} | " +
                   $"Score: {_score} | " +
                   $"Lives: {_lives} | " +
                   $"Items: {_inventory.Count}";
        }

        // Game operations
        public void AdvanceLevel()
        {
            _level++;
            var scoreBonus = _level * 1000;
            _score += scoreBonus;
            _gameTime = DateTime.Now;

            LogEvent("Level Up", $"Advanced to level {_level}, gained {scoreBonus} points");
            Console.WriteLine($"[GameState] {_playerName} advanced to level {_level} (+{scoreBonus} points)");
        }

        public void AddScore(int points)
        {
            _score += points;
            _gameTime = DateTime.Now;

            LogEvent("Score Gain", $"Gained {points} points");
            Console.WriteLine($"[GameState] {_playerName} gained {points} points (Total: {_score})");
        }

        public void LoseLife()
        {
            if (_lives > 0)
            {
                _lives--;
                _gameTime = DateTime.Now;

                LogEvent("Life Lost", $"Lost a life, {_lives} remaining");
                Console.WriteLine($"[GameState] {_playerName} lost a life ({_lives} remaining)");
            }
        }

        public void GainLife()
        {
            _lives++;
            _gameTime = DateTime.Now;

            LogEvent("Extra Life", "Gained an extra life");
            Console.WriteLine($"[GameState] {_playerName} gained an extra life ({_lives} total)");
        }

        public void AddItem(string item)
        {
            if (!_inventory.Contains(item))
            {
                _inventory.Add(item);
                _gameTime = DateTime.Now;

                LogEvent("Item Collected", $"Collected {item}");
                Console.WriteLine($"[GameState] {_playerName} collected {item}");
            }
        }

        public void RemoveItem(string item)
        {
            if (_inventory.Remove(item))
            {
                _gameTime = DateTime.Now;

                LogEvent("Item Used", $"Used {item}");
                Console.WriteLine($"[GameState] {_playerName} used {item}");
            }
        }

        public void SetPlayerName(string name)
        {
            var oldName = _playerName;
            _playerName = name;
            _gameTime = DateTime.Now;

            LogEvent("Name Changed", $"Changed name from {oldName} to {name}");
            Console.WriteLine($"[GameState] Player name changed from {oldName} to {name}");
        }

        // Getters
        public int GetLevel() => _level;
        public int GetScore() => _score;
        public int GetLives() => _lives;
        public string GetPlayerName() => _playerName;
        public List<string> GetInventory() => new List<string>(_inventory);
        public DateTime GetGameTime() => _gameTime;
        public List<GameEvent> GetEventLog() => new List<GameEvent>(_eventLog);

        public void DisplayGameState()
        {
            Console.WriteLine($"\n=== Game State: {_playerName} ===");
            Console.WriteLine($"Level: {_level}");
            Console.WriteLine($"Score: {_score:N0}");
            Console.WriteLine($"Lives: {_lives}");
            Console.WriteLine($"Game Time: {_gameTime:yyyy-MM-dd HH:mm:ss}");

            if (_inventory.Any())
            {
                Console.WriteLine($"Inventory ({_inventory.Count} items):");
                foreach (var item in _inventory)
                {
                    Console.WriteLine($"  • {item}");
                }
            }
            else
            {
                Console.WriteLine("Inventory: Empty");
            }

            Console.WriteLine($"\nRecent Events ({Math.Min(3, _eventLog.Count)} of {_eventLog.Count}):");
            var recentEvents = _eventLog.TakeLast(3);
            foreach (var evt in recentEvents)
            {
                Console.WriteLine($"  [{evt.Timestamp:HH:mm:ss}] {evt.EventType}: {evt.Description}");
            }

            Console.WriteLine(new string('=', 40));
        }

        private void LogEvent(string eventType, string description)
        {
            _eventLog.Add(new GameEvent
            {
                EventType = eventType,
                Description = description,
                Timestamp = DateTime.Now
            });

            // Keep only last 50 events to prevent memory issues
            if (_eventLog.Count > 50)
            {
                _eventLog.RemoveAt(0);
            }
        }

        // Game state data class for serialization
        private class GameStateData
        {
            public int Level { get; set; }
            public int Score { get; set; }
            public int Lives { get; set; }
            public string PlayerName { get; set; } = string.Empty;
            public List<string> Inventory { get; set; } = new List<string>();
            public DateTime GameTime { get; set; }
            public List<GameEvent> EventLog { get; set; } = new List<GameEvent>();

            public override string ToString()
            {
                return $"GameState[Player: {PlayerName}, Level: {Level}, Score: {Score}, Lives: {Lives}]";
            }
        }

        // Game event structure
        public class GameEvent
        {
            public string EventType { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public DateTime Timestamp { get; set; }
        }
    }
}