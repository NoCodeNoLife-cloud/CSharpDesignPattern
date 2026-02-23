// See https://aka.ms/new-console-template for more information

using Strategy.Contexts;
using Strategy.Strategies;

namespace Strategy
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Strategy Pattern Implementation Examples ===\n");

            try
            {
                // Demonstrate basic sorting strategies
                DemonstrateBasicSorting();

                Console.WriteLine(new string('=', 70));

                // Demonstrate strategy switching
                DemonstrateStrategySwitching();

                Console.WriteLine(new string('=', 70));

                // Demonstrate performance comparison
                DemonstratePerformanceComparison();

                Console.WriteLine(new string('=', 70));

                // Demonstrate different data types
                DemonstrateDifferentDataTypes();

                Console.WriteLine(new string('=', 70));

                // Demonstrate strategy pattern benefits
                DemonstrateStrategyPatternBenefits();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }

            Console.WriteLine("\n=== Strategy Pattern Demo Completed ===");
            Console.ReadKey();
        }

        /// <summary>
        /// Demonstrates basic sorting strategy operations
        /// </summary>
        private static void DemonstrateBasicSorting()
        {
            Console.WriteLine("1. Basic Sorting Strategies Demo");
            Console.WriteLine("=================================");

            var data = new List<int> { 64, 34, 25, 12, 22, 11, 90 };
            Console.WriteLine($"Original data: [{string.Join(", ", data)}]");

            // Test bubble sort
            var context = new SortingContext<int>(new BubbleSortStrategy<int>());
            var bubbleResult = context.Sort(data);
            Console.WriteLine($"Bubble sort result: [{string.Join(", ", bubbleResult)}]");

            // Test quick sort
            context.SetStrategy(new QuickSortStrategy<int>());
            var quickResult = context.Sort(data);
            Console.WriteLine($"Quick sort result: [{string.Join(", ", quickResult)}]");

            // Test merge sort
            context.SetStrategy(new MergeSortStrategy<int>());
            var mergeResult = context.Sort(data);
            Console.WriteLine($"Merge sort result: [{string.Join(", ", mergeResult)}]");

            context.DisplayPerformanceReport();
        }

        /// <summary>
        /// Demonstrates dynamic strategy switching
        /// </summary>
        private static void DemonstrateStrategySwitching()
        {
            Console.WriteLine("\n2. Dynamic Strategy Switching Demo");
            Console.WriteLine("===================================");

            var data = GenerateRandomData(20);
            Console.WriteLine($"Test data ({data.Count} elements): [{string.Join(", ", data.Take(10))}...]\n");

            var context = new SortingContext<int>(new InsertionSortStrategy<int>());

            // Sort with insertion sort (good for small/nearly sorted data)
            Console.WriteLine($"Current strategy: {context.GetCurrentStrategyInfo()}");
            var result1 = context.Sort(data);

            // Switch to quick sort for larger datasets
            context.SetStrategy(new QuickSortStrategy<int>());
            Console.WriteLine($"\nSwitched to: {context.GetCurrentStrategyInfo()}");
            var result2 = context.Sort(data);

            // Switch to merge sort for guaranteed performance
            context.SetStrategy(new MergeSortStrategy<int>());
            Console.WriteLine($"\nSwitched to: {context.GetCurrentStrategyInfo()}");
            var result3 = context.Sort(data);

            Console.WriteLine($"\nAll results are equal: {result1.SequenceEqual(result2) && result2.SequenceEqual(result3)}");
            context.DisplayPerformanceReport();
        }

        /// <summary>
        /// Demonstrates performance comparison between strategies
        /// </summary>
        private static void DemonstratePerformanceComparison()
        {
            Console.WriteLine("\n3. Performance Comparison Demo");
            Console.WriteLine("===============================");

            // Test with different data sizes
            var sizes = new[] { 100, 500, 1000 };
            var strategies = new ISortingStrategy<int>[]
            {
                new BubbleSortStrategy<int>(),
                new InsertionSortStrategy<int>(),
                new QuickSortStrategy<int>(),
                new MergeSortStrategy<int>()
            };

            var context = new SortingContext<int>(strategies[0]);

            foreach (var size in sizes)
            {
                Console.WriteLine($"\nTesting with {size} elements:");
                var testData = GenerateRandomData(size);

                foreach (var strategy in strategies)
                {
                    context.SetStrategy(strategy);
                    context.Sort(testData);
                }
            }

            context.DisplayPerformanceReport();
        }

        /// <summary>
        /// Demonstrates strategy pattern with different data types
        /// </summary>
        private static void DemonstrateDifferentDataTypes()
        {
            Console.WriteLine("\n4. Different Data Types Demo");
            Console.WriteLine("=============================");

            // String sorting
            var stringData = new List<string> { "banana", "apple", "cherry", "date", "elderberry" };
            Console.WriteLine($"String data: [{string.Join(", ", stringData)}]");

            var stringContext = new SortingContext<string>(new QuickSortStrategy<string>());
            var sortedStrings = stringContext.Sort(stringData);
            Console.WriteLine($"Sorted strings: [{string.Join(", ", sortedStrings)}]\n");

            // Double sorting
            var doubleData = new List<double> { 3.14, 2.71, 1.41, 1.73, 0.57 };
            Console.WriteLine($"Double data: [{string.Join(", ", doubleData)}]");

            var doubleContext = new SortingContext<double>(new MergeSortStrategy<double>());
            var sortedDoubles = doubleContext.Sort(doubleData);
            Console.WriteLine($"Sorted doubles: [{string.Join(", ", sortedDoubles)}]\n");

            // Custom object sorting
            var personData = new List<Person>
            {
                new Person("Alice", 30),
                new Person("Bob", 25),
                new Person("Charlie", 35)
            };

            Console.WriteLine("Person data:");
            foreach (var person in personData)
            {
                Console.WriteLine($"  {person}");
            }

            var personContext = new SortingContext<Person>(new QuickSortStrategy<Person>());
            var sortedPersons = personContext.Sort(personData);

            Console.WriteLine("\nSorted by age:");
            foreach (var person in sortedPersons)
            {
                Console.WriteLine($"  {person}");
            }
        }

        /// <summary>
        /// Demonstrates strategy pattern benefits and concepts
        /// </summary>
        private static void DemonstrateStrategyPatternBenefits()
        {
            Console.WriteLine("\n5. Strategy Pattern Benefits Demo");
            Console.WriteLine("==================================");

            Console.WriteLine("Strategy Pattern Key Benefits:");
            Console.WriteLine("• Enables selecting algorithms at runtime");
            Console.WriteLine("• Eliminates conditional statements");
            Console.WriteLine("• Makes algorithms interchangeable");
            Console.WriteLine("• Supports adding new algorithms easily");
            Console.WriteLine("• Promotes loose coupling");

            Console.WriteLine("\nCommon Use Cases:");
            Console.WriteLine("• Sorting algorithms");
            Console.WriteLine("• Compression algorithms");
            Console.WriteLine("• Payment processing methods");
            Console.WriteLine("• Route calculation algorithms");
            Console.WriteLine("• Data validation strategies");
            Console.WriteLine("• Cache eviction policies");

            Console.WriteLine("\nWhen to Use Strategy Pattern:");
            Console.WriteLine("• Multiple algorithms for same operation");
            Console.WriteLine("• Need to switch algorithms at runtime");
            Console.WriteLine("• Want to eliminate complex conditionals");
            Console.WriteLine("• Need to add new algorithms frequently");
            Console.WriteLine("• Algorithms should be configurable");
        }

        /// <summary>
        /// Generates random integer data for testing
        /// </summary>
        private static List<int> GenerateRandomData(int size)
        {
            var random = new Random(42); // Fixed seed for reproducible results
            return Enumerable.Range(0, size).Select(_ => random.Next(1, 1000)).ToList();
        }
    }

    /// <summary>
    /// Sample person class for demonstrating custom object sorting
    /// </summary>
    public class Person : IComparable<Person>
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public int CompareTo(Person? other)
        {
            if (other == null) return 1;
            return Age.CompareTo(other.Age);
        }

        public override string ToString()
        {
            return $"{Name} ({Age} years)";
        }
    }
}