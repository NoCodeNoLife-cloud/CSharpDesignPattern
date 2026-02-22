using Prototype.Models;

namespace Prototype
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Prototype Pattern Example ===\n");

            // Create prototype manager
            var prototypeManager = new PrototypeManager();

            // Create standard employee prototype
            var standardEmployee = new Employee(1001, "Zhang San", "Development Department")
            {
                Skills = { "C#", "Java", "Python" },
                Address = new Address("Guangzhou Street 1", "Beijing", "China")
            };

            // Add prototype to manager
            prototypeManager.AddPrototype("StandardDeveloper", standardEmployee);

            // Create another prototype
            var managerEmployee = new Employee(2001, "Li Si", "Management Department")
            {
                Skills = { "Project Management", "Team Leadership", "Communication" },
                Address = new Address("Guiomar CBD", "Beijing", "China")
            };
            prototypeManager.AddPrototype("Manager", managerEmployee);

            Console.WriteLine("1. Display prototype objects:");
            Console.WriteLine($"Standard Developer Prototype: {standardEmployee}");
            Console.WriteLine($"Manager Prototype: {managerEmployee}\n");

            // Use shallow clone to create new object
            Console.WriteLine("2. Shallow Clone Example:");
            var clonedEmployee1 = prototypeManager.GetClone("StandardDeveloper");
            clonedEmployee1.Id = 1002;
            clonedEmployee1.Name = "Wang Wu";
            clonedEmployee1.Skills.Add("JavaScript"); // This affects original object due to shallow clone

            Console.WriteLine($"Cloned Object: {clonedEmployee1}");
            Console.WriteLine($"Original Prototype (skills affected): {standardEmployee}\n");

            // Use deep clone to create new object
            Console.WriteLine("3. Deep Clone Example:");
            var clonedEmployee2 = prototypeManager.GetDeepClone("StandardDeveloper");
            clonedEmployee2.Id = 1003;
            clonedEmployee2.Name = "Zhao Liu";
            clonedEmployee2.Skills.Add("React");
            clonedEmployee2.Address.Street = "Software Park";

            Console.WriteLine($"Deep Cloned Object: {clonedEmployee2}");
            Console.WriteLine($"Original Prototype (not affected): {standardEmployee}");
            Console.WriteLine($"Shallow Cloned Object (also not affected): {clonedEmployee1}\n");

            // Demonstrate prototype manager functionality
            Console.WriteLine("4. Prototype Manager Functionality:");
            Console.WriteLine("Available Prototype List:");
            foreach (var key in prototypeManager.GetAllKeys())
            {
                Console.WriteLine($"  - {key}");
            }

            // Direct MemberwiseClone example
            Console.WriteLine("\n5. Direct MemberwiseClone Usage:");
            var directClone = standardEmployee.Clone();
            directClone.Name = "Directly Cloned Employee";
            Console.WriteLine($"Direct Clone: {directClone}");

            Console.WriteLine("\n=== Example Completed ===");
            Console.ReadKey();
        }
    }
}