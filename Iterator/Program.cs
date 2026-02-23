// See https://aka.ms/new-console-template for more information
using Iterator.Pattern;

namespace Iterator
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Iterator Pattern Implementation Examples ===\n");

            try
            {
                // Demonstrate book collection iteration
                DemonstrateBookCollection();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate employee collection iteration
                DemonstrateEmployeeCollection();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate different iterator types
                DemonstrateIteratorTypes();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate advanced iteration scenarios
                DemonstrateAdvancedIteration();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate iterator pattern benefits
                DemonstratePatternBenefits();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
            
            Console.WriteLine("\n=== Iterator Pattern Demo Completed ===");
            Console.ReadKey();
        }

        /// <summary>
        /// Demonstrates book collection iteration
        /// </summary>
        private static void DemonstrateBookCollection()
        {
            Console.WriteLine("1. Book Collection Iteration Demo");
            Console.WriteLine("==================================");
            
            var bookCollection = new BookCollection();
            
            // Add books
            bookCollection.Add(new Book("The Great Gatsby", "F. Scott Fitzgerald", 1925, "978-0-7432-7356-5", "Classic"));
            bookCollection.Add(new Book("To Kill a Mockingbird", "Harper Lee", 1960, "978-0-06-112008-4", "Fiction"));
            bookCollection.Add(new Book("1984", "George Orwell", 1949, "978-0-452-28423-4", "Dystopian"));
            bookCollection.Add(new Book("Pride and Prejudice", "Jane Austen", 1813, "978-0-14-143951-8", "Romance"));
            bookCollection.Add(new Book("The Catcher in the Rye", "J.D. Salinger", 1951, "978-0-316-76948-0", "Coming-of-age"));
            
            bookCollection.DisplayCollection();
            
            // Forward iteration
            Console.WriteLine("\nForward iteration:");
            var forwardIterator = bookCollection.CreateIterator();
            while (forwardIterator.MoveNext())
            {
                var book = forwardIterator.Current;
                Console.WriteLine($"  Position {forwardIterator.Position + 1}: {book}");
            }
            
            // Reverse iteration
            Console.WriteLine("\nReverse iteration:");
            var reverseIterator = bookCollection.CreateReverseIterator();
            while (reverseIterator.MoveNext())
            {
                var book = reverseIterator.Current;
                Console.WriteLine($"  Position {reverseIterator.Position + 1}: {book}");
            }
            
            // Demonstrate iterator position tracking
            Console.WriteLine("\nIterator position tracking:");
            var iterator = bookCollection.CreateIterator();
            iterator.MoveNext(); // First book
            Console.WriteLine($"Current position: {iterator.Position + 1}");
            Console.WriteLine($"Is first: {iterator.IsFirst()}");
            Console.WriteLine($"Is last: {iterator.IsLast()}");
            Console.WriteLine($"Has next: {iterator.HasNext()}");
        }

        /// <summary>
        /// Demonstrates employee collection iteration
        /// </summary>
        private static void DemonstrateEmployeeCollection()
        {
            Console.WriteLine("\n2. Employee Collection Iteration Demo");
            Console.WriteLine("=====================================");
            
            var employeeCollection = new EmployeeCollection();
            
            // Add employees
            employeeCollection.Add(new Employee(1001, "John Smith", "Engineering", "Software Engineer", 75000, new DateTime(2020, 3, 15)));
            employeeCollection.Add(new Employee(1002, "Jane Doe", "Marketing", "Marketing Manager", 65000, new DateTime(2019, 7, 1)));
            employeeCollection.Add(new Employee(1003, "Bob Johnson", "Engineering", "Senior Developer", 95000, new DateTime(2018, 1, 10)));
            employeeCollection.Add(new Employee(1004, "Alice Brown", "HR", "HR Specialist", 55000, new DateTime(2021, 5, 20)));
            employeeCollection.Add(new Employee(1005, "Charlie Wilson", "Finance", "Financial Analyst", 70000, new DateTime(2020, 11, 8)));
            
            employeeCollection.DisplayCollection();
            
            // Salary-based iteration
            Console.WriteLine("\nIteration by salary (ascending):");
            var salaryIterator = employeeCollection.CreateSalaryIterator();
            while (salaryIterator.MoveNext())
            {
                var employee = salaryIterator.Current;
                Console.WriteLine($"  ${employee.Salary:F2} - {employee.Name} ({employee.Department})");
            }
            
            // Department filtering
            Console.WriteLine("\nEngineering department employees:");
            var engineeringEmployees = employeeCollection.FindEmployeesByDepartment("Engineering");
            foreach (var employee in engineeringEmployees)
            {
                Console.WriteLine($"  {employee.Name} - {employee.Position} (${employee.Salary:F2})");
            }
            
            // Salary statistics
            Console.WriteLine($"\nSalary Statistics:");
            Console.WriteLine($"  Average Salary: ${employeeCollection.GetAverageSalary():F2}");
            Console.WriteLine($"  Total Salary: ${employeeCollection.GetTotalSalary():F2}");
            Console.WriteLine($"  Employee Count: {employeeCollection.Count}");
        }

        /// <summary>
        /// Demonstrates different iterator types and their features
        /// </summary>
        private static void DemonstrateIteratorTypes()
        {
            Console.WriteLine("\n3. Different Iterator Types Demo");
            Console.WriteLine("=================================");
            
            var books = new BookCollection();
            books.Add(new Book("Book A", "Author A", 2020));
            books.Add(new Book("Book B", "Author B", 2021));
            books.Add(new Book("Book C", "Author C", 2022));
            books.Add(new Book("Book D", "Author D", 2023));
            
            // Forward iterator with skipping
            Console.WriteLine("Forward iterator with skipping:");
            var forwardIter = books.CreateIterator() as ForwardIterator<Book>;
            if (forwardIter != null)
            {
                forwardIter.MoveNext(); // Position 0
                Console.WriteLine($"Current: {forwardIter.Current}");
                
                forwardIter.Skip(2); // Skip 2 elements
                Console.WriteLine($"After skipping 2: {forwardIter.Current}");
                
                forwardIter.MoveTo(1); // Move to position 1
                Console.WriteLine($"Moved to position 1: {forwardIter.Current}");
            }
            
            // Reverse iterator with skipping
            Console.WriteLine("\nReverse iterator with skipping:");
            var reverseIter = books.CreateReverseIterator() as ReverseIterator<Book>;
            if (reverseIter != null)
            {
                reverseIter.MoveNext(); // Last element
                Console.WriteLine($"Current: {reverseIter.Current}");
                
                reverseIter.Skip(1); // Skip 1 element backwards
                Console.WriteLine($"After skipping 1: {reverseIter.Current}");
            }
            
            // Iterator comparison
            Console.WriteLine("\nIterator state comparison:");
            var iter1 = books.CreateIterator();
            var iter2 = books.CreateIterator();
            
            iter1.MoveNext();
            iter2.MoveNext();
            iter2.MoveNext();
            
            Console.WriteLine($"Iterator 1 position: {iter1.Position}");
            Console.WriteLine($"Iterator 2 position: {iter2.Position}");
            Console.WriteLine($"Both at same position: {iter1.Position == iter2.Position}");
        }

        /// <summary>
        /// Demonstrates advanced iteration scenarios
        /// </summary>
        private static void DemonstrateAdvancedIteration()
        {
            Console.WriteLine("\n4. Advanced Iteration Scenarios Demo");
            Console.WriteLine("=====================================");
            
            var employees = new EmployeeCollection();
            employees.Add(new Employee(2001, "Developer 1", "Engineering", "Developer", 80000, DateTime.Now.AddYears(-2)));
            employees.Add(new Employee(2002, "Developer 2", "Engineering", "Developer", 85000, DateTime.Now.AddYears(-1)));
            employees.Add(new Employee(2003, "Manager 1", "Engineering", "Manager", 120000, DateTime.Now.AddYears(-3)));
            employees.Add(new Employee(2004, "Designer 1", "Design", "Designer", 70000, DateTime.Now.AddYears(-1)));
            
            // Multiple simultaneous iterations
            Console.WriteLine("Multiple simultaneous iterations:");
            var iter1 = employees.CreateIterator();
            var iter2 = employees.CreateReverseIterator();
            
            Console.WriteLine("Forward iteration:");
            while (iter1.MoveNext())
            {
                Console.WriteLine($"  F: {iter1.Current.Name}");
            }
            
            Console.WriteLine("Reverse iteration:");
            while (iter2.MoveNext())
            {
                Console.WriteLine($"  R: {iter2.Current.Name}");
            }
            
            // Conditional iteration
            Console.WriteLine("\nConditional iteration (salary > $75000):");
            var salaryFilterIter = employees.CreateIterator();
            while (salaryFilterIter.MoveNext())
            {
                var emp = salaryFilterIter.Current;
                if (emp.Salary > 75000)
                {
                    Console.WriteLine($"  High earner: {emp.Name} (${emp.Salary:F2})");
                }
            }
            
            // Range iteration
            Console.WriteLine("\nSalary range iteration ($70000-$90000):");
            var rangeEmployees = employees.FindEmployeesBySalaryRange(70000, 90000);
            foreach (var emp in rangeEmployees)
            {
                Console.WriteLine($"  {emp.Name}: ${emp.Salary:F2}");
            }
        }

        /// <summary>
        /// Demonstrates iterator pattern benefits and concepts
        /// </summary>
        private static void DemonstratePatternBenefits()
        {
            Console.WriteLine("\n5. Iterator Pattern Benefits Demo");
            Console.WriteLine("==================================");
            
            Console.WriteLine("Iterator Pattern Key Benefits:");
            Console.WriteLine("• Provides uniform way to traverse different collections");
            Console.WriteLine("• Decouples algorithms from container implementations");
            Console.WriteLine("• Supports multiple simultaneous traversals");
            Console.WriteLine("• Enables different traversal strategies");
            Console.WriteLine("• Hides internal structure of collections");
            
            Console.WriteLine("\nCommon Use Cases:");
            Console.WriteLine("• Collection traversal in GUI applications");
            Console.WriteLine("• Database result set iteration");
            Console.WriteLine("• File system navigation");
            Console.WriteLine("• Tree and graph traversal");
            Console.WriteLine("• Streaming data processing");
            Console.WriteLine("• Pagination in web applications");
            
            Console.WriteLine("\nWhen to Use Iterator Pattern:");
            Console.WriteLine("• Need to access collection elements without exposing internal structure");
            Console.WriteLine("• Want to provide multiple traversal methods");
            Console.WriteLine("• Need to support different iteration algorithms");
            Console.WriteLine("• Want to decouple traversal logic from collection implementation");
            
            Console.WriteLine("\nFeatures Demonstrated:");
            Console.WriteLine("• Forward and reverse iteration");
            Console.WriteLine("• Position tracking and bounds checking");
            Console.WriteLine("• Multiple iterator instances");
            Console.WriteLine("• Custom iteration strategies");
            Console.WriteLine("• Safe element access with error handling");
            Console.WriteLine("• Collection-independent traversal interface");
        }
    }
}