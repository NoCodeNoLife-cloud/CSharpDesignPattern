// See https://aka.ms/new-console-template for more information
using Memento.Pattern;

namespace Memento
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Memento Pattern Implementation Examples ===\n");

            try
            {
                // Demonstrate document editor with undo functionality
                DemonstrateDocumentEditor();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate game state management
                DemonstrateGameStateManagement();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate advanced caretaker features
                DemonstrateAdvancedCaretaker();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate multiple originators scenario
                DemonstrateMultipleOriginators();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate memento pattern benefits
                DemonstratePatternBenefits();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
            
            Console.WriteLine("\n=== Memento Pattern Demo Completed ===");
            Console.ReadKey();
        }

        /// <summary>
        /// Demonstrates document editor with undo/redo functionality
        /// </summary>
        private static void DemonstrateDocumentEditor()
        {
            Console.WriteLine("1. Document Editor Undo/Redo Demo");
            Console.WriteLine("====================================");
            
            var document = new DocumentEditor("MyDocument.txt", "Hello World");
            var caretaker = new Caretaker("Document Editor");
            
            // Initial state
            document.DisplayDocument();
            caretaker.AddMemento(document.SaveState("Initial"));
            
            // Make some edits
            Console.WriteLine("\nMaking edits...");
            document.InsertText("! How are you?", 11);
            caretaker.AddMemento(document.SaveState("Added greeting"));
            
            document.DeleteText(0, 5); // Remove "Hello"
            caretaker.AddMemento(document.SaveState("Removed greeting"));
            
            document.InsertText("Greetings", 0);
            caretaker.AddMemento(document.SaveState("Added formal greeting"));
            
            document.SetFileName("FormalLetter.txt");
            caretaker.AddMemento(document.SaveState("Renamed file"));
            
            document.DisplayDocument();
            caretaker.DisplayHistory();
            
            // Undo operations
            Console.WriteLine("\nPerforming undo operations:");
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    var memento = caretaker.GetMemento(caretaker.GetMementoCount() - 2 - i);
                    document.RestoreState(memento);
                    document.DisplayDocument();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Undo failed: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Demonstrates game state management with checkpoints
        /// </summary>
        private static void DemonstrateGameStateManagement()
        {
            Console.WriteLine("\n2. Game State Management Demo");
            Console.WriteLine("==============================");
            
            var game = new GameState("HeroPlayer");
            var gameCaretaker = new Caretaker("Game State", 20);
            
            // Initial game state
            game.DisplayGameState();
            gameCaretaker.AddMemento(game.SaveState("GameStart"));
            
            // Progress through levels
            Console.WriteLine("\nProgressing through game:");
            game.AdvanceLevel();
            game.AddItem("Sword");
            game.AddScore(500);
            gameCaretaker.AddMemento(game.SaveState("Level1Complete"));
            
            game.AdvanceLevel();
            game.AddItem("Shield");
            game.AddScore(750);
            game.GainLife();
            gameCaretaker.AddMemento(game.SaveState("Level2Complete"));
            
            game.LoseLife();
            game.AddScore(1000);
            gameCaretaker.AddMemento(game.SaveState("BossBattle"));
            
            game.AdvanceLevel();
            game.AddItem("Magic Potion");
            game.AddScore(2000);
            gameCaretaker.AddMemento(game.SaveState("Level3Complete"));
            
            game.DisplayGameState();
            gameCaretaker.DisplayHistory();
            
            // Restore to checkpoint
            Console.WriteLine("\nRestoring to Level 2 checkpoint:");
            try
            {
                var checkpoint = gameCaretaker.GetMementoByName("Level2Complete");
                game.RestoreState(checkpoint);
                game.DisplayGameState();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Restore failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Demonstrates advanced caretaker features
        /// </summary>
        private static void DemonstrateAdvancedCaretaker()
        {
            Console.WriteLine("\n3. Advanced Caretaker Features Demo");
            Console.WriteLine("====================================");
            
            var document = new DocumentEditor("AdvancedTest.txt");
            var caretaker = new Caretaker("Advanced Editor", 5); // Small history limit
            
            // Create many snapshots to test history management
            var changes = new[]
            {
                "First line of text",
                "Second line added",
                "Third line appended",
                "Fourth line included",
                "Fifth line inserted",
                "Sixth line added"
            };
            
            Console.WriteLine("Creating multiple snapshots:");
            for (int i = 0; i < changes.Length; i++)
            {
                document.InsertText(changes[i] + Environment.NewLine);
                caretaker.AddMemento(document.SaveState($"Edit_{i + 1}"));
            }
            
            document.DisplayDocument();
            caretaker.DisplayHistory();
            
            // Test time-based filtering
            Console.WriteLine("\nTesting time-based operations:");
            var oldMementos = caretaker.GetOldMementos(TimeSpan.FromMinutes(1));
            Console.WriteLine($"Mementos older than 1 minute: {oldMementos.Count}");
            
            // Test chronological ordering
            Console.WriteLine("\nChronological order:");
            var chronological = caretaker.GetChronologicalMementos();
            foreach (var memento in chronological.Take(3))
            {
                Console.WriteLine($"  {memento.GetTimestamp():HH:mm:ss} - {memento.GetName()}");
            }
        }

        /// <summary>
        /// Demonstrates multiple originators scenario
        /// </summary>
        private static void DemonstrateMultipleOriginators()
        {
            Console.WriteLine("\n4. Multiple Originators Demo");
            Console.WriteLine("=============================");
            
            // Create multiple originators
            var doc1 = new DocumentEditor("Document1.txt", "First document content");
            var doc2 = new DocumentEditor("Document2.txt", "Second document content");
            var game1 = new GameState("Player1");
            var game2 = new GameState("Player2");
            
            // Single caretaker managing multiple originators
            var multiCaretaker = new Caretaker("Multi-Originator Manager");
            
            // Save states from different originators
            multiCaretaker.AddMemento(doc1.SaveState("Doc1_Initial"));
            multiCaretaker.AddMemento(game1.SaveState("Game1_Start"));
            multiCaretaker.AddMemento(doc2.SaveState("Doc2_Initial"));
            multiCaretaker.AddMemento(game2.SaveState("Game2_Start"));
            
            // Make changes
            doc1.InsertText(" - Updated");
            game1.AdvanceLevel();
            game1.AddScore(1000);
            doc2.DeleteText(7, 8); // Remove "document"
            
            // Save updated states
            multiCaretaker.AddMemento(doc1.SaveState("Doc1_Updated"));
            multiCaretaker.AddMemento(game1.SaveState("Game1_Level1"));
            multiCaretaker.AddMemento(doc2.SaveState("Doc2_Modified"));
            
            // Display combined history
            multiCaretaker.DisplayHistory();
            
            Console.WriteLine("\nCurrent states:");
            doc1.DisplayDocument();
            doc2.DisplayDocument();
            game1.DisplayGameState();
            game2.DisplayGameState();
        }

        /// <summary>
        /// Demonstrates memento pattern benefits and concepts
        /// </summary>
        private static void DemonstratePatternBenefits()
        {
            Console.WriteLine("\n5. Memento Pattern Benefits Demo");
            Console.WriteLine("==================================");
            
            Console.WriteLine("Memento Pattern Key Benefits:");
            Console.WriteLine("• Preserves encapsulation boundaries");
            Console.WriteLine("• Simplifies originator classes");
            Console.WriteLine("• Provides easy rollback capability");
            Console.WriteLine("• Supports multiple undo/redo levels");
            Console.WriteLine("• Enables checkpoint and restore functionality");
            
            Console.WriteLine("\nCommon Use Cases:");
            Console.WriteLine("• Text editors with undo/redo");
            Console.WriteLine("• Game state saving/loading");
            Console.WriteLine("• Database transactions");
            Console.WriteLine("• Form wizards with back/forward");
            Console.WriteLine("• Configuration management");
            Console.WriteLine("• Workflow systems");
            
            Console.WriteLine("\nWhen to Use Memento Pattern:");
            Console.WriteLine("• Need to save and restore object state");
            Console.WriteLine("• Want to avoid exposing internal state");
            Console.WriteLine("• Need multiple checkpoint capability");
            Console.WriteLine("• Want clean separation of concerns");
            Console.WriteLine("• State changes are frequent and complex");
            
            Console.WriteLine("\nFeatures Demonstrated:");
            Console.WriteLine("• State preservation and restoration");
            Console.WriteLine("• History management with size limits");
            Console.WriteLine("• Named checkpoints and automatic naming");
            Console.WriteLine("• Time-based state filtering");
            Console.WriteLine("• Multiple originator support");
            Console.WriteLine("• Comprehensive state information");
        }
    }
}